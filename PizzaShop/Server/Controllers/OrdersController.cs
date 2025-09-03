using demos.blazer.webapp.PizzaShop.Server.Repositories;
using demos.blazer.webapp.PizzaShop.Shared.DTOs.Inputs;
using demos.blazer.webapp.PizzaShop.Shared.DTOs.Outputs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demos.blazer.webapp.PizzaShop.Server.Controllers
{
    [Route("api/pizzashop")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly PizzaStoreContext _db;

        public OrdersController(PizzaStoreContext db)
        {
            _db = db;
        }

        [HttpGet("orders")]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            var orders = await _db.Orders
                .Include(o => o.DeliveryAddress)
                .Include(o => o.Pizzas).ThenInclude(p => p.Special)
                .Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
                .OrderByDescending(o => o.CreatedTime)
                .ToListAsync();

            return orders;
        }

        [HttpPost("order")]
        public async Task<ActionResult<int>> PlaceOrder(OrderInput input)
        {
            var order = new Order()
            {
                UserId = "1",
                DeliveryAddress = new Address()
                {
                    City = input.DeliveryAddress.City,
                    Line1 = input.DeliveryAddress.Line1,
                    Name = input.DeliveryAddress.Name,
                    PostalCode = input.DeliveryAddress.PostalCode,
                    Region = input.DeliveryAddress.Region,
                    Line2 = input.DeliveryAddress.Line2
                },
                CreatedTime = DateTime.Now,
                Pizzas = input.Pizzas.Select(p => new OrderPizza()
                {
                   Size = p.Size,
                   SpecialId = p.SpecialId
                   //toppings
                }).ToList()
            };

            _db.Orders.Attach(order);
            await _db.SaveChangesAsync();

            return order.Id;
        }

        [HttpGet("orders/{orderId}")]
        public async Task<ActionResult<Order>> GetOrder(int orderId)
        {
            var order = await _db.Orders
                .Where(o => o.Id == orderId)
                .Include(o => o.DeliveryAddress)
                .Include(o => o.Pizzas).ThenInclude(p => p.Special)
                .Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
                .SingleOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }
    }
}
