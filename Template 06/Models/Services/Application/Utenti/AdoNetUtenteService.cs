using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.Exceptions.Application;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.InputModels.Utenti;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.Services.Infrastructure;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.ViewModels.Profili;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.ViewModels.Utenti;

namespace Template_SQLite_AdoNet_Crud_AutoMapper.Models.Services.Application.Utenti
{
    public class AdoNetUtenteService : IUtenteService
    {
        private readonly ILogger<AdoNetUtenteService> logger;
        private readonly IDatabaseAccessor db;
        private readonly IMapper mapper;
        public AdoNetUtenteService(ILogger<AdoNetUtenteService> logger, IDatabaseAccessor db, IMapper mapper)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
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
            //var utenteDetailViewModel = UtenteDetailViewModel.FromDataRow(utenteRow);
            var utenteDetailViewModel = mapper.Map<UtenteDetailViewModel>(utenteRow);

            //Utente profilo
            var profiloDataTable = dataSet.Tables[1];

            /*foreach (DataRow profiloRow in profiloDataTable.Rows)
            {
                ProfiloViewModel profiloViewModel = ProfiloViewModel.FromDataRow(profiloRow);
                utenteDetailViewModel.Profili.Add(profiloViewModel);
            }*/
            utenteDetailViewModel.Profili = mapper.Map<List<ProfiloViewModel>>(profiloDataTable.Rows);
            return utenteDetailViewModel;
        }

        public async Task<List<UtenteViewModel>> GetUtentiAsync()
        {
            FormattableString query = $"SELECT Id, Cognome, Nome, Email, Telefono FROM Utenti";
            DataSet dataSet = await db.QueryAsync(query);
            var dataTable = dataSet.Tables[0];
            /*var utenteList = new List<UtenteViewModel>();
            foreach (DataRow courseRow in dataTable.Rows)
            {
                UtenteViewModel utenteViewModel = UtenteViewModel.FromDataRow(courseRow);
                utenteList.Add(utenteViewModel);
            }*/
            var utenteList = mapper.Map<List<UtenteViewModel>>(dataTable.Rows);
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
            //var utenteEditInputModel = UtenteEditInputModel.FromDataRow(utenteRow);
            var utenteEditInputModel = mapper.Map<UtenteEditInputModel>(utenteRow);
            return utenteEditInputModel;
        }

        public async Task<int> CreateUtenteAsync(UtenteCreateInputModel inputModel)
        {
            int affectedRows = await db.CommandAsync($@"INSERT INTO Utenti (Cognome, Nome, Email, Telefono) VALUES ({inputModel.Cognome}, {inputModel.Nome}, {inputModel.Email}, {inputModel.Telefono});");
                
            return affectedRows;
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