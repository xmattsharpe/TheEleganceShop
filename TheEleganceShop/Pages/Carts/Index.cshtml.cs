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


        // list to hold the IDs that are checked with the checkbox
        [BindProperty]
        public int[] SelectedCartProductIds { get; set; }
        public IList<CartProduct> CartProducts { get; set; } = new List<CartProduct>(); 



        public async Task OnGetAsync()
        {

            // I found this claims method on stack overflow
            // it seems to work as expected using the currently signed in user data
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                
                Cart = await _context.Cart
                    .Include(c => c.CartProducts)
                        .ThenInclude(cp => cp.Product) 
                    .FirstOrDefaultAsync(c => c.UserId == userId); 

                

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
            if (cartProduct != null)
            {
                // only if the passed id is not null, execute the save against the db
                _context.CartProduct.Remove(cartProduct);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(); 
        }

        public async Task<IActionResult> OnpostOrder()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            Cart = await _context.Cart
                .Include(c => c.CartProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            // If there is nothing in their cart, customer cannot advance to the order screen...
            if (Cart == null || !SelectedCartProductIds.Any())
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }


    }
}