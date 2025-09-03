using demos.blazer.webapp.Features.PizzaShop.Server.Repositories;
using demos.blazer.webapp.Features.PizzaShop.Shared.DTOs.Inputs;
using demos.blazer.webapp.Features.PizzaShop.Shared.DTOs.Outputs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demos.blazer.webapp.Features.PizzaShop.Server.Controllers
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
                    City = "somehwere",
                    Line1 = "line fdsf",
                    Name = "sfdf",
                    PostalCode = "34142",
                    Region = "FL"
                },
                CreatedTime = DateTime.Now,
                Pizzas = input.Pizzas.Select(p => new Pizza()
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
    }
}
