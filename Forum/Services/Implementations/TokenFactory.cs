﻿using Forum.Data.Repositories.Interfaces;
using Forum.Models;
using Forum.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Forum.Services.Implementations
{
    public class TokenFactory : ITokenFactory
    {
        public const int RefreshTokenExpiration = 31536000;
        public const int AccessTokenExpiration = 60;

        private readonly IConfiguration _configuration;
        public readonly ITokenRepository _tokenRepository;
        public TokenFactory( ITokenRepository tokenRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenRepository = tokenRepository;
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
            return new(new JwtSecurityTokenHandler().WriteToken(refreshToken), issuedAt, expiredAt);
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
    }


    public class AuthOptions
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string KEY) =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
