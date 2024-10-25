using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheEleganceShop.Data;
using TheEleganceShop.Models;

namespace TheEleganceShop.Pages.OrderHeaders
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
        ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerID", "CustomerID");
            return Page();
        }

        [BindProperty]
        public OrderHeader OrderHeader { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrderHeader.Add(OrderHeader);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
