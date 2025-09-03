using System.ComponentModel.DataAnnotations;

namespace demos.blazer.webapp.PizzaShop.Shared.DTOs.Inputs
{
    public class AddressInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Line1 { get; set; }

        public string? Line2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string PostalCode { get; set; }
    }
}
