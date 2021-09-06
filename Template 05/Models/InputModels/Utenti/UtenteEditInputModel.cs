using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Template_SQLite_AdoNet_Crud_Polly.Models.InputModels.Utenti
{
    public class UtenteEditInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio"),
        MinLength(1, ErrorMessage = "Il cognome dev'essere di almeno {1} caratteri"),
        MaxLength(50, ErrorMessage = "Il cognome dev'essere di al massimo {1} caratteri"),
        Display(Name = "Cognome")]
        public string Cognome { get; set; }
        
        [Required(ErrorMessage = "Il nome è obbligatorio"),
        MinLength(1, ErrorMessage = "Il nome dev'essere di almeno {1} caratteri"),
        MaxLength(50, ErrorMessage = "Il nome dev'essere di al massimo {1} caratteri"),
        Display(Name = "Nome")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "L'indirizzo email è obbligatorio"),
        EmailAddress(ErrorMessage = "Devi inserire un indirizzo email"),
        Display(Name = "Indirizzo Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Il numero di telefono è obbligatorio"),
        Phone(ErrorMessage = "Devi inserire un numero di telefono"),
        Display(Name = "Telefono")]
        public string Telefono { get; set; }
    }

    
}