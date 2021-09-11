using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.Exceptions.Application;

namespace Template_SQLite_AdoNet_Crud_AutoMapper.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            switch(feature.Error)
            {
                case UtenteNotFoundException exc:
                    ViewData["Title"] = "Utente non trovato";
                    Response.StatusCode = 404;
                    return View("UtenteNotFound");
                
                case ProfiloNotFoundException exc:
                    ViewData["Title"] = "Profilo non trovato";
                    Response.StatusCode = 404;
                    return View("ProfiloNotFound");

                default:
                    ViewData["Title"] = "Errore";
                    return View();
            }
        }
    }
}