using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheEleganceShop.Data;
using TheEleganceShop.Models;

namespace TheEleganceShop.Pages.OrderDetails
{
    public class DetailsModel : PageModel
    {
        private readonly TheEleganceShop.Data.ApplicationDbContext _context;

        public DetailsModel(TheEleganceShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetail = await _context.OrderDetail.FirstOrDefaultAsync(m => m.OrderDetailID == id);
            if (orderdetail == null)
            {
                return NotFound();
            }
            else
            {
                OrderDetail = orderdetail;
            }
            return Page();
        }
    }
}
