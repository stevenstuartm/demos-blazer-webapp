using demos.blazer.webapp.Features.PizzaShop.DTOs;
using Microsoft.EntityFrameworkCore;

namespace demos.blazer.webapp.Features.PizzaShop.Repositories
{
    public class PizzaStoreContext : DbContext
    {
        public PizzaStoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PizzaSpecial> Specials { get; set; }
    }
}
