using System;

namespace Template_SQLite_AdoNet_Crud_ExtensionMethod.Models.Exceptions.Application
{
    public class UtenteNotFoundException : Exception
    {
        public UtenteNotFoundException(int utenteId) : base($"Utente {utenteId} not found")
        {
        }
    }
}