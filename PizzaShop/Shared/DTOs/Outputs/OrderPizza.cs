using System.Text.Json.Serialization;

namespace demos.blazer.webapp.PizzaShop.Shared.DTOs.Outputs
{
    /// <summary>
    /// Represents a customized pizza as part of an order
    /// </summary>
    public class OrderPizza
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        [JsonIgnore]
        public PizzaSpecial Special { get; set; }

        public int SpecialId { get; set; }

        public int Size { get; set; }

        public List<OrderPizzaTopping> Toppings { get; set; }
    }
}
