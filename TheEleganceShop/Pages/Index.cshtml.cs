using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
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
                // Search for products in the database
                SearchResults = await _context.Product
                    .Where(p => p.ProductName.Contains(search)) // Modify this as needed for case sensitivity
                    .ToListAsync();
            }
            else
            {
                SearchResults = new List<Product>(); // No results if search is empty
            }

            return Page(); // Return the same page with the results
        }
        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return RedirectToPage();
        }
    }
}
