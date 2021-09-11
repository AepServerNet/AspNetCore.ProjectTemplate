using System.ComponentModel.DataAnnotations;

namespace Template_SQLite_AdoNet_Crud_AutoMapper.Models.InputModels.Utenti
{
    public class UtenteDeleteInputModel
    {
        [Required]
        public int Id { get; set; }
    }
}