using demos.blazer.webapp.PizzaShop.Client.Public.Models;

namespace demos.blazer.webapp.PizzaShop.Client.Public.Services
{
    public interface IPizzaStoreRepository
    {
        List<PizzaSpecial> Specials { get; }
        Task<List<Order>> GetOrders();
        Task Init();
        Task<int> Save(Order order);
        Task<Order?> GetOrder(int id);
    }
}