using System.Collections.Generic;
using System.Threading.Tasks;
using Template_SQLite_AdoNet_Crud.Models.InputModels.Profili;
using Template_SQLite_AdoNet_Crud.Models.ViewModels.Profili;

namespace Template_SQLite_AdoNet_Crud.Models.Services.Application.Profili
{
    public interface IProfiloService
    {
        Task<ProfiloDetailViewModel> GetProfiloAsync(int id);
        Task<ProfiloEditInputModel> GetProfiloForEditingAsync(int id);
        Task<ProfiloDetailViewModel> CreateProfiloAsync(ProfiloCreateInputModel inputModel);
        Task<ProfiloDetailViewModel> EditProfiloAsync(ProfiloEditInputModel inputModel);
        Task DeleteProfiloAsync(ProfiloDeleteInputModel id);
    }
}