using System;

namespace Template_SQLite_AdoNet_Crud_ExtensionMethod.Models.Exceptions.Application
{
    public class ProfiloNotFoundException : Exception
    {
        public ProfiloNotFoundException(int utenteId) : base($"Profilo {utenteId} not found")
        {
        }
    }
}