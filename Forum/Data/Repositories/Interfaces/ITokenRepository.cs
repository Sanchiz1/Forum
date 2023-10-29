using Forum.Models.Identity;

namespace Forum.Data.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        public void CreateRefreshToken(Token refreshToken, int userId);
        public void UpdateRefreshToken(string oldRefreshToken, Token refreshToken, int userId);
        public void DeleteRefreshToken(string refreshToken);
        public void DeleteAllRefreshTokens(int userId);
        public Token? GetRefreshToken(string refreshToken);
    }
}
