using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
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
