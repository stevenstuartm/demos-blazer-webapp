using demos.blazer.webapp.Features.Pizza.DTOs;

namespace demos.blazer.webapp.Features.Pizza.Services
{
    public interface IPizzaService
    {
        Task<List<PizzaSpecial>> GetPizzaSpecialAsync();
    }
}