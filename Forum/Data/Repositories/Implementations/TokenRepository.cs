using Dapper;
using Forum.Data.Repositories.Interfaces;
using Forum.Models;

namespace Forum.Data.Repositories.Implementations
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DapperContext _dapperContext;

        public TokenRepository(DapperContext context)
        {
            _dapperContext = context;
        }

        public void CreateRefreshToken(Token refreshToken, int userId)
        {
            try {
                string query = "INSERT INTO Refresh_Tokens (User_Id, Value, Issued_at, Expires_at) VALUES(@userId, @Value, @Issued_at, @Expires_at)";
                using var connection = _dapperContext.CreateConnection();
                connection.Execute(query, new { userId, refreshToken.Value, refreshToken.Issued_at, refreshToken.Expires_at });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        public void DeleteAllRefreshTokens(int userId)
        {
            string query = "DELETE Refresh_Tokens WHERE User_Id = @userId";
            using var connection = _dapperContext.CreateConnection();
            connection.Execute(query, new { userId });
        }

        public void DeleteRefreshToken(string refreshToken)
        {
            string query = "DELETE Refresh_Tokens WHERE Value = @refreshToken";
            using var connection = _dapperContext.CreateConnection();
            connection.Execute(query, new { refreshToken });
        }

        public void UpdateRefreshToken(string oldRefreshToken, Token refreshToken, int userId)
        {
            string query = "UPDATE Refresh_Tokens SET Token = @token, ExpiresStart = @issuedAt , ExpiresEnd = @expiredAt   WHERE UserId = @userId AND Token = @oldRefreshToken";
            using var connection = _dapperContext.CreateConnection();
            connection.Execute(query, new { userId, refreshToken.Value, refreshToken.Expires_at, refreshToken.Issued_at, oldRefreshToken });
        }

        public Token GetRefreshToken(string refreshToken)
        {
            string query = "SELECT * FROM Refresh_Tokens WHERE Token = @refreshToken";
            using var connection = _dapperContext.CreateConnection();
            return connection.QuerySingleOrDefault<Token?>(query, new { refreshToken });
        }
    }
}
