using demos.blazer.webappPizzaShop.Server.Repositories;
using demos.blazer.webappPizzaShop.Shared.DTOs.Outputs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demos.blazer.webappPizzaShop.Server.Controllers
{
    [Route("api/pizzashop")]
    [ApiController]
    public class MenuController : Controller
    {
        private readonly PizzaStoreContext _db;

        public MenuController(PizzaStoreContext db)
        {
            _db = db;
        }

        [HttpGet("specials")]
        public async Task<List<PizzaSpecial>> GetSpecials()
        {
            return (await _db.Specials.ToListAsync()).OrderByDescending(s => s.BasePrice).ToList();
        }
    }
}
