using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheEleganceShop.Data;
using TheEleganceShop.Models;

namespace TheEleganceShop.Pages.OrderHeaders
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CheckoutModel(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [BindProperty]
        public OrderHeader OrderHeader { get; set; } = new OrderHeader(); 

        // Property to hold CartProducts for display 
        public IList<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

        
        public decimal TotalAmount { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            // Grabbing the signed in users ID, if not found redirect to Home
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Index");
            }

            // Loading the users Cart and related Cart Products 
            var cart = await _context.Cart
                .Include(c => c.CartProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartProducts.Any())
            {
                return RedirectToPage("/Index");
            }

            
            CartProducts = cart.CartProducts;
            TotalAmount = CartProducts.Sum(cp => cp.Quantity * cp.Product.ProductPrice ?? 0);

           
            OrderHeader.OrderAmount = TotalAmount;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Index");
            }

            // Fetching the cart for the currentlu signed in user
            var cart = await _context.Cart
                .Include(c => c.CartProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartProducts.Any())
            {
                ModelState.AddModelError(string.Empty, "Your cart is empty.");
                return Page();
            }

            // Populating myy fields from the orderheader model. 
            OrderHeader.UserId = userId;
            OrderHeader.OrderDate = DateTime.Now;
            OrderHeader.OrderStatus = "Placed"; 

            // found the ?? notation below on stack overflow
            // I am using it to check if productprice property is null if so, assign 0
            OrderHeader.OrderAmount = cart.CartProducts.Sum(cp => cp.Quantity * cp.Product.ProductPrice ?? 0);

            _context.OrderHeader.Add(OrderHeader);
            await _context.SaveChangesAsync();

            // Creating a OrderDetail instance for each CartProduct
            var orderDetails = cart.CartProducts.Select(cp =>   new OrderDetail
            {
                OrderHeaderID = OrderHeader.OrderHeaderId, 
                ProductID = cp.ProductID,
                ProductQuantity = cp.Quantity,
                ProductShoeSize = null 
            });

            _context.OrderDetail.AddRange(orderDetails);
            await _context.SaveChangesAsync();

            // Clearing entire cart after order is placed to restart

            _context.CartProduct.RemoveRange(cart.CartProducts);
            await _context.SaveChangesAsync();

            return RedirectToPage("/OrderDetails/Index");
        }

        public async Task<IActionResult> OnPostPaymentDetailsAsync()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Index");
            }

            // Loading the user's order in progress, or create a new one if necessary
            var orderHeader = await _context.OrderHeader
                .FirstOrDefaultAsync(o => o.UserId == userId && o.OrderStatus == "Pending");

            if (orderHeader == null)
            {
                // I create a new order if it does not exist
                // default status pending until order is successfully placed
                orderHeader = new OrderHeader
                {
                    UserId = userId,
                    OrderStatus = "Pending",
                    OrderDate = DateTime.UtcNow
                };

                _context.OrderHeader.Add(orderHeader);
                await _context.SaveChangesAsync();
            }

            // Updating my model attributes with the user entered data from the form
            orderHeader.OrderPaymentMethod = OrderHeader.OrderPaymentMethod;
            orderHeader.OrderPaymentCard = OrderHeader.OrderPaymentCard;

           
            await _context.SaveChangesAsync();

            
            return RedirectToPage("/OrderDetails/Index");
        }
    }
}