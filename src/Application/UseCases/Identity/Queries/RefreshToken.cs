using Application.Common.Interfaces.Services;
using Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Identity.Queries
{
    public class RefreshTokenQuery : IRequest<Result<LoginResponse>>
    {
        public string Token { get; set; }
    }
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, Result<LoginResponse>>
    {
        private readonly IIdentityService _context;

        public RefreshTokenQueryHandler(IIdentityService context)
        {
            _context = context;
        }

        public async Task<Result<LoginResponse>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken) => await _context.RefreshToken(request);
    }
}