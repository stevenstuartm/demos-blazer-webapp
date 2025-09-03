using demos.blazer.webapp.PizzaShop.Server.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace demos.blazer.webapp.PizzaShop.Server.Controllers
{
    [Route("api/pizzashop/admin")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly PizzaStoreContext _db;

        public AdminController(PizzaStoreContext db)
        {
            _db = db;
        }


    }
}
