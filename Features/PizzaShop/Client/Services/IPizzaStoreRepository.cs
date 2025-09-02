using demos.blazer.webapp.Features.PizzaShop.Client.Models;

namespace demos.blazer.webapp.Features.PizzaShop.Client.Services
{
    public interface IPizzaStoreRepository
    {
        List<PizzaSpecial> Specials { get; }

        Task<List<OrderWithStatus>> GetMyOrders();
        Task Init();
        Task<int> Save(Order order);
    }
}