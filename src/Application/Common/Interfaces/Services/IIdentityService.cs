using Application.Common.Models;
using Application.UseCases.Comments.Queries;
using Application.UseCases.Identity.Queries;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<Result<LoginResponse>> Login(LoginQuery loginQuery);
        Task<Result<LoginResponse>> RefreshToken(RefreshTokenQuery refreshTokenQuery);
        Task Logout(LogoutQuery logoutQuery);
    }
}
