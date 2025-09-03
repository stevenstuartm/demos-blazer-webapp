namespace demos.blazer.webapp.PizzaShop.Shared.DTOs.Outputs
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedTime { get; set; }

        public Address DeliveryAddress { get; set; }

        public List<OrderPizza> Pizzas { get; set; } = new List<OrderPizza>();
    }
}
