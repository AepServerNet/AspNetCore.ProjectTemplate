using System.ComponentModel.DataAnnotations;

namespace Template_SQLite_AdoNet_Crud_ExtensionMethod.Models.InputModels.Profili
{
    public class ProfiloDeleteInputModel
    {
        [Required]
        public int Id { get; set; }
        public int UtenteId { get; set; }
    }
}