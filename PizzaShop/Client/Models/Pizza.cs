namespace demos.blazer.webappPizzaShop.Client.Models
{
    /// <summary>
    /// Represents a customized pizza as part of an order
    /// </summary>
    public class Pizza
    {
        public const int DefaultSize = 12;
        public const int MinimumSize = 9;
        public const int MaximumSize = 17;

        public PizzaSpecial Special { get; set; }

        public int Size { get; set; }

        public List<int>? ToppingIds { get; set; }

        public decimal GetBasePrice()
        {
            return Size / (decimal)DefaultSize * Special.BasePrice;
        }

        public decimal GetTotalPrice()
        {
            return GetBasePrice();
        }

        public string GetFormattedTotalPrice()
        {
            return GetTotalPrice().ToString("0.00");
        }
    }
}
