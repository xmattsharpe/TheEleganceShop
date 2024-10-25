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
    public class DetailsModel : PageModel
    {
        private readonly TheEleganceShop.Data.ApplicationDbContext _context;

        public DetailsModel(TheEleganceShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
