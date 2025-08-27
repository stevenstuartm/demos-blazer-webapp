namespace demos.blazer.webapp.Features.Pizza.DTOs
{
    public class Order
    {
        public int OrderId { get; set; }

        public required string UserId { get; set; }

        public DateTime CreatedTime { get; set; }

        public required Address DeliveryAddress { get; set; }

        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();

        public decimal GetTotalPrice() => Pizzas.Sum(p => p.GetTotalPrice());

        public string GetFormattedTotalPrice() => GetTotalPrice().ToString("0.00");
    }
}
