using System;
using System.Collections.Generic;

namespace Template_SQLite_EfCore.Models.Entities
{
    public partial class Profilo
    {
        public Profilo(int utenteId)
        {
            UtenteId = utenteId;
            Username = string.Empty; //In alternativa aggiungere un valore di default
            Password = string.Empty; //In alternativa aggiungere un valore di default
        }

        public int Id { get; private set; }
        public int UtenteId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public virtual Utente Utente { get; set; }

        public void ChangeUsername(string username)
        {
            Username = username;
        }
        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}
