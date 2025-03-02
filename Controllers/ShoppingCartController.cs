using Microsoft.AspNetCore.Mvc;

namespace Webbanson.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
