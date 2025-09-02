namespace demos.blazer.webapp.Features.PizzaShop.Client.Models
{
    public class OrderWithStatus
    {
        public Order Order { get; set; }

        public string StatusText { get; set; }

        public bool IsDelivered => StatusText == "Delivered";
    }
}
