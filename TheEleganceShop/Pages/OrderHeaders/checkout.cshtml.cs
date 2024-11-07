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

        // The OrderHeader instance for capturing user input like shipping info
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

            // Load the Cart and related CartProducts for the logged-in user
            var cart = await _context.Cart
                .Include(c => c.CartProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartProducts.Any())
            {
                return RedirectToPage("/Index");
            }

            // Assign CartProducts and calculate the total
            CartProducts = cart.CartProducts;
            TotalAmount = CartProducts.Sum(cp => cp.Quantity * cp.Product.ProductPrice ?? 0);

            // Initialize Order total amount
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

            // Fetch the cart for the current user
            var cart = await _context.Cart
                .Include(c => c.CartProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartProducts.Any())
            {
                ModelState.AddModelError(string.Empty, "Your cart is empty.");
                return Page();
            }

            // Populate OrderHeader with details and save it
            OrderHeader.UserId = userId;
            OrderHeader.OrderDate = DateTime.UtcNow;
            OrderHeader.OrderStatus = "Placed"; // Example status
            OrderHeader.OrderAmount = cart.CartProducts.Sum(cp => cp.Quantity * cp.Product.ProductPrice ?? 0);

            _context.OrderHeader.Add(OrderHeader);
            await _context.SaveChangesAsync();

            // Create OrderDetails for each CartProduct
            var orderDetails = cart.CartProducts.Select(cp => new OrderDetail
            {
                OrderHeaderID = OrderHeader.OrderHeaderId, // Use the saved OrderHeader ID
                ProductID = cp.ProductID,
                ProductQuantity = cp.Quantity,
                ProductShoeSize = null // Assign if applicable
            });

            _context.OrderDetail.AddRange(orderDetails);
            await _context.SaveChangesAsync();

            // Optionally clear the cart
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

            // Load the user's order in progress, or create a new one if necessary
            var orderHeader = await _context.OrderHeader
                .FirstOrDefaultAsync(o => o.UserId == userId && o.OrderStatus == "Pending");

            if (orderHeader == null)
            {
                // Initialize a new order if it doesn’t exist
                orderHeader = new OrderHeader
                {
                    UserId = userId,
                    OrderStatus = "Pending",
                    OrderDate = DateTime.UtcNow
                };

                _context.OrderHeader.Add(orderHeader);
                await _context.SaveChangesAsync();
            }

            // Update order with payment details from the form
            orderHeader.OrderPaymentMethod = OrderHeader.OrderPaymentMethod;
            orderHeader.OrderPaymentCard = OrderHeader.OrderPaymentCard;

            // Save the changes to the order
            await _context.SaveChangesAsync();

            // Redirect to another page (e.g., confirmation) or reload this page to show success message
            return RedirectToPage("/OrderDetails/Index");
        }
    }
}