using demos.blazer.webapp.Configuration;
using HttpClient = demos.blazer.webapp.Features.PizzaShop.Shared.HttpClient;

namespace demos.blazer.webapp.Features.PizzaShop.Client.Services
{
    public class PizzaStoreAPI : IPizzaStoreAPI
    {
        private readonly HttpClient _httpClient;

        public PizzaStoreAPI(IApiConfiguration apiConfig)
        {
            var baseUri = apiConfig.GetApiBaseUrl();
            _httpClient = new HttpClient(baseUri);
        }

        public async Task<List<Shared.DTOs.Outputs.PizzaSpecial>> GetPizzaSpecials()
        {
            const string path = "specials";
            var response = await _httpClient.Get<List<Shared.DTOs.Outputs.PizzaSpecial>>(path);
            return response ?? new List<Shared.DTOs.Outputs.PizzaSpecial>();
        }

        public async Task<List<Shared.DTOs.Outputs.Order>> GetMyOrders()
        {
            const string path = "orders";
            var response = await _httpClient.Get<List<Shared.DTOs.Outputs.Order>>(path);
            return response ?? new List<Shared.DTOs.Outputs.Order>();
        }

        public async Task<int> Save(Shared.DTOs.Inputs.OrderInput order)
        {
            const string path = "order";
            var response = await _httpClient.Post<int, Shared.DTOs.Inputs.OrderInput>(path, order);
            return response;
        }
    }
}