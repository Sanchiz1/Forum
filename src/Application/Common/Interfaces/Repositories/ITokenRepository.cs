﻿using Application.Common.Models;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface ITokenRepository
    {
        Task CreateRefreshTokenAsync(Token refreshToken, int userId);
        Task UpdateRefreshTokenAsync(string oldRefreshToken, Token refreshToken, int userId);
        Task DeleteRefreshTokenAsync(string refreshToken);
        Task DeleteAllRefreshTokensAsync(int userId);
        Task<Token> GetRefreshTokenAsync(string refreshToken);
    }
}
