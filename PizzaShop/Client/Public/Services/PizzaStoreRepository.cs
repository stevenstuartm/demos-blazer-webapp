using demos.blazer.webapp.CacheManagement;
using demos.blazer.webapp.PizzaShop.Client.Public.Models;
using demos.blazer.webapp.PizzaShop.Shared;

namespace demos.blazer.webapp.PizzaShop.Client.Public.Services
{
    public class PizzaStoreRepository : IPizzaStoreRepository
    {
        private const string MenuCacheKey = "PizzaSpecials";
        private readonly IPizzaStoreAPI _menuAPI;
        private readonly IGlobalCacheCoordinator _cacheCoordinator;

        public List<PizzaSpecial> Specials { get; private set; } = new List<PizzaSpecial>();

        public PizzaStoreRepository(IPizzaStoreAPI menuAPI, IGlobalCacheCoordinator cacheCoordinator)
        {
            _menuAPI = menuAPI;
            _cacheCoordinator = cacheCoordinator;
            _cacheCoordinator.CacheRefreshed += OnCacheRefreshed;
        }

        public async Task Init()
        {
            if (Specials == null || !Specials.Any())
            {
                Specials = await PizzaSpecialsCache.GetSpecialsAsync(_menuAPI);
            }
        }

        public async Task<List<Order>> GetOrders()
        {
            var response = await _menuAPI.GetMyOrders();
            return response.Select(o => ToModel(o)).ToList();
        }

        public async Task<int> Save(Order order)
        {
            var input = ToDTO(order);
            return await _menuAPI.Save(input);
        }

        public async Task<Order?> GetOrder(int id)
        {
            var response = await _menuAPI.GetOrder(id);

            if (response == null) return null;

            var model = ToModel(response);
            return model;
        }

        private async void OnCacheRefreshed(object? sender, CacheRefreshedEventArgs e)
        {
            if (e.CacheKey == MenuCacheKey)
            {
                Specials = await PizzaSpecialsCache.GetSpecialsAsync(_menuAPI);
            }
        }

        public readonly static TimeSpan PreparationDuration = TimeSpan.FromSeconds(10);
        public readonly static TimeSpan DeliveryDuration = TimeSpan.FromMinutes(1);

        private Order ToModel(Shared.DTOs.Outputs.Order order)
        {
            return new Order()
            {
                Id = order.Id,
                StatusText = GetOrderStatus(order),
                DeliveryAddress = new Address()
                {
                    City = order.DeliveryAddress.City,
                    Line1 = order.DeliveryAddress.Line1,
                    Line2 = order.DeliveryAddress.Line2,
                    Name = order.DeliveryAddress.Name,
                    PostalCode = order.DeliveryAddress.PostalCode,
                    Region = order.DeliveryAddress.Region
                },
                CreatedTime = order.CreatedTime,
                Pizzas = order.Pizzas.Select(p => new OrderPizza()
                {
                    Size = p.Size,
                    ToppingIds = p.Toppings?.Select(t => t.ToppingId).ToList(),
                    Special = Specials.Single(s => s.Id == p.SpecialId)
                }).ToList()
            };
        }

        private static string GetOrderStatus(Shared.DTOs.Outputs.Order order)
        {
            var dispatchTime = order.CreatedTime.Add(PreparationDuration);

            if (DateTime.Now < dispatchTime)
            {
                return OrderStatuses.Preparing;
            }
            else if (DateTime.Now < dispatchTime + DeliveryDuration)
            {
                return OrderStatuses.OutForDelivery;
            }
            else
            {
                return OrderStatuses.Delivered;
            }
        }

        private Shared.DTOs.Inputs.OrderInput ToDTO(Order model)
        {
            return new Shared.DTOs.Inputs.OrderInput()
            {
                DeliveryAddress = new Shared.DTOs.Inputs.AddressInput()
                {
                    City = model.DeliveryAddress.City,
                    Line1 = model.DeliveryAddress.Line1,
                    Line2 = model.DeliveryAddress.Line2,
                    Name = model.DeliveryAddress.Name,
                    PostalCode = model.DeliveryAddress.PostalCode,
                    Region = model.DeliveryAddress.Region
                },
                Pizzas = model.Pizzas.Select(p => new Shared.DTOs.Inputs.OrderPizzaInput()
                {
                    Size = p.Size,
                    SpecialId = p.Special.Id,
                    ToppingIds = p.ToppingIds
                }).ToList()
            };
        }
    }
}
