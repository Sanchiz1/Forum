using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Common.Interfaces.Services
{
    public interface ITokenValidator
    {

        Task<bool> ValidateRefreshToken(string refreshToken);
        Task<bool> ValidateToken(string token);
        Task<JwtSecurityToken> ReadJwtToken(string token);
    }
}
