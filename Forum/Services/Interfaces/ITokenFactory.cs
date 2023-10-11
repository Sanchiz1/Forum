using Forum.Models;

namespace Forum.Services.Interfaces
{
    public interface ITokenFactory
    {
        public Token GetRefreshToken(int userId);
        public Token GetAccessToken(int userId);
    }
}
