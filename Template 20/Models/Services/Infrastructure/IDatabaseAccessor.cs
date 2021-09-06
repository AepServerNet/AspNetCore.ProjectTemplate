using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Template_SqlServer_AdoNet.Models.Services.Infrastructure
{
    public interface IDatabaseAccessor
    {
        Task<DataSet> QueryAsync(FormattableString formattableQuery);
        Task<T> QueryScalarAsync<T>(FormattableString formattableQuery);
        Task<int> CommandAsync(FormattableString formattableCommand);
    }
}