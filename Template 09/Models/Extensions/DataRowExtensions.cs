using System;
using System.Collections.Generic;
using System.Data;
using Template_SQLite_EfCore.Models.InputModels.Profili;
using Template_SQLite_EfCore.Models.InputModels.Utenti;
using Template_SQLite_EfCore.Models.ViewModels.Profili;
using Template_SQLite_EfCore.Models.ViewModels.Utenti;

namespace Template_SQLite_EfCore.Models.Extensions
{
    public static class DataRowExtensions
    {
        public static UtenteDetailViewModel ToUtenteDetailViewModel(this DataRow utenteRow)
        {
            var utenteDetailViewModel = new UtenteDetailViewModel
            {
                Cognome = Convert.ToString(utenteRow["Cognome"]),
                Nome = Convert.ToString(utenteRow["Nome"]),
                Email = Convert.ToString(utenteRow["Email"]),
                Telefono = Convert.ToString(utenteRow["Telefono"]),
                Id = Convert.ToInt32(utenteRow["Id"]),
                Profili = new List<ProfiloViewModel>()
            };
            return utenteDetailViewModel;
        }

        public static UtenteViewModel ToUtenteViewModel(this DataRow utenteRow)
        {
            var utenteViewModel = new UtenteViewModel
            {
                Cognome = Convert.ToString(utenteRow["Cognome"]),
                Nome = Convert.ToString(utenteRow["Nome"]),
                Email = Convert.ToString(utenteRow["Email"]),
                Telefono = Convert.ToString(utenteRow["Telefono"]),
                Id = Convert.ToInt32(utenteRow["Id"])
            };
            return utenteViewModel;
        }

        public static UtenteEditInputModel ToUtenteEditInputModel(this DataRow utenteRow)
        {
            var utenteEditInputModel = new UtenteEditInputModel
            {
                Cognome = Convert.ToString(utenteRow["Cognome"]),
                Nome = Convert.ToString(utenteRow["Nome"]),
                Email = Convert.ToString(utenteRow["Email"]),
                Telefono = Convert.ToString(utenteRow["Telefono"]),
                Id = Convert.ToInt32(utenteRow["Id"]),
            };
            return utenteEditInputModel;
        }

        public static ProfiloDetailViewModel ToProfiloDetailViewModel(this DataRow dataRow)
        {
            var profiloDetailViewModel = new ProfiloDetailViewModel {
                Id = Convert.ToInt32(dataRow["Id"]),
                UtenteId = Convert.ToInt32(dataRow["UtenteId"]),
                Username = Convert.ToString(dataRow["Username"]),
                Password = Convert.ToString(dataRow["Password"])
            };
            return profiloDetailViewModel;
        }

        public static ProfiloViewModel ToProfiloViewModel(this DataRow dataRow)
        {
            var profiloViewModel = new ProfiloViewModel {
                Id = Convert.ToInt32(dataRow["Id"]),
                Username = Convert.ToString(dataRow["Username"]),
                Password = Convert.ToString(dataRow["Password"]),
            };
            return profiloViewModel;
        }

        public static ProfiloEditInputModel ToProfiloEditInputModel(this DataRow utenteRow)
        {
            var profiloEditInputModel = new ProfiloEditInputModel
            {
                Id = Convert.ToInt32(utenteRow["Id"]),
                UtenteId = Convert.ToInt32(utenteRow["UtenteId"]),
                Username = Convert.ToString(utenteRow["Username"]),
                Password = Convert.ToString(utenteRow["Password"])                
            };
            return profiloEditInputModel;
        }
    }
}