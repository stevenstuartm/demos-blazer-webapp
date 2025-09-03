namespace demos.blazer.webappPizzaShop.Shared.DTOs.Inputs
{
    /// <summary>
    /// Represents a customized pizza as part of an order
    /// </summary>
    public class PizzaInput
    {
        public int SpecialId { get; set; }

        public int Size { get; set; }

        public List<int>? ToppingIds { get; set; }
    }
}
