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
            var cartProduct = await _context.CartProduct.FindAsync(cartProductId);
            if (cartProduct != null)
            {
                _context.CartProduct.Remove(cartProduct);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(); 
        }
    }
}