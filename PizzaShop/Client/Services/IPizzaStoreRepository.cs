using demos.blazer.webappPizzaShop.Client.Models;

namespace demos.blazer.webappPizzaShop.Client.Services
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