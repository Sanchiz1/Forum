using Application.Common.Models;

namespace Application.Common.Interfaces.Services
{
    public interface ITokenFactory
    {
        Token GetRefreshToken(int userId);
        Token GetAccessToken(int userId);
    }
}
