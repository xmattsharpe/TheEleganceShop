using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheEleganceShop.Data;
using TheEleganceShop.Models;

namespace TheEleganceShop.Pages.OrderDetails
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly TheEleganceShop.Data.ApplicationDbContext _context;

        public IndexModel(TheEleganceShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OrderDetail> OrderDetail { get;set; } = default!;

        public async Task <IActionResult> OnGetAsync()
        {
            // fetch current user id claim about logged in user 
            // SO
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;


            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Index");
            }

            if (User.IsInRole("Admin"))
            {
                OrderDetail = await _context.OrderDetail
                .Include(o => o.OrderHeader)
                .Include(o => o.Product).ToListAsync();
            }
            else
            {
                // oNLY SHOW the relevant order details for the currently logged in user
                OrderDetail = await _context.OrderDetail
                    .Include(o => o.OrderHeader)
                    .Include(o => o.Product).Where(x => x.OrderHeader.UserId == userId).ToListAsync();
            }

            return Page();
        }
    }
}
