using Microsoft.AspNetCore.Mvc;

namespace Template_Bootstrap.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Benvenuto!";
            return View();
        }
    }
}