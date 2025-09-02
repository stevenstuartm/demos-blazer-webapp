using demos.blazer.webapp.Features.PizzaShop.Client.Models;
using demos.blazer.webapp.Features.PizzaShop.Shared;

namespace demos.blazer.webapp.Features.PizzaShop.Client.Services
{
    public class PizzaStoreRepository : IPizzaStoreRepository
    {
        private readonly IPizzaStoreAPI _menuAPI;

        public List<PizzaSpecial> Specials { get; private set; }

        public PizzaStoreRepository(IPizzaStoreAPI menuAPI)
        {
            _menuAPI = menuAPI;
        }

        public async Task Init()
        {
            var response = await _menuAPI.GetPizzaSpecials();

            if (response != null)
            {
                Specials = response.Select(o => ToModel(o)).ToList();
            }
        }

        public async Task<List<OrderWithStatus>> GetMyOrders()
        {
            var response = await _menuAPI.GetMyOrders();

            return response.Select(o => ToOrderWithStatusModel(o)).ToList();
        }

        public async Task<int> Save(Models.Order order)
        {
            var input = ToDTO(order);

            var response = await _menuAPI.Save(input);

            return response;
        }

        public readonly static TimeSpan PreparationDuration = TimeSpan.FromSeconds(10);
        public readonly static TimeSpan DeliveryDuration = TimeSpan.FromMinutes(1); // Unrealistic, but more interesting to watch

        private OrderWithStatus ToOrderWithStatusModel(Shared.DTOs.Outputs.Order order)
        {
            // To simulate a real backend process, we fake status updates based on the amount
            // of time since the order was placed
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
                    City = order.DeliveryAddress.City,
                    Line1 = order.DeliveryAddress.Line1,
                    Line2 = order.DeliveryAddress.Line2,
                    Name = order.DeliveryAddress.Name,
                    PostalCode = order.DeliveryAddress.PostalCode,
                    Region = order.DeliveryAddress.Region
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
        private PizzaSpecial ToModel(Shared.DTOs.Outputs.PizzaSpecial special)
        {
            return new PizzaSpecial()
            {
                BasePrice = special.BasePrice,
                Description = special.Description,
                Id = special.Id,
                ImageUrl = special.ImageUrl,
                Name = special.Name
            };
        }
    }
}
