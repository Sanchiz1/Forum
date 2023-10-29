using Dapper;
using Forum.Data.Repositories.Interfaces;
using Forum.Models.Identity;

namespace Forum.Data.Repositories.Implementations
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DapperContext _dapperContext;

        public TokenRepository(DapperContext context)
        {
            _dapperContext = context;
        }

        public Token GetRefreshToken(string refreshToken)
        {
            string query = "SELECT * FROM Refresh_Tokens WHERE Value = @refreshToken";
            using var connection = _dapperContext.CreateConnection();
            return connection.QuerySingleOrDefault<Token?>(query, new { refreshToken });
        }

        public void CreateRefreshToken(Token refreshToken, int userId)
        {
            string query = "INSERT INTO Refresh_Tokens (User_Id, Value, Issued_at, Expires_at) VALUES(@userId, @Value, @Issued_at, @Expires_at)";
            using var connection = _dapperContext.CreateConnection();
            connection.Execute(query, new { userId, refreshToken.Value, refreshToken.Issued_at, refreshToken.Expires_at });
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
            string query = "UPDATE Refresh_Tokens SET Value = @Value, Issued_at = @Issued_at , Expires_at = @Expires_at WHERE User_Id = @userId AND Value = @oldRefreshToken";
            using var connection = _dapperContext.CreateConnection();
            connection.Execute(query, new { userId, refreshToken.Value, refreshToken.Expires_at, refreshToken.Issued_at, oldRefreshToken });
        }
    }
}
