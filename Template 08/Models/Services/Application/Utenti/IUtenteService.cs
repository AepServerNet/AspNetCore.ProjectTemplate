using System.Collections.Generic;
using System.Threading.Tasks;
using Template_SQLite_EfCore.Models.InputModels.Utenti;
using Template_SQLite_EfCore.Models.ViewModels.Utenti;

namespace Template_SQLite_EfCore.Models.Services.Application.Utenti
{
    public interface IUtenteService
    {
        Task<List<UtenteViewModel>> GetUtentiAsync();
        Task<UtenteDetailViewModel> CreateUtenteAsync(UtenteCreateInputModel inputModel);
        Task<UtenteEditInputModel> GetUtenteForEditingAsync(int id);
        Task<UtenteDetailViewModel> EditUtenteAsync(UtenteEditInputModel inputModel);
        Task<UtenteDetailViewModel> GetUtenteAsync(int id);
        Task DeleteUtenteAsync(UtenteDeleteInputModel inputModel);
    }

}