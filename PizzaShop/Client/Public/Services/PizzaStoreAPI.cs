using demos.blazer.webapp.Global.Configuration;
using demos.blazer.webapp.PizzaShop.Shared.DTOs.Outputs;
using HttpClient = demos.blazer.webapp.PizzaShop.Shared.HttpClient;

namespace demos.blazer.webapp.PizzaShop.Client.Public.Services
{
    public class PizzaStoreAPI : IPizzaStoreAPI
    {
        private readonly HttpClient _httpClient;

        public PizzaStoreAPI(IApiConfiguration apiConfig)
        {
            var baseUri = apiConfig.GetApiBaseUrl();
            _httpClient = new HttpClient(baseUri);
        }

        public async Task<List<PizzaSpecial>> GetPizzaSpecials()
        {
            const string path = "specials";
            var response = await _httpClient.Get<List<PizzaSpecial>>(path);
            return response ?? new List<PizzaSpecial>();
        }

        public async Task<List<Order>> GetMyOrders()
        {
            const string path = "orders";
            var response = await _httpClient.Get<List<Order>>(path);
            return response ?? new List<Order>();
        }

        public async Task<int> Save(Shared.DTOs.Inputs.OrderInput order)
        {
            const string path = "order";
            var response = await _httpClient.Post<int, Shared.DTOs.Inputs.OrderInput>(path, order);
            return response;
        }

        public async Task<Order?> GetOrder(int id)
        {
            var response = await _httpClient.Get<Order>($"orders/{id}");
            return response;
        }
    }
}