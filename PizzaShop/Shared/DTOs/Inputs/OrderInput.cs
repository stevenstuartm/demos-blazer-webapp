using System.ComponentModel.DataAnnotations;

namespace demos.blazer.webapp.PizzaShop.Shared.DTOs.Inputs
{
    public class OrderInput
    {
        [Required]
        public AddressInput DeliveryAddress { get; set; }

        [Required]
        public List<OrderPizzaInput> Pizzas { get; set; }
    }
}
