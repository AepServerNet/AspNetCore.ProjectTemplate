using System.Collections.Generic;
using System.Threading.Tasks;
using Template_SQLite_AdoNet_Crud.Models.InputModels.Utenti;
using Template_SQLite_AdoNet_Crud.Models.ViewModels.Utenti;

namespace Template_SQLite_AdoNet_Crud.Models.Services.Application.Utenti
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