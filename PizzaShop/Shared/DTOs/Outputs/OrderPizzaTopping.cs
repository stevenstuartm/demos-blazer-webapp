namespace demos.blazer.webapp.PizzaShop.Shared.DTOs.Outputs
{
    public class OrderPizzaTopping
    {
        public Topping Topping { get; set; }

        public int ToppingId { get; set; }
        
        public int PizzaId { get; set; }
    }
}
