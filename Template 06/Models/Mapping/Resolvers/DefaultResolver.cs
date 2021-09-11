using System;
using System.Data;
using AutoMapper;

namespace Template_SQLite_AdoNet_Crud_AutoMapper.Models.Mapping.Resolvers
{
    public class DefaultResolver : IMemberValueResolver<DataRow, object, string, object>
    {
        public object Resolve(DataRow source, object destination, string sourceMember, object destMember, ResolutionContext context)
        {
            return source[sourceMember];
        }

        private static Lazy<DefaultResolver> instance = new Lazy<DefaultResolver>(() => new DefaultResolver());
        public static DefaultResolver Instance => instance.Value;
    }
}