using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheEleganceShop.Data;
using TheEleganceShop.Models;

namespace TheEleganceShop.Pages.CartProducts
{
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
            CartProduct = await _context.CartProduct
                .Include(c => c.Cart)
                .Include(c => c.Product).ToListAsync();
        }
    }
}
