using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.UseCases.Categories.Queries;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DapperContext _dapperContext;

        public TokenRepository(DapperContext context)
        {
            _dapperContext = context;
        }
        public async Task<Token> GetRefreshTokenAsync(string refreshToken)
        {
            Token result = null;

            string query = "SELECT * FROM Refresh_Tokens WHERE Value = @refreshToken";

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<Token>(query, new { refreshToken })).FirstOrDefault();

            return result;
        }
        public async Task CreateRefreshTokenAsync(Token refreshToken, int userId)
        {
            string query = "INSERT INTO Refresh_Tokens (User_Id, Value, Issued_at, Expires_at) VALUES(@userId, @Value, @Issued_at, @Expires_at)";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, new { userId, refreshToken.Value, refreshToken.Issued_at, refreshToken.Expires_at });
        }
        public async Task UpdateRefreshTokenAsync(string oldRefreshToken, Token refreshToken, int userId)
        {
            string query = "UPDATE Refresh_Tokens SET Value = @Value, Issued_at = @Issued_at , Expires_at = @Expires_at WHERE User_Id = @userId AND Value = @oldRefreshToken";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, new { userId, refreshToken.Value, refreshToken.Expires_at, refreshToken.Issued_at, oldRefreshToken });
        }
        public async Task DeleteRefreshTokenAsync(string refreshToken)
        {
            string query = $"DELETE Refresh_Tokens WHERE Value = @refreshToken";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, new { refreshToken });
        }
        public async Task DeleteAllRefreshTokensAsync(int userId)
        {
            string query = "DELETE Refresh_Tokens WHERE User_Id = @userId";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, new { userId });
        }
    }
}
