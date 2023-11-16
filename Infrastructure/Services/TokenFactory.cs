using Application.Common.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Infrastructure.Services.TokenValidator;

namespace Infrastructure.Services
{
    public class TokenFactory : ITokenFactory
    {
        public const int RefreshTokenExpiration = 31536000;
        public const int AccessTokenExpiration = 60;

        private readonly IConfiguration _configuration;
        public TokenFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Token GetAccessToken(int userId)
        {
            var expiredAt = DateTime.UtcNow.Add(TimeSpan.FromSeconds(TokenFactory.AccessTokenExpiration));
            var issuedAt = DateTime.UtcNow;

            DateTimeOffset issuedAtOffset = issuedAt;

            var claims = new[] {
                new Claim("UserId", userId.ToString()),
                new Claim("IsAccess",true.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, issuedAtOffset.ToUnixTimeSeconds().ToString()),
            };

            var newAccessToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Author"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: expiredAt,
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(_configuration["JWT:Key"]), SecurityAlgorithms.HmacSha256)
            );

            return new(new JwtSecurityTokenHandler().WriteToken(newAccessToken), issuedAt, expiredAt);
        }

        public Token GetRefreshToken(int userId)
        {
            var expiredAt = DateTime.UtcNow.Add(TimeSpan.FromSeconds(TokenFactory.RefreshTokenExpiration));
            var issuedAt = DateTime.UtcNow;

            DateTimeOffset issuedAtOffset = issuedAt;

            var claims = new[] {
                new Claim("UserId", userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, issuedAtOffset.ToUnixTimeSeconds().ToString()),
                new Claim("isRefresh",true.ToString())
            };

            var refreshToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Author"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: expiredAt,
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(_configuration["JWT:Key"]), SecurityAlgorithms.HmacSha256)
            );
            return new Token(new JwtSecurityTokenHandler().WriteToken(refreshToken), issuedAt, expiredAt);
        }
    }
}
