using Application.Common.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TokenFactory : ITokenFactory
    {
        public Task<Token> GetAccessTokenAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Token> GetRefreshTokenAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
