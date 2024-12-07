using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheEleganceShop.Data;
using TheEleganceShop.Models;

namespace TheEleganceShop.Pages.OrderHeaders
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly TheEleganceShop.Data.ApplicationDbContext _context;

        public IndexModel(TheEleganceShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OrderHeader> OrderHeader { get;set; } = default!;

        public async Task OnGetAsync()
        {
            
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // only admin can see all user order

            if (User.IsInRole("Admin"))
            {
                OrderHeader = await _context.OrderHeader
                    .Include(o => o.Customer)
                .ToListAsync();

            }


            else
            {
                // only the order headers created from logged in user to be show
                OrderHeader = await _context.OrderHeader
                        .Include(o => o.Customer)
                    .Where(x => x.UserId == userId).ToListAsync();
            }
        }
    }
}
