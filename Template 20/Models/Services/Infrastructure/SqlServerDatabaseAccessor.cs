using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Template_SqlServer_AdoNet.Models.Options;
using Template_SqlServer_AdoNet.Models.ValueTypes;

namespace Template_SqlServer_AdoNet.Models.Services.Infrastructure
{
    public class SqlServerDatabaseAccessor : IDatabaseAccessor
    {
        private readonly ILogger<SqlServerDatabaseAccessor> logger;
        private readonly IOptionsMonitor<ConnectionStringsOptions> connectionStringOptions;
        public SqlServerDatabaseAccessor(ILogger<SqlServerDatabaseAccessor> logger, IOptionsMonitor<ConnectionStringsOptions> connectionStringOptions)
        {
            this.logger = logger;
            this.connectionStringOptions = connectionStringOptions;
        }
        public async Task<int> CommandAsync(FormattableString formattableCommand)
        {
                using SqlConnection conn = await GetOpenedConnection();
                using SqlCommand cmd = GetCommand(formattableCommand, conn);

                int affectedRows = await cmd.ExecuteNonQueryAsync();
                return affectedRows;
        }
        public async Task<T> QueryScalarAsync<T>(FormattableString formattableQuery)
        {
                using SqlConnection conn = await GetOpenedConnection();
                using SqlCommand cmd = GetCommand(formattableQuery, conn);

                object result = await cmd.ExecuteScalarAsync();
                return (T)Convert.ChangeType(result, typeof(T));
        }
        public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {
            logger.LogInformation(formattableQuery.Format, formattableQuery.GetArguments());

            using SqlConnection conn = await GetOpenedConnection();
            using SqlCommand cmd = GetCommand(formattableQuery, conn);

                using var reader = await cmd.ExecuteReaderAsync();
                var dataSet = new DataSet();

                do
                {
                    var dataTable = new DataTable();
                    dataSet.Tables.Add(dataTable);
                    dataTable.Load(reader);
                } while (!reader.IsClosed);

                return dataSet;
        }
        private static SqlCommand GetCommand(FormattableString formattableQuery, SqlConnection conn)
        {
            var queryArguments = formattableQuery.GetArguments();
            var sqlParameter = new List<SqlParameter>();
            for (var i = 0; i < queryArguments.Length; i++)
            {
                if (queryArguments[i] is Sql)
                {
                    continue;
                }
                var parameter = new SqlParameter(i.ToString(), value: queryArguments[i] ?? DBNull.Value);
                sqlParameter.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            string query = formattableQuery.ToString();

            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddRange(sqlParameter.ToArray());
            return cmd;
        }
        private async Task<SqlConnection> GetOpenedConnection()
        {
            var conn = new SqlConnection(connectionStringOptions.CurrentValue.Default);
            await conn.OpenAsync();
            return conn;
        }
    }
}