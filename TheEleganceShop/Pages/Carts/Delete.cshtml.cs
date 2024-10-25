using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheEleganceShop.Data;
using TheEleganceShop.Models;

namespace TheEleganceShop.Pages.Carts
{
    public class DeleteModel : PageModel
    {
        private readonly TheEleganceShop.Data.ApplicationDbContext _context;

        public DeleteModel(TheEleganceShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cart Cart { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FirstOrDefaultAsync(m => m.CartID == id);

            if (cart == null)
            {
                return NotFound();
            }
            else
            {
                Cart = cart;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart != null)
            {
                Cart = cart;
                _context.Cart.Remove(Cart);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
