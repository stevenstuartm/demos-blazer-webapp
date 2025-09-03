using demos.blazer.webapp.PizzaShop.Shared;

namespace demos.blazer.webapp.PizzaShop.Client.Public.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreatedTime { get; set; }

        public Address DeliveryAddress { get; set; } = new Address();

        public List<OrderPizza> Pizzas { get; set; } = new List<OrderPizza>();

        public decimal GetTotalPrice() => Pizzas.Sum(p => p.GetTotalPrice());

        public string GetFormattedTotalPrice() => GetTotalPrice().ToString("0.00");

        public string StatusText { get; set; }

        public bool IsDelivered => StatusText == OrderStatuses.Delivered;
    }
}
