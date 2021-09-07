using System;
using System.Collections.Generic;

namespace Template_SQLite_EfCore.Models.Entities
{
    public partial class Utente
    {
        public Utente(string cognome, string nome)
        {
            Profili = new HashSet<Profilo>();
            ChangeCognome(cognome);
            ChangeNome(nome);
            Email = string.Empty; //In alternativa aggiungere un valore di default
            Telefono = string.Empty; //In alternativa aggiungere un valore di default
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
        public void ChangeEmail(string email)
        {
            Email = email;
        }
        public void ChangeTelefono(string telefono)
        {
            Telefono = telefono;
        }
        public virtual ICollection<Profilo> Profili { get; private set; }
    }
}
