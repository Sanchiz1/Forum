using Application.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    internal class TokenValidator : ITokenValidator
    {
        public Task<bool> ValidateRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateToken(string token)
        {
            throw new NotImplementedException();
        }
        public Task<JwtSecurityToken> ReadJwtToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
