using Application.Common.Interfaces.Services;
using Application.Common.Models;
using Application.UseCases.Comments.Queries;
using Application.UseCases.Identity.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        public Task<LoginResponse> Login(LoginQuery loginQuery)
        {
            throw new NotImplementedException();
        }
        public Task<LoginResponse> RefreshToken(RefreshTokenQuery refreshTokenCommand)
        {
            throw new NotImplementedException();
        }
        public Task Logout(LogoutQuery logoutQuery)
        {
            throw new NotImplementedException();
        }
    }
}
