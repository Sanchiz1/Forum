using Forum.Data.Repositories.Interfaces;
using Forum.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

namespace Forum.Services.Implementations
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

        public void ValidateRefreshToken(string refreshToken)
        {
            JwtSecurityToken objRefreshToken = ReadJwtToken(refreshToken);

            int userId = int.Parse(objRefreshToken.Claims.First(c => c.Type == "UserId").Value);
            bool isRefresh = bool.Parse(objRefreshToken.Claims.First(c => c.Type == "isRefresh").Value);

            if (!isRefresh)
            {
                throw new ValidationException("Invalid token");
            }

            var savedToken = _tokenRepository.GetRefreshToken(refreshToken);

            if (savedToken == null)
            {
                throw new ValidationException("Invalid token");
            }

            if (!ValidateToken(refreshToken))
            {
                throw new ValidationException("Invalid token");
            }
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

        public JwtSecurityToken ReadJwtToken(string token) => new JwtSecurityTokenHandler().ReadJwtToken(token);
    }
}
