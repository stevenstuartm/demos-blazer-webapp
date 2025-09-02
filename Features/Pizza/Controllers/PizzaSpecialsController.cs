using demos.blazer.webapp.Features.Pizza.DTOs;
using demos.blazer.webapp.Features.Pizza.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demos.blazer.webapp.Features.Pizza.Controllers
{
    [Route("api/pizzas")]
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
