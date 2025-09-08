namespace demos.blazer.webapp.PizzaShop.Client.Public.Models
{
    /// <summary>
    /// Represents a customized pizza as part of an order
    /// </summary>
    public class OrderPizza
    {
        public const int DefaultSize = 12;
        public int MinimumSize = 9;
        public int MaximumSize = 17;

        public PizzaSpecial Special { get; set; }

        public int Size { get; set; }

        public List<int>? ToppingIds { get; set; }

        public decimal GetBasePrice() =>
            Special is { FixedSize: not null }
                ? Special.BasePrice
                : (decimal)Size / DefaultSize * Special?.BasePrice ?? 1;

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
