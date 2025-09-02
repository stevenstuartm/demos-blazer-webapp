using demos.blazer.webapp.Features.PizzaShop.DTOs;
using demos.blazer.webapp.Features.PizzaShop.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demos.blazer.webapp.Features.PizzaShop.Controllers
{
    [Route("api/pizzashop")]
    [ApiController]
    public class SpecialsController : Controller
    {
        private readonly PizzaStoreContext _db;

        public SpecialsController(PizzaStoreContext db)
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
