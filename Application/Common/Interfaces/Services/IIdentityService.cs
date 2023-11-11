using Application.Common.Models;
using Application.UseCases.Comments.Queries;
using Application.UseCases.Identity.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<LoginOutput> Login(LoginQuery loginQuery);
        Task<LoginOutput> RefreshToken(RefreshTokenQuery refreshTokenCommand);
        Task Logout(LogoutQuery logoutQuery);
    }
}
