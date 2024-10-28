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

namespace TheEleganceShop.Pages.Carts
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly TheEleganceShop.Data.ApplicationDbContext _context;

        public IndexModel(TheEleganceShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Cart> Cart { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Cart = await _context.Cart
                .Include(c => c.Customer).ToListAsync();
        }
    }
}
