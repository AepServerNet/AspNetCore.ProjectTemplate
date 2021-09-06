using System.Collections.Generic;
using System.Threading.Tasks;
using Template_SQLite_AdoNet_Crud_Polly.Models.InputModels.Utenti;
using Template_SQLite_AdoNet_Crud_Polly.Models.ViewModels.Utenti;

namespace Template_SQLite_AdoNet_Crud_Polly.Models.Services.Application.Utenti
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