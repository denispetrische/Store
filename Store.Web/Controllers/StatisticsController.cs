using Microsoft.AspNetCore.Mvc;

namespace Store.Web.Controllers
{
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
