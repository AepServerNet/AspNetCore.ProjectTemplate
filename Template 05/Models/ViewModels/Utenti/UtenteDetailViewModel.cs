using System.Collections.Generic;
using Template_SQLite_AdoNet_Crud_Polly.Models.ViewModels.Profili;

namespace Template_SQLite_AdoNet_Crud_Polly.Models.ViewModels.Utenti
{
    public class UtenteDetailViewModel
    {
        public int Id { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public List<ProfiloViewModel> Profili { get; set; } = new List<ProfiloViewModel>();
    }
}