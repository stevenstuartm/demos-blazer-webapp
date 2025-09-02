namespace demos.blazer.webapp.Features.Pizza.DTOs
{
    public class Topping
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required decimal Price { get; set; }

        public required string Ingredients { get; set; }

        public string GetFormattedPrice() => Price.ToString("0.00");
    }
}
