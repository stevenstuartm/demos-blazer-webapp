using demos.blazer.webapp.Features.Pizza.DTOs;
using Microsoft.EntityFrameworkCore;

namespace demos.blazer.webapp.Features.Pizza.Repositories
{
    public class PizzaStoreContext : DbContext
    {
        public PizzaStoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PizzaSpecial> Specials { get; set; }
    }
}
