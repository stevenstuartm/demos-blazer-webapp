namespace demos.blazer.webappPizzaShop.Shared.DTOs.Outputs
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedTime { get; set; }

        public Address DeliveryAddress { get; set; }

        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();
    }
}
