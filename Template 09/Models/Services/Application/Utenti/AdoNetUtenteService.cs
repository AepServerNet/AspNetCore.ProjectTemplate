using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Template_SQLite_EfCore.Models.Exceptions.Application;
using Template_SQLite_EfCore.Models.Extensions;
using Template_SQLite_EfCore.Models.InputModels.Utenti;
using Template_SQLite_EfCore.Models.Services.Infrastructure;
using Template_SQLite_EfCore.Models.ViewModels.Profili;
using Template_SQLite_EfCore.Models.ViewModels.Utenti;

namespace Template_SQLite_EfCore.Models.Services.Application.Utenti
{
    public class AdoNetUtenteService : IUtenteService
    {
        private readonly ILogger<AdoNetUtenteService> logger;
        private readonly IDatabaseAccessor db;
        public AdoNetUtenteService(ILogger<AdoNetUtenteService> logger, IDatabaseAccessor db)
        {
            this.logger = logger;
            this.db = db;
        }

        public async Task<UtenteDetailViewModel> GetUtenteAsync(int id)
        {
            logger.LogInformation("Utente {id} requested", id);

            FormattableString query = $@"SELECT Id, Cognome, Nome, Email, Telefono FROM Utenti WHERE Id={id}; 
            SELECT Id, Username, Password FROM Profili WHERE UtenteId={id} ORDER BY Id";

            DataSet dataSet = await db.QueryAsync(query);

            //Utente
            var utenteTable = dataSet.Tables[0];
            if (utenteTable.Rows.Count != 1)
            {
                logger.LogWarning("Utente {id} not found", id);
                throw new UtenteNotFoundException(id);
            }
            var utenteRow = utenteTable.Rows[0];
            var utenteDetailViewModel = utenteRow.ToUtenteDetailViewModel();

            //Utente profilo
            var profiloDataTable = dataSet.Tables[1];

            foreach (DataRow profiloRow in profiloDataTable.Rows)
            {
                ProfiloViewModel profiloViewModel = profiloRow.ToProfiloViewModel();
                utenteDetailViewModel.Profili.Add(profiloViewModel);
            }
            return utenteDetailViewModel;
        }

        public async Task<List<UtenteViewModel>> GetUtentiAsync()
        {
            FormattableString query = $"SELECT Id, Cognome, Nome, Email, Telefono FROM Utenti";
            DataSet dataSet = await db.QueryAsync(query);
            var dataTable = dataSet.Tables[0];
            var utenteList = new List<UtenteViewModel>();
            foreach (DataRow utenteRow in dataTable.Rows)
            {
                UtenteViewModel utenteViewModel = utenteRow.ToUtenteViewModel();
                utenteList.Add(utenteViewModel);
            }
            return utenteList;
        }

        public async Task<UtenteEditInputModel> GetUtenteForEditingAsync(int id)
        {
            FormattableString query = $@"SELECT Id, Cognome, Nome, Email, Telefono FROM Utenti WHERE Id={id}";

            DataSet dataSet = await db.QueryAsync(query);

            var utenteTable = dataSet.Tables[0];
            if (utenteTable.Rows.Count != 1)
            {
                logger.LogWarning("Utente {id} not found", id);
                throw new UtenteNotFoundException(id);
            }
            var utenteRow = utenteTable.Rows[0];
            var utenteEditInputModel = utenteRow.ToUtenteEditInputModel();
            return utenteEditInputModel;
        }

        /*public async Task<int> CreateUtenteAsync(UtenteCreateInputModel inputModel)
        {
            int affectedRows = await db.CommandAsync($@"INSERT INTO Utenti (Cognome, Nome, Email, Telefono) VALUES ({inputModel.Cognome}, {inputModel.Nome}, {inputModel.Email}, {inputModel.Telefono});");
                
            return affectedRows;
        }*/

        public async Task<UtenteDetailViewModel> CreateUtenteAsync(UtenteCreateInputModel inputModel)
        {
            int utenteId = await db.QueryScalarAsync<int>($@"INSERT INTO Utenti (Cognome, Nome, Email, Telefono) VALUES ({inputModel.Cognome}, {inputModel.Nome}, {inputModel.Email}, {inputModel.Telefono});
                                                SELECT last_insert_rowid();");

                UtenteDetailViewModel utente = await GetUtenteAsync(utenteId);
                return utente;
        }

        
        public async Task<UtenteDetailViewModel> EditUtenteAsync(UtenteEditInputModel inputModel)
        {
            int affectedRows = await db.CommandAsync($"UPDATE Utenti SET Cognome={inputModel.Cognome}, Nome={inputModel.Nome}, Email={inputModel.Email}, Telefono={inputModel.Telefono} WHERE Id={inputModel.Id}");
            if (affectedRows == 0)
            {
                bool utenteExists = await db.QueryScalarAsync<bool>($"SELECT COUNT(*) FROM Utenti WHERE Id={inputModel.Id}");
                if (!utenteExists)
                {
                    throw new UtenteNotFoundException(inputModel.Id);
                }
            }

            UtenteDetailViewModel utente = await GetUtenteAsync(inputModel.Id);
            return utente;
        }

        public async Task DeleteUtenteAsync(UtenteDeleteInputModel inputModel)
        {
            int affectedRows = await db.CommandAsync($"DELETE FROM Utenti WHERE Id={inputModel.Id}");
            if (affectedRows == 0)
            {
                throw new UtenteNotFoundException(inputModel.Id);
            }
        }
        
    }
}