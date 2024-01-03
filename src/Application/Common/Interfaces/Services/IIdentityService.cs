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
        Task<LoginResponse> Login(LoginQuery loginQuery);
        Task<LoginResponse> RefreshToken(RefreshTokenQuery refreshTokenQuery);
        Task Logout(LogoutQuery logoutQuery);
    }
}
