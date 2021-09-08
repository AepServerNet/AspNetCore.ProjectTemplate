using Template_SQLite_EfCore.Models.Entities;

namespace Template_SQLite_EfCore.Models.ViewModels.Utenti
{
    public class UtenteViewModel
    {
        public int Id { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        public static UtenteViewModel FromEntity(Utente utente)
        {
            return new UtenteViewModel
            {
                Id = utente.Id,
                Cognome = utente.Cognome,
                Nome = utente.Nome,
                Email = utente.Email,
                Telefono = utente.Telefono
            };
        }
    }

}