using Template_SQLite_EfCore.Models.Entities;

namespace Template_SQLite_EfCore.Models.ViewModels.Profili
{
    public class ProfiloDetailViewModel
    {
        public int Id { get; set; }
        public int UtenteId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static ProfiloDetailViewModel FromEntity(Profilo profilo)
        {
            return new ProfiloDetailViewModel
            {
                Id = profilo.Id,
                UtenteId = profilo.UtenteId,
                Username = profilo.Username,
                Password = profilo.Password
            };
        }
    }

}