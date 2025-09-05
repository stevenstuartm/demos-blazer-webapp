using demos.blazer.webapp.PizzaShop.Shared.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace demos.blazer.webapp.PizzaShop.Client.Admin.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must set a name for your pizza.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [EmailAddress(
            ErrorMessage = "You must set a valid email address for the chef responsible for the pizza recipe.")]
        public string ChefEmail { get; set; }

        [Required]
        [Range(10.00, 25.00, ErrorMessage = "You must set a price between $10 and $25.")]
        public decimal Price { get; set; }

        [PizzaBase]
        public string Base { get; set; }
    }
}
