using demos.blazer.webapp.PizzaShop.Shared.DTOs.Inputs;
using demos.blazer.webapp.PizzaShop.Shared.DTOs.Outputs;

namespace demos.blazer.webapp.PizzaShop.Client.Public.Services
{
    public interface IPizzaStoreAPI
    {
        Task<List<Order>> GetMyOrders();
        Task<List<PizzaSpecial>> GetPizzaSpecials();
        Task<int> Save(OrderInput order);
        Task<Order?> GetOrder(int id);
    }
}