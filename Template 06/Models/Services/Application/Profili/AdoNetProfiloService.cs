using System;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.Exceptions.Application;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.InputModels.Profili;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.Services.Infrastructure;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.ViewModels.Profili;

namespace Template_SQLite_AdoNet_Crud_AutoMapper.Models.Services.Application.Profili
{
    public class AdoNetProfiloService : IProfiloService
    {
        private readonly ILogger<AdoNetProfiloService> logger;
        private readonly IDatabaseAccessor db;
        private readonly IMapper mapper;
        public AdoNetProfiloService(ILogger<AdoNetProfiloService> logger, IDatabaseAccessor db, IMapper mapper)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<ProfiloDetailViewModel> CreateProfiloAsync(ProfiloCreateInputModel inputModel)
        {
            int profiloId = await db.QueryScalarAsync<int>($@"INSERT INTO Profili (UtenteId, Username, Password) VALUES ({inputModel.UtenteId}, {inputModel.Username}, {inputModel.Password});
                                                 SELECT last_insert_rowid();");

            ProfiloDetailViewModel profilo = await GetProfiloAsync(profiloId);
            return profilo;
        }

        public async Task<ProfiloDetailViewModel> EditProfiloAsync(ProfiloEditInputModel inputModel)
        {
            int affectedRows = await db.CommandAsync($"UPDATE Profili SET Username={inputModel.Username}, Password={inputModel.Password} WHERE Id={inputModel.Id}");
            if (affectedRows == 0)
            {
                bool profiloExists = await db.QueryScalarAsync<bool>($"SELECT COUNT(*) FROM Profili WHERE Id={inputModel.Id}");
                if (!profiloExists)
                {
                    throw new ProfiloNotFoundException(inputModel.Id);
                }
            }
            ProfiloDetailViewModel profilo = await GetProfiloAsync(inputModel.Id);
            return profilo;
        }

        public async Task<ProfiloDetailViewModel> GetProfiloAsync(int id)
        {
            FormattableString query = $@"SELECT Id, UtenteId, Username, Password FROM Profili WHERE ID={id}";

            DataSet dataSet = await db.QueryAsync(query);

            //Utenti
            var profiloTable = dataSet.Tables[0];
            if (profiloTable.Rows.Count != 1)
            {
                logger.LogWarning("Profilo {id} not found", id);
                throw new ProfiloNotFoundException(id);
            }
            var profiloRow = profiloTable.Rows[0];
            //var profiloDetailViewModel = ProfiloDetailViewModel.FromDataRow(profiloRow);
            var profiloDetailViewModel = mapper.Map<ProfiloDetailViewModel>(profiloRow);
            return profiloDetailViewModel;
        }

        public async Task<ProfiloEditInputModel> GetProfiloForEditingAsync(int id)
        {
            FormattableString query = $@"SELECT Id, UtenteId, Username, Password FROM Profili WHERE ID={id}";

            DataSet dataSet = await db.QueryAsync(query);

            //Utenti
            var profiloTable = dataSet.Tables[0];
            if (profiloTable.Rows.Count != 1)
            {
                logger.LogWarning("Profilo {id} not found", id);
                throw new ProfiloNotFoundException(id);
            }
            var profiloRow = profiloTable.Rows[0];
            //var profiloEditInputModel = ProfiloEditInputModel.FromDataRow(profiloRow);
            var profiloEditInputModel = mapper.Map<ProfiloEditInputModel>(profiloRow);
            return profiloEditInputModel;
        }

        public async Task DeleteProfiloAsync(ProfiloDeleteInputModel inputModel)
        {
            int affectedRows = await db.CommandAsync($"DELETE FROM Profili WHERE Id={inputModel.Id}");
            if (affectedRows == 0)
            {
                throw new ProfiloNotFoundException(inputModel.Id);
            }
        }
    }
}