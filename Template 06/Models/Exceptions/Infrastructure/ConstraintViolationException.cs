using System;

namespace Template_SQLite_AdoNet_Crud_AutoMapper.Models.Exceptions.Infrastructure
{
    public class ConstraintViolationException : Exception
    {
        public ConstraintViolationException(Exception innerException) : base($"A violation occurred for a database constraint", innerException)
        {
        }
    }
}