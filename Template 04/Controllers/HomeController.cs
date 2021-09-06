using Microsoft.AspNetCore.Mvc;

namespace Template_SQLite_AdoNet_Crud_ExtensionMethod.Controllers
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