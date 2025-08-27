
namespace demos.blazer.webapp.Features.Pizza.DTOs
{
    public class Address
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Line1 { get; set; }

        public required string? Line2 { get; set; }

        public required string City { get; set; }

        public required string Region { get; set; }

        public required string PostalCode { get; set; }
    }
}
