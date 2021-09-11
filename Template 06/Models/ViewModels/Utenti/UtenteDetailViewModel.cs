using System;
using System.Collections.Generic;
using System.Data;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.InputModels.Utenti;
using Template_SQLite_AdoNet_Crud_AutoMapper.Models.ViewModels.Profili;

namespace Template_SQLite_AdoNet_Crud_AutoMapper.Models.ViewModels.Utenti
{
    public class UtenteDetailViewModel
    {
        public int Id { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public List<ProfiloViewModel> Profili { get; set; } = new List<ProfiloViewModel>();

        public static UtenteDetailViewModel FromDataRow(DataRow utenteRow)
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
    }
}