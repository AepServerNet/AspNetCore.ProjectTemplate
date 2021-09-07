using Microsoft.AspNetCore.Mvc;

namespace Template_SQLite_EfCore.Controllers
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