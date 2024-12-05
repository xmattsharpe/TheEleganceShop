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

        // Property to hold CartProducts for display OF CSHMTL 
        public IList<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

        
        public decimal TotalAmount { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            // Grabbing the signed in users ID, if not found redirect to Home (This fetch a claim method I found via Stack Overflow)
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Index");
            }

           
            // Must go "through" cartproducts to access the products in the include
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

            // if price is null I am using 0 because when null an exception is raised
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
                // custom message for the user if the cart is null
                ModelState.AddModelError(string.Empty, "Your cart is empty. PLEASE BUY SOMETHING :) ");
                return Page();
            }

            // Populating myy fields from the orderheader model. 
            OrderHeader.UserId = userId;
            OrderHeader.OrderDate = DateTime.Now;
            OrderHeader.OrderStatus = "Placed"; 

            
            OrderHeader.OrderAmount = cart.CartProducts.Sum(cp => cp.Quantity * cp.Product.ProductPrice ?? 0);

            _context.OrderHeader.Add(OrderHeader);
            await _context.SaveChangesAsync();

            // creating a OrderDetail object for each cart product
            var orderDetails = cart.CartProducts.Select(cp =>   new OrderDetail
            {
                OrderHeaderID = OrderHeader.OrderHeaderId, 
                ProductID = cp.ProductID,
                ProductQuantity = cp.Quantity,
                ProductShoeSize = null 
            });

            _context.OrderDetail.AddRange(orderDetails);
            await _context.SaveChangesAsync();

          // remove all products from the cart after the order is placed I assume is the most accurate thing to do
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
                    OrderDate = DateTime.Now
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