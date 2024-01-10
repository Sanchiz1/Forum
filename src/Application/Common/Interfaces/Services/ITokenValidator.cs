using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface ITokenValidator
    {
        Task<bool> ValidateRefreshTokenAsync(string refreshToken);
        bool ValidateToken(string token);
        JwtSecurityToken ReadJwtToken(string token);
    }
}
