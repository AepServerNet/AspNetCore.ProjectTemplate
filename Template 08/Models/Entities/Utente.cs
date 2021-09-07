using System;
using System.Collections.Generic;

namespace Template_SQLite_EfCore.Models.Entities
{
    public partial class Utente
    {
        public Utente(string cognome, string nome, string email, string telefono)
        {
            Profili = new HashSet<Profilo>();
            ChangeCognome(cognome);
            ChangeNome(nome);
            ChangeEmail(email);
            ChangeTelefono(telefono);
        }

        public int Id { get; private set; }
        public string Cognome { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Telefono { get; private set; }
        
        public void ChangeCognome(string newCognome)
        {
            if (string.IsNullOrWhiteSpace(newCognome))
            {
                throw new ArgumentException("Il cognome è obbligatorio");
            }
            Cognome = newCognome;
        }
        public void ChangeNome(string newNome)
        {
            if (string.IsNullOrWhiteSpace(newNome))
            {
                throw new ArgumentException("Il nome è obbligatorio");
            }
            Nome = newNome;
        }
        public void ChangeEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
            {
                throw new ArgumentException("L'indirizzo email è obbligatorio");
            }
            Email = newEmail;
        }
        public void ChangeTelefono(string newTelefono)
        {
            if (string.IsNullOrWhiteSpace(newTelefono))
            {
                throw new ArgumentException("Il numero di telefono è obbligatorio");
            }
            Telefono = newTelefono;
        }
        public virtual ICollection<Profilo> Profili { get; private set; }
    }
}
