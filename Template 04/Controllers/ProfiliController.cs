using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Template_SQLite_AdoNet_Crud_ExtensionMethod.Models.InputModels.Profili;
using Template_SQLite_AdoNet_Crud_ExtensionMethod.Models.Services.Application.Profili;
using Template_SQLite_AdoNet_Crud_ExtensionMethod.Models.ViewModels.Profili;

namespace Template_SQLite_AdoNet_Crud_ExtensionMethod.Controllers
{
    public class ProfiliController : Controller
    {
        private readonly IProfiloService profiloService;
        public ProfiliController(IProfiloService profiloService)
        {
            this.profiloService = profiloService;
        }

        public async Task<IActionResult> Detail(int id)
        {
            ProfiloDetailViewModel viewModel = await profiloService.GetProfiloAsync(id);
            ViewData["Title"] = "Dettaglio profilo";
            return View(viewModel);
        }

        public IActionResult Create(int id)
        {
            ViewData["Title"] = "Nuovo profilo";
            var inputModel = new ProfiloCreateInputModel();
            inputModel.UtenteId = id;
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProfiloCreateInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                ProfiloDetailViewModel profilo = await profiloService.CreateProfiloAsync(inputModel);
                TempData["ConfirmationMessage"] = "Ok! Il profilo è stato creato";
                return RedirectToAction(nameof(UtentiController.Detail), "Utenti", new { id = inputModel.UtenteId });
            }
            
            ViewData["Title"] = "Nuovo profilo";
            return View(inputModel);
            
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Modifica profilo";
            ProfiloEditInputModel inputModel = await profiloService.GetProfiloForEditingAsync(id);
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfiloEditInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                ProfiloDetailViewModel viewModel = await profiloService.EditProfiloAsync(inputModel);
                TempData["ConfirmationMessage"] = "I dati sono stati salvati con successo";
                return RedirectToAction(nameof(UtentiController.Index), "Utenti");
            }

            ViewData["Title"] = "Modifica profilo";
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProfiloDeleteInputModel inputModel)
        {
            await profiloService.DeleteProfiloAsync(inputModel);
            TempData["ConfirmationMessage"] = "Il profilo è stato eliminato";
            return RedirectToAction(nameof(UtentiController.Detail), "Utenti", new { id = inputModel.UtenteId });
        }
    }
}