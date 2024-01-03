using Application.Common.Interfaces.Services;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class DapperContext : IDbContext
    {
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQLConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
