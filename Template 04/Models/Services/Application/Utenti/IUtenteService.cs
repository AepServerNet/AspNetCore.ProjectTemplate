using System.Collections.Generic;
using System.Threading.Tasks;
using Template_SQLite_AdoNet_Crud_ExtensionMethod.Models.InputModels.Utenti;
using Template_SQLite_AdoNet_Crud_ExtensionMethod.Models.ViewModels.Utenti;

namespace Template_SQLite_AdoNet_Crud_ExtensionMethod.Models.Services.Application.Utenti
{
    public interface IUtenteService
    {
        Task<List<UtenteViewModel>> GetUtentiAsync();
        Task<int> CreateUtenteAsync(UtenteCreateInputModel inputModel);
        Task<UtenteEditInputModel> GetUtenteForEditingAsync(int id);
        Task<UtenteDetailViewModel> EditUtenteAsync(UtenteEditInputModel inputModel);
        Task<UtenteDetailViewModel> GetUtenteAsync(int id);
        Task DeleteUtenteAsync(UtenteDeleteInputModel inputModel);
    }
}