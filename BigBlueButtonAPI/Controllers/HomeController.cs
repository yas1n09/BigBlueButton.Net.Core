using Microsoft.AspNetCore.Mvc;

namespace BigBlueButtonAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
