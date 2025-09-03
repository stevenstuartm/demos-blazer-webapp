namespace demos.blazer.webappPizzaShop.Client.Models
{
    public class OrderWithStatus
    {
        public Order Order { get; set; }

        public string StatusText { get; set; }

        public bool IsDelivered => StatusText == "Delivered";
    }
}
