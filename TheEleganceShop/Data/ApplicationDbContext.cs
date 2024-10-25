using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheEleganceShop.Models;

namespace TheEleganceShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TheEleganceShop.Models.Cart> Cart { get; set; } = default!;
        public DbSet<TheEleganceShop.Models.CartProduct> CartProduct { get; set; } = default!;
        public DbSet<TheEleganceShop.Models.Customer> Customer { get; set; } = default!;
        public DbSet<TheEleganceShop.Models.OrderDetail> OrderDetail { get; set; } = default!;
        public DbSet<TheEleganceShop.Models.OrderHeader> OrderHeader { get; set; } = default!;
        public DbSet<TheEleganceShop.Models.Product> Product { get; set; } = default!;
    }
}
