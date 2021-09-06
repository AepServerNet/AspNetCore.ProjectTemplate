using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Template_SQLite_AdoNet_Crud_Polly.Models.InputModels.Profili
{
    public class ProfiloEditInputModel
    {
        [Required]
        public int Id { get; set; }
        public int UtenteId { get; set; }

        [Required(ErrorMessage = "Il nome utente è obbligatorio"),
        MinLength(5, ErrorMessage = "Il nome utente dev'essere di almeno {1} caratteri"),
        MaxLength(50, ErrorMessage = "Il nome utente dev'essere di al massimo {1} caratteri"),
        Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La password è obbligatoria"),
        MinLength(5, ErrorMessage = "La password dev'essere di almeno {1} caratteri"),
        MaxLength(50, ErrorMessage = "La password dev'essere di al massimo {1} caratteri"),
        Display(Name = "Password")]
        public string Password { get; set; }
    }
}