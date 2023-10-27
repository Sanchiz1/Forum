using Forum.Services.Implementations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Forum.Services.Interfaces
{
    public interface ITokenValidator
    {
        public void ValidateRefreshToken(string refreshToken);
        public bool ValidateToken(string token);
        public JwtSecurityToken ReadJwtToken(string token);
    }
}
