using System.ComponentModel.DataAnnotations;

namespace Template_SQLite_AdoNet_Crud.Models.InputModels.Profili
{
    public class ProfiloDeleteInputModel
    {
        [Required]
        public int Id { get; set; }
        public int UtenteId { get; set; }
    }
}