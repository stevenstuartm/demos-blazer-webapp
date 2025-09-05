using System.ComponentModel.DataAnnotations;

namespace demos.blazer.webapp.PizzaShop.Shared.ValidationAttributes
{
    public class PizzaBase : ValidationAttribute
    {
        private string GetErrorMessage() => $"Sorry, that's not a valid pizza base.";
        private static readonly string[] ValidValues = { "Tomato", "Pesto" };

        protected override ValidationResult IsValid(
            object? value, ValidationContext validationContext)
        {
            var valueString = value as string;

            if (!string.IsNullOrWhiteSpace(valueString)
                && ValidValues.Contains(valueString))
            {
                return ValidationResult.Success!;
            }

            return new ValidationResult(GetErrorMessage());
        }
    }
}
