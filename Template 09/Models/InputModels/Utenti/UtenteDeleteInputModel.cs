using System.ComponentModel.DataAnnotations;

namespace Template_SQLite_EfCore.Models.InputModels.Utenti
{
    public class UtenteDeleteInputModel
    {
        [Required]
        public int Id { get; set; }
    }

}