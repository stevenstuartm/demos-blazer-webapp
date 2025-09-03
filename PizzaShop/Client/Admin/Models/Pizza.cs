using System.ComponentModel.DataAnnotations;

namespace demos.blazer.webapp.PizzaShop.Client.Admin.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [EmailAddress]
        public string ChefEmail { get; set; }

        [Required]
        [Range(10.00, 25.00)]
        public decimal Price { get; set; }
    }
}
