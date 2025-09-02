namespace demos.blazer.webapp.Features.PizzaShop.Client.Models
{
    public class Order
    {
        public DateTime CreatedTime { get; set; }

        public Address DeliveryAddress { get; set; }

        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();

        public decimal GetTotalPrice() => Pizzas.Sum(p => p.GetTotalPrice());

        public string GetFormattedTotalPrice() => GetTotalPrice().ToString("0.00");
    }
}
