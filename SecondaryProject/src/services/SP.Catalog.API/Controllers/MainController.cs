using Microsoft.AspNetCore.Mvc;

namespace SP.Catalog.API.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
