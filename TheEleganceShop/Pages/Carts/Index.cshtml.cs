using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheEleganceShop.Data;
using TheEleganceShop.Models;

namespace TheEleganceShop.Pages.Carts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Cart Cart { get; set; } = default!;

        
        public IList<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

        



        public async Task OnGetAsync()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                // grabbing the user's cart along with all cartproducts, for the user who is signed in
                Cart = await _context.Cart
                    .Include(c => c.CartProducts)
                        .ThenInclude(cp => cp.Product) 
                    .FirstOrDefaultAsync(c => c.UserId == userId); 

                
                // If the user has a cart, the products in their cart are still assigned to cartproducts.
                if (Cart != null)
                {
                    CartProducts = Cart.CartProducts ?? new List<CartProduct>(); 
                }
            }
        }

        public async Task<IActionResult> OnPostRemoveFromCartAsync(int cartProductId)
        {

            // finding the product based on the passed ID
            var cartProduct = await _context.CartProduct.FindAsync(cartProductId);

            // if the product exists in their cart, I am removing it to execute against DB.
            if (cartProduct != null)
            {
                // only if the passed id is not null, execute the save against the db
                _context.CartProduct.Remove(cartProduct);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage(); 
        }

        public async Task<IActionResult> OnPostCheckout()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // again Loading the user's cart along with its products
            Cart = await _context.Cart
                .Include(c => c.CartProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            // Checking if the cart is empty
            if (Cart == null || !Cart.CartProducts.Any())
            {
                // Redirect to the cart page if it's empty
                return RedirectToPage("/Carts/Index");
            }

            foreach (var cartProduct in Cart.CartProducts)
            {
                var orderDetail = new OrderDetail
                {
                    ProductID = cartProduct.ProductID,
                    ProductQuantity = cartProduct.Quantity,

                    
                };

                _context.OrderDetail.Add(orderDetail);

            }

            // Redirecting the Checkout page in OrderHeaders
            return RedirectToPage("/OrderHeaders/Checkout");
        }


    }
}