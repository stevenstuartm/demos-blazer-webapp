using System.Text.Json.Serialization;

namespace demos.blazer.webappPizzaShop.Shared.DTOs.Outputs
{
    /// <summary>
    /// Represents a customized pizza as part of an order
    /// </summary>
    public class Pizza
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        [JsonIgnore]
        public PizzaSpecial Special { get; set; }

        public int SpecialId { get; set; }

        public int Size { get; set; }

        public List<PizzaTopping> Toppings { get; set; }
    }
}
