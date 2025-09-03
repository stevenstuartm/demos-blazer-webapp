using demos.blazer.webapp.CacheManagement;
using demos.blazer.webapp.Features.PizzaShop.Client.Models;
using demos.blazer.webapp.Features.PizzaShop.Shared;

namespace demos.blazer.webapp.Features.PizzaShop.Client.Services
{
    public class PizzaStoreRepository : IPizzaStoreRepository
    {
        private readonly IPizzaStoreAPI _menuAPI;
        private readonly IGlobalCacheCoordinator _cacheCoordinator;

        public List<PizzaSpecial> Specials { get; private set; } = new List<PizzaSpecial>();

        public PizzaStoreRepository(IPizzaStoreAPI menuAPI, IGlobalCacheCoordinator cacheCoordinator)
        {
            _menuAPI = menuAPI;
            _cacheCoordinator = cacheCoordinator;

            // Subscribe to cache refresh events
            _cacheCoordinator.CacheRefreshed += OnCacheRefreshed;
        }

        public async Task Init()
        {
            // Get cached specials (will be populated by coordinator)
            Specials = await PizzaSpecialsCache.GetSpecialsAsync(_menuAPI);
        }

        public async Task<List<OrderWithStatus>> GetMyOrders()
        {
            // Ensure initialization
            await Init();

            var response = await _menuAPI.GetMyOrders();
            return response.Select(o => ToOrderWithStatusModel(o)).ToList();
        }

        public async Task<int> Save(Models.Order order)
        {
            var input = ToDTO(order);
            return await _menuAPI.Save(input);
        }

        private async void OnCacheRefreshed(object? sender, CacheRefreshedEventArgs e)
        {
            if (e.CacheKey == "PizzaSpecials")
            {
                // Refresh local specials when cache is updated
                Specials = await PizzaSpecialsCache.GetSpecialsAsync(_menuAPI);
            }
        }

        public readonly static TimeSpan PreparationDuration = TimeSpan.FromSeconds(10);
        public readonly static TimeSpan DeliveryDuration = TimeSpan.FromMinutes(1);

        private OrderWithStatus ToOrderWithStatusModel(Shared.DTOs.Outputs.Order order)
        {
            string statusText;
            var dispatchTime = order.CreatedTime.Add(PreparationDuration);

            if (DateTime.Now < dispatchTime)
            {
                statusText = OrderStatuses.Preparing;
            }
            else if (DateTime.Now < dispatchTime + DeliveryDuration)
            {
                statusText = OrderStatuses.OutForDelivery;
            }
            else
            {
                statusText = OrderStatuses.Delivered;
            }

            return new OrderWithStatus
            {
                Order = ToModel(order),
                StatusText = statusText
            };
        }

        private Order ToModel(Shared.DTOs.Outputs.Order order)
        {
            return new Order()
            {
                DeliveryAddress = new Address()
                {
                    City = order.DeliveryAddress?.City,
                    Line1 = order.DeliveryAddress?.Line1,
                    Line2 = order.DeliveryAddress?.Line2,
                    Name = order.DeliveryAddress?.Name,
                    PostalCode = order.DeliveryAddress?.PostalCode,
                    Region = order.DeliveryAddress?.Region
                },
                CreatedTime = order.CreatedTime,
                Pizzas = order.Pizzas.Select(p => new Pizza()
                {
                    Size = p.Size,
                    ToppingIds = p.Toppings?.Select(t => t.ToppingId).ToList(),
                    Special = Specials.Single(s => s.Id == p.SpecialId)
                }).ToList()
            };
        }

        private Shared.DTOs.Inputs.OrderInput ToDTO(Order model)
        {
            return new Shared.DTOs.Inputs.OrderInput()
            {
                Pizzas = model.Pizzas.Select(p => new Shared.DTOs.Inputs.PizzaInput()
                {
                    Size = p.Size,
                    SpecialId = p.Special.Id,
                    ToppingIds = p.ToppingIds
                }).ToList()
            };
        }
    }
}
