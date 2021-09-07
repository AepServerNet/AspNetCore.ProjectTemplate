using System.Collections.Generic;
using System.Data;
using System.Linq;
using Template_SQLite_EfCore.Models.Entities;
using Template_SQLite_EfCore.Models.ViewModels.Profili;

namespace Template_SQLite_EfCore.Models.ViewModels.Utenti
{
    public class UtenteDetailViewModel
    {
        public int Id { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public List<ProfiloViewModel> Profili { get; set; } = new List<ProfiloViewModel>();

        public static UtenteDetailViewModel FromEntity(Utente utente)
        {
            return new UtenteDetailViewModel
            {
                Id = utente.Id,
                Cognome = utente.Cognome,
                Nome = utente.Nome,
                Email = utente.Email,
                Telefono = utente.Telefono,
                Profili = utente.Profili
                    .Select(profilo => ProfiloViewModel.FromEntity(profilo))
                    .ToList()
            };
        }
    }

}