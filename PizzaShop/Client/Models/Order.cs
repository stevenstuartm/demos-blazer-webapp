using demos.blazer.webappPizzaShop.Shared;

namespace demos.blazer.webappPizzaShop.Client.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreatedTime { get; set; }

        public Address? DeliveryAddress { get; set; }

        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();

        public decimal GetTotalPrice() => Pizzas.Sum(p => p.GetTotalPrice());

        public string GetFormattedTotalPrice() => GetTotalPrice().ToString("0.00");

        public string StatusText { get; set; }

        public bool IsDelivered => StatusText == OrderStatuses.Delivered;
    }
}
