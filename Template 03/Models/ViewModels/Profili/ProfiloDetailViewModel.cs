using System;
using System.Data;

namespace Template_SQLite_AdoNet_Crud.Models.ViewModels.Profili
{
    public class ProfiloDetailViewModel
    {
        public int Id { get; set; }
        public int UtenteId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static ProfiloDetailViewModel FromDataRow(DataRow dataRow)
        {
            var profiloDetailViewModel = new ProfiloDetailViewModel {
                Id = Convert.ToInt32(dataRow["Id"]),
                UtenteId = Convert.ToInt32(dataRow["UtenteId"]),
                Username = Convert.ToString(dataRow["Username"]),
                Password = Convert.ToString(dataRow["Password"])
            };
            return profiloDetailViewModel;
        }
    }
}