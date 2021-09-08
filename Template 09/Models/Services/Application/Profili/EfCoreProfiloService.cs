using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template_SQLite_EfCore.Models.Entities;
using Template_SQLite_EfCore.Models.Exceptions.Application;
using Template_SQLite_EfCore.Models.InputModels.Profili;
using Template_SQLite_EfCore.Models.Services.Infrastructure;
using Template_SQLite_EfCore.Models.ViewModels.Profili;

namespace Template_SQLite_EfCore.Models.Services.Application.Profili
{
    public class EfCoreProfiloService : IProfiloService
    {
        private readonly ILogger<EfCoreProfiloService> logger;
        private readonly MySQLiteEfCoreDbContext dbContext;
        public EfCoreProfiloService(ILogger<EfCoreProfiloService> logger, MySQLiteEfCoreDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<ProfiloDetailViewModel> CreateProfiloAsync(ProfiloCreateInputModel inputModel)
        {
            var profilo = new Profilo(inputModel.UtenteId, inputModel.Username, inputModel.Password);
            dbContext.Add(profilo);
            await dbContext.SaveChangesAsync();

            return ProfiloDetailViewModel.FromEntity(profilo);
        }

        public async Task DeleteProfiloAsync(ProfiloDeleteInputModel inputModel)
        {
            Profilo profilo = await dbContext.Profili.FindAsync(inputModel.Id);
            if (profilo == null)
            {
                throw new ProfiloNotFoundException(inputModel.Id);
            }
            dbContext.Remove(profilo);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ProfiloDetailViewModel> EditProfiloAsync(ProfiloEditInputModel inputModel)
        {
            Profilo profilo = await dbContext.Profili.FindAsync(inputModel.Id);
            
            if (profilo == null)
            {
                throw new ProfiloNotFoundException(inputModel.Id);
            }

            profilo.ChangeUsername(inputModel.Username);
            profilo.ChangePassword(inputModel.Password);

            await dbContext.SaveChangesAsync();
            return ProfiloDetailViewModel.FromEntity(profilo);
        }

        public async Task<ProfiloDetailViewModel> GetProfiloAsync(int id)
        {
           IQueryable<ProfiloDetailViewModel> queryLinq = dbContext.Profili
                .AsNoTracking()
                .Where(profilo => profilo.Id == id)
                .Select(profilo => ProfiloDetailViewModel.FromEntity(profilo));

            ProfiloDetailViewModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Profilo {id} not found", id);
                throw new ProfiloNotFoundException(id);
            }

            return viewModel;
        }

        public async Task<ProfiloEditInputModel> GetProfiloForEditingAsync(int id)
        {
            IQueryable<ProfiloEditInputModel> queryLinq = dbContext.Profili
                .AsNoTracking()
                .Where(profilo => profilo.Id == id)
                .Select(profilo => ProfiloEditInputModel.FromEntity(profilo));

            ProfiloEditInputModel inputModel = await queryLinq.FirstOrDefaultAsync();

            if (inputModel == null)
            {
                logger.LogWarning("Profilo {id} not found", id);
                throw new ProfiloNotFoundException(id);
            }

            return inputModel;
        }
    }
}