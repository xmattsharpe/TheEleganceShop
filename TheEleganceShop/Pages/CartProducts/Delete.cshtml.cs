using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheEleganceShop.Data;
using TheEleganceShop.Models;

namespace TheEleganceShop.Pages.CartProducts
{
    public class DeleteModel : PageModel
    {
        private readonly TheEleganceShop.Data.ApplicationDbContext _context;

        public DeleteModel(TheEleganceShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CartProduct CartProduct { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartproduct = await _context.CartProduct.FirstOrDefaultAsync(m => m.CartProductID == id);

            if (cartproduct == null)
            {
                return NotFound();
            }
            else
            {
                CartProduct = cartproduct;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartproduct = await _context.CartProduct.FindAsync(id);
            if (cartproduct != null)
            {
                CartProduct = cartproduct;
                _context.CartProduct.Remove(CartProduct);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
