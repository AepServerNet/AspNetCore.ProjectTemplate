using Template_SQLite_EfCore.Models.Entities;

namespace Template_SQLite_EfCore.Models.ViewModels.Profili
{
    public class ProfiloViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static ProfiloViewModel FromEntity(Profilo profilo)
        {
            return new ProfiloViewModel
            {
                Id = profilo.Id,
                Username = profilo.Username,
                Password = profilo.Password
            };
        }
    }

}