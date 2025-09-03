using System.ComponentModel.DataAnnotations;

namespace demos.blazer.webapp.PizzaShop.Client.Public.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ChefEmail { get; set; }

        public decimal Price { get; set; }
    }
}
