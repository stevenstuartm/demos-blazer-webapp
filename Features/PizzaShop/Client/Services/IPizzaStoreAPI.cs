using demos.blazer.webapp.Features.PizzaShop.Shared.DTOs.Inputs;
using demos.blazer.webapp.Features.PizzaShop.Shared.DTOs.Outputs;

namespace demos.blazer.webapp.Features.PizzaShop.Client.Services
{
    public interface IPizzaStoreAPI
    {
        Task<List<Order>> GetMyOrders();
        Task<List<PizzaSpecial>> GetPizzaSpecials();
        Task<int> Save(OrderInput order);
    }
}