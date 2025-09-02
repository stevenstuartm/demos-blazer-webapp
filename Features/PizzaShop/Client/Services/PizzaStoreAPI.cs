using Microsoft.AspNetCore.Components;
using HttpClient = demos.blazer.webapp.Features.PizzaShop.Shared.HttpClient;

namespace demos.blazer.webapp.Features.PizzaShop.Client.Services
{
    public class PizzaStoreAPI : IPizzaStoreAPI
    {
        private readonly NavigationManager _navigationManager;
        private readonly HttpClient _httpClient;

        public PizzaStoreAPI(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _httpClient = new HttpClient($"{_navigationManager.BaseUri}api/pizzashop/");
        }

        public async Task<List<Shared.DTOs.Outputs.PizzaSpecial>> GetPizzaSpecials()
        {
            const string path = "specials";

            var response = await _httpClient.Get<List<Shared.DTOs.Outputs.PizzaSpecial>>(path);

            return response;
        }

        public async Task<List<Shared.DTOs.Outputs.Order>> GetMyOrders()
        {
            const string path = "orders";

            var response = await _httpClient.Get<List<Shared.DTOs.Outputs.Order>>(path);

            return response;
        }

        public async Task<int> Save(Shared.DTOs.Inputs.OrderInput order)
        {
            const string path = "order";

            var response = await _httpClient.Post<int, Shared.DTOs.Inputs.OrderInput>(path, order);

            return response;
        }
    }
}
