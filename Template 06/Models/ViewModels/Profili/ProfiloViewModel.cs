using System;
using System.Data;

namespace Template_SQLite_AdoNet_Crud_AutoMapper.Models.ViewModels.Profili
{
    public class ProfiloViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static ProfiloViewModel FromDataRow(DataRow dataRow)
        {
            var profiloViewModel = new ProfiloViewModel {
                Id = Convert.ToInt32(dataRow["Id"]),
                Username = Convert.ToString(dataRow["Username"]),
                Password = Convert.ToString(dataRow["Password"]),
            };
            return profiloViewModel;
        }
    }
}