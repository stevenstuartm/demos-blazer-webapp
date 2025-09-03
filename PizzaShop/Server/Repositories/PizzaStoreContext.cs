using demos.blazer.webapp.PizzaShop.Shared.DTOs.Outputs;
using Microsoft.EntityFrameworkCore;

namespace demos.blazer.webapp.PizzaShop.Server.Repositories
{
    public class PizzaStoreContext : DbContext
    {
        public PizzaStoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderPizza> OrderPizzas { get; set; }

        public DbSet<PizzaSpecial> Specials { get; set; }

        public DbSet<Topping> Toppings { get; set; }

        public DbSet<Pizza> Pizzas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring a many-to-many special -> topping relationship that is friendly for serialization
            modelBuilder.Entity<OrderPizzaTopping>().HasKey(pst => new { pst.PizzaId, pst.ToppingId });
            modelBuilder.Entity<OrderPizzaTopping>().HasOne<OrderPizza>().WithMany(ps => ps.Toppings);
            modelBuilder.Entity<OrderPizzaTopping>().HasOne(pst => pst.Topping).WithMany();
        }
    }
}
