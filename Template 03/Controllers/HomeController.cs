using Microsoft.AspNetCore.Mvc;

namespace Template_SQLite_AdoNet_Crud.Controllers
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