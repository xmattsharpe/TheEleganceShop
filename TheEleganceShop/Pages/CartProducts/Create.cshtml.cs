using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheEleganceShop.Data;
using TheEleganceShop.Models;

namespace TheEleganceShop.Pages.CartProducts
{
    public class CreateModel : PageModel
    {
        private readonly TheEleganceShop.Data.ApplicationDbContext _context;

        public CreateModel(TheEleganceShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CartID"] = new SelectList(_context.Cart, "CartID", "CartID");
        ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "ProductID");
            return Page();
        }

        [BindProperty]
        public CartProduct CartProduct { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CartProduct.Add(CartProduct);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
