using demos.blazer.webappPizzaShop.Client.Models;

namespace demos.blazer.webappPizzaShop.Client.Services
{
    public interface IPizzaStoreRepository
    {
        List<PizzaSpecial> Specials { get; }

        Task<List<OrderWithStatus>> GetMyOrders();
        Task Init();
        Task<int> Save(Order order);
    }
}