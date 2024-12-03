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


// this model / .cs is only a junction model, its not really meant to be accessed by any user but ill restrict it to admin (employee)...

namespace TheEleganceShop.Pages.CartProducts
{
    [Authorize(Roles = "Admin")]

    public class IndexModel : PageModel
    {
        private readonly TheEleganceShop.Data.ApplicationDbContext _context;

        public IndexModel(TheEleganceShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CartProduct> CartProduct { get;set; } = default!;

        public async Task OnGetAsync()
        {

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            CartProduct = await _context.CartProduct
                .Include(c => c.Cart)
                .Include(c => c.Product).ToListAsync();
        }
    }
}
