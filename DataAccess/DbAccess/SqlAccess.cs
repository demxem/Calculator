using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccess
{
    public class SqlAccess : ISqlAccess
    {
        private readonly IConfiguration _config;

        public SqlAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedures, U parameters, string connectionId = "Default")
        {
            using var cnn = new SqlConnection(_config.GetConnectionString(connectionId));

            return await cnn.QueryAsync<T>(storedProcedures, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task SafeData<U>(string storedProcedures, U parameters, string connectionId = "Default")
        {
            using var cnn = new SqlConnection(_config.GetConnectionString(connectionId));

            await cnn.ExecuteAsync(storedProcedures, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
