using demos.blazer.webappPizzaShop.Shared.DTOs.Inputs;
using demos.blazer.webappPizzaShop.Shared.DTOs.Outputs;

namespace demos.blazer.webappPizzaShop.Client.Services
{
    public interface IPizzaStoreAPI
    {
        Task<List<Order>> GetMyOrders();
        Task<List<PizzaSpecial>> GetPizzaSpecials();
        Task<int> Save(OrderInput order);
        Task<Order?> GetOrder(int id);
    }
}