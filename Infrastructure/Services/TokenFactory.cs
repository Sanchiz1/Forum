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
        public Task<Token> GetAccessToken(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Token> GetRefreshToken(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
