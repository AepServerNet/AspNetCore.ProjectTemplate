using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Template_SQLite_AdoNet_Crud_Polly.Models.InputModels.Utenti;
using Template_SQLite_AdoNet_Crud_Polly.Models.Services.Application.Utenti;
using Template_SQLite_AdoNet_Crud_Polly.Models.ViewModels.Utenti;

namespace Template_SQLite_AdoNet_Crud_Polly.Controllers
{
    public class UtentiController : Controller
    {
        private readonly IUtenteService utenteService;
        public UtentiController(IUtenteService utenteService)
        {
            this.utenteService = utenteService;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Elenco utenti";
            List<UtenteViewModel> utenti = await utenteService.GetUtentiAsync();
            return View(utenti);
        }

        public async Task<IActionResult> Detail(int id)
        {
            UtenteDetailViewModel viewModel = await utenteService.GetUtenteAsync(id);
            ViewData["Title"] = "Dettaglio utente";
            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Nuovo utente";
            var inputModel = new UtenteCreateInputModel();
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UtenteCreateInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                    int utente = await utenteService.CreateUtenteAsync(inputModel);

                    if (utente != 0)
                    {
                       TempData["ConfirmationMessage"] = "Ok! Il nuovo utente è stato creato";
                       return RedirectToAction(nameof(Index));
                    }
            }
            ViewData["Title"] = "Nuovo utente";
            return View(inputModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Modifica utente";
            UtenteEditInputModel inputModel = await utenteService.GetUtenteForEditingAsync(id);
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UtenteEditInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                UtenteDetailViewModel utente = await utenteService.EditUtenteAsync(inputModel);
                TempData["ConfirmationMessage"] = "I dati sono stati salvati con successo";
                return RedirectToAction(nameof(Detail), new { id = inputModel.Id });
            }

            ViewData["Title"] = "Modifica utente";
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UtenteDeleteInputModel inputModel)
        {
            await utenteService.DeleteUtenteAsync(inputModel);
            TempData["ConfirmationMessage"] = "L'utente è stato eliminato.";
            return RedirectToAction(nameof(Index));
        }
    }
}