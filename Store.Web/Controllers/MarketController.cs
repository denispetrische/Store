using Microsoft.AspNetCore.Mvc;

namespace Store.Web.Controllers
{
    public class MarketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
