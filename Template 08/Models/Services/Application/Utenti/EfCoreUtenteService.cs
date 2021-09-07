using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template_SQLite_EfCore.Models.Entities;
using Template_SQLite_EfCore.Models.Exceptions.Application;
using Template_SQLite_EfCore.Models.InputModels.Utenti;
using Template_SQLite_EfCore.Models.Services.Infrastructure;
using Template_SQLite_EfCore.Models.ViewModels.Utenti;

namespace Template_SQLite_EfCore.Models.Services.Application.Utenti
{
    public class EfCoreUtenteService : IUtenteService
    {
        private readonly ILogger<EfCoreUtenteService> logger;
        private readonly MySQLiteEfCoreDbContext dbContext;
        public EfCoreUtenteService(ILogger<EfCoreUtenteService> logger, MySQLiteEfCoreDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<UtenteDetailViewModel> CreateUtenteAsync(UtenteCreateInputModel inputModel)
        {
            string cognome = inputModel.Cognome;
            string nome = inputModel.Nome;
            string email = inputModel.Email;
            string telefono = inputModel.Telefono;

            var utente = new Utente(cognome, nome, email, telefono);
            dbContext.Add(utente);
            
            await dbContext.SaveChangesAsync();
            return UtenteDetailViewModel.FromEntity(utente);
        }

        public async Task DeleteUtenteAsync(UtenteDeleteInputModel inputModel)
        {
            Utente utente = await dbContext.Utenti.FindAsync(inputModel.Id);

            if (utente == null)
            {
                throw new UtenteNotFoundException(inputModel.Id);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<UtenteDetailViewModel> EditUtenteAsync(UtenteEditInputModel inputModel)
        {
            Utente utente = await dbContext.Utenti.FindAsync(inputModel.Id);

            if (utente == null)
            {
                throw new UtenteNotFoundException(inputModel.Id);
            }

            utente.ChangeCognome(inputModel.Cognome);
            utente.ChangeNome(inputModel.Nome);
            utente.ChangeEmail(inputModel.Email);
            utente.ChangeTelefono(inputModel.Telefono);

            await dbContext.SaveChangesAsync();
            return UtenteDetailViewModel.FromEntity(utente);
        }

        public async Task<UtenteDetailViewModel> GetUtenteAsync(int id)
        {
            IQueryable<UtenteDetailViewModel> queryLinq = dbContext.Utenti
                .AsNoTracking()
                .Include(utente => utente.Profili)
                .Where(utente => utente.Id == id)
                .Select(utente => UtenteDetailViewModel.FromEntity(utente));

            UtenteDetailViewModel viewModel = await queryLinq.FirstOrDefaultAsync();
            //.FirstOrDefaultAsync(); //Restituisce null se l'elenco è vuoto e non solleva mai un'eccezione
            //.SingleOrDefaultAsync(); //Tollera il fatto che l'elenco sia vuoto e in quel caso restituisce null, oppure se l'elenco contiene più di 1 elemento, solleva un'eccezione
            //.FirstAsync(); //Restituisce il primo elemento, ma se l'elenco è vuoto solleva un'eccezione
            //.SingleAsync(); //Restituisce il primo elemento, ma se l'elenco è vuoto o contiene più di un elemento, solleva un'eccezione

            if (viewModel == null)
            {
                logger.LogWarning("Utente {id} not found", id);
                throw new UtenteNotFoundException(id);
            }

            return viewModel;
        }

        public async Task<UtenteEditInputModel> GetUtenteForEditingAsync(int id)
        {
            IQueryable<UtenteEditInputModel> queryLinq = dbContext.Utenti
                .AsNoTracking()
                .Where(utente => utente.Id == id)
                .Select(utente => UtenteEditInputModel.FromEntity(utente));

            UtenteEditInputModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Utente {id} not found", id);
                throw new UtenteNotFoundException(id);
            }

            return viewModel;
        }

        public async Task<List<UtenteViewModel>> GetUtentiAsync()
        {
            IQueryable<UtenteViewModel> queryLinq = dbContext.Utenti
                .AsNoTracking()
                .Select(utente => UtenteViewModel.FromEntity(utente));

            List<UtenteViewModel> utenti = await queryLinq.ToListAsync();

            return utenti;
        }
    }
}