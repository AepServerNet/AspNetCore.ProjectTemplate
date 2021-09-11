using System;
using System.Data;
using AutoMapper;

namespace Template_SQLite_AdoNet_Crud_AutoMapper.Models.Mapping.Resolvers
{
    public class UtenteIdResolver : IValueResolver<DataRow, object, int>
    {
        public int Resolve(DataRow source, object destination, int destMember, ResolutionContext context)
        {
            return Convert.ToInt32(source["UtenteId"]);
        }

        private static Lazy<IdResolver> instance = new Lazy<IdResolver>(() => new IdResolver());
        public static IdResolver Instance => instance.Value;
    }
}