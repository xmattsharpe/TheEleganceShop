using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheEleganceShop.Data;
using TheEleganceShop.Models;

namespace TheEleganceShop.Pages.OrderHeaders
{
    public class DeleteModel : PageModel
    {
        private readonly TheEleganceShop.Data.ApplicationDbContext _context;

        public DeleteModel(TheEleganceShop.Data.ApplicationDbContext context)
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

            var orderheader = await _context.OrderHeader.FirstOrDefaultAsync(m => m.OrderHeaderId == id);

            if (orderheader == null)
            {
                return NotFound();
            }
            else
            {
                OrderHeader = orderheader;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderheader = await _context.OrderHeader.FindAsync(id);
            if (orderheader != null)
            {
                OrderHeader = orderheader;
                _context.OrderHeader.Remove(OrderHeader);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
