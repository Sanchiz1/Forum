﻿using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly ILogger _logger;

        public TokenRepository(DapperContext context, ILogger logger)
        {
            _dapperContext = context;
            _logger = logger;
        }
        public async Task<Token> GetRefreshTokenAsync(string refreshToken)
        {
            Token result = null;

            string query = "SELECT * FROM Refresh_Tokens WHERE Value = @refreshToken";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<Token>(query, new { refreshToken })).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting token");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting token");
                throw;
            }

            return result;
        }
        public async Task CreateRefreshTokenAsync(Token refreshToken, int userId)
        {
            string query = "INSERT INTO Refresh_Tokens (User_Id, Value, Issued_at, Expires_at) VALUES(@userId, @Value, @Issued_at, @Expires_at)";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, new { userId, refreshToken.Value, refreshToken.Issued_at, refreshToken.Expires_at });
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Creating token");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Creating token");
                throw;
            }
        }
        public async Task UpdateRefreshTokenAsync(string oldRefreshToken, Token refreshToken, int userId)
        {
            string query = "UPDATE Refresh_Tokens SET Value = @Value, Issued_at = @Issued_at , Expires_at = @Expires_at WHERE User_Id = @userId AND Value = @oldRefreshToken";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, new { userId, refreshToken.Value, refreshToken.Expires_at, refreshToken.Issued_at, oldRefreshToken });
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Updating token");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Updating token");
                throw;
            }
        }
        public async Task DeleteRefreshTokenAsync(string refreshToken)
        {
            string query = $"DELETE Refresh_Tokens WHERE Value = @refreshToken";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, new { refreshToken });
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Deleting token");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deleting token");
                throw;
            }
        }
        public async Task DeleteAllRefreshTokensAsync(int userId)
        {
            string query = "DELETE Refresh_Tokens WHERE User_Id = @userId";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, new { userId });
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Deleting all tokens");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deleting all tokens");
                throw;
            }
        }
    }
}
