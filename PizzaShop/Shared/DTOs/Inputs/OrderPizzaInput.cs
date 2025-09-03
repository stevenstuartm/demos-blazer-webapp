using System.ComponentModel.DataAnnotations;

namespace demos.blazer.webapp.PizzaShop.Shared.DTOs.Inputs
{
    /// <summary>
    /// Represents a customized pizza as part of an order
    /// </summary>
    public class OrderPizzaInput
    {
        [Required]
        public int SpecialId { get; set; }

        [Required]
        public int Size { get; set; }

        public List<int>? ToppingIds { get; set; }
    }
}
