using System;
using System.Data;

namespace Template_SQLite_AdoNet_Crud.Models.ViewModels.Utenti
{
    public class UtenteViewModel
    {
        public int Id { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        public static UtenteViewModel FromDataRow(DataRow utenteRow)
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
    }
}