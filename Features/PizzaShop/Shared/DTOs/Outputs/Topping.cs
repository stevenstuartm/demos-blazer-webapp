﻿namespace demos.blazer.webapp.Features.PizzaShop.Shared.DTOs.Outputs
{
    public class Topping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Ingredients { get; set; }

        public string GetFormattedPrice() => Price.ToString("0.00");
    }
}
