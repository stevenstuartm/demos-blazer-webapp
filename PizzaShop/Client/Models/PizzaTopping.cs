namespace demos.blazer.webappPizzaShop.Client.Models
{
    public class PizzaTopping
    {
        public Topping Topping { get; set; }

        public int ToppingId { get; set; }
        
        public int PizzaId { get; set; }
    }
}
