using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TokenValidator : ITokenValidator
    {
        public readonly ITokenRepository _tokenRepository;
        private readonly IConfiguration _configuration;

        public TokenValidator(IConfiguration configuration, ITokenRepository tokenRepository)
        {
            _configuration = configuration;
            _tokenRepository = tokenRepository;
        }
        public async Task<bool> ValidateRefreshTokenAsync(string refreshToken)
        {
            JwtSecurityToken objRefreshToken = ReadJwtToken(refreshToken);

            int userId = int.Parse(objRefreshToken.Claims.First(c => c.Type == "UserId").Value);
            bool isRefresh = bool.Parse(objRefreshToken.Claims.First(c => c.Type == "isRefresh").Value);

            if (!isRefresh) return false;

            var savedToken = await _tokenRepository.GetRefreshTokenAsync(refreshToken);

            if (savedToken == null) return false;

            if (!ValidateToken(refreshToken)) return false;

            return true;
        }

        public bool ValidateToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();

                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JWT:Author"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["JWT:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(_configuration["JWT:Key"]!),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken securityToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public JwtSecurityToken ReadJwtToken(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token);
        }
    }
    public class AuthOptions
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string KEY) =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
