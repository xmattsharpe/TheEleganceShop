using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheEleganceShop.Data;
using TheEleganceShop.Models;

namespace TheEleganceShop.Pages.OrderHeaders
{
    public class EditModel : PageModel
    {
        private readonly TheEleganceShop.Data.ApplicationDbContext _context;

        public EditModel(TheEleganceShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderHeader OrderHeader { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderheader =  await _context.OrderHeader.FirstOrDefaultAsync(m => m.OrderHeaderId == id);
            if (orderheader == null)
            {
                return NotFound();
            }
            OrderHeader = orderheader;
           ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerID", "CustomerID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OrderHeader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderHeaderExists(OrderHeader.OrderHeaderId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrderHeaderExists(int id)
        {
            return _context.OrderHeader.Any(e => e.OrderHeaderId == id);
        }
    }
}
