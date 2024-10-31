using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore;
using TheEleganceShop.Data;
using TheEleganceShop.Models;

namespace TheEleganceShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Product> SearchResults { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                
                SearchResults = await _context.Product
                    .Where(p => p.ProductName.Contains(search)) 
                    .ToListAsync();
            }
            else
            {
                SearchResults = new List<Product>(); 
            }

            return Page(); 
        }
        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
        {
            // Only allow user to add an item if they are signed in, ELSE REDIRECT
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return RedirectToPage();
        }
    }
}
