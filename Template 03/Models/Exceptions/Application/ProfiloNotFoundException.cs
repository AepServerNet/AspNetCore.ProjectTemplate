using System;

namespace Template_SQLite_AdoNet_Crud.Models.Exceptions.Application
{
    public class ProfiloNotFoundException : Exception
    {
        public ProfiloNotFoundException(int utenteId) : base($"Profilo {utenteId} not found")
        {
        }
    }
}