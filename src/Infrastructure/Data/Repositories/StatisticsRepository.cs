using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
using Application.UseCases.Statistics.Queries;
using Application.UseCases.Users.Queries;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly ILogger _logger;

        public StatisticsRepository(DapperContext context, ILogger logger)
        {
            _dapperContext = context;
            _logger = logger;
        }

        public async Task<int[]> GetMonthlyPostsAsync(GetMonthlyPostsQuery getMonthlyPostsQuery)
        {
            int[] result = null;

            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Year", getMonthlyPostsQuery.Year);
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<int>("proc_monthly_posts", parameters, commandType: CommandType.StoredProcedure)).ToArray();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting monthly posts");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting monthly posts");
                throw;
            }

            return result;
        }

        public async Task<int[]> GetMonthlyUsersAsync(GetMonthlyUsersQuery getMonthlyUsersQuery)
        {

            int[] result = null;

            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Year", getMonthlyUsersQuery.Year);
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<int>("proc_monthly_users", parameters, commandType: CommandType.StoredProcedure)).ToArray();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting monthly users");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting monthly users");
                throw;
            }

            return result;
        }
    }
}
