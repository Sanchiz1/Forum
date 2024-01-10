using Application.Common.Interfaces.Services;
using Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Identity.Queries
{
    public class LogoutQuery : IRequest<Result<string>>
    {
        public string Token { get; set; }
    }
    public class LogoutQueryHandler : IRequestHandler<LogoutQuery, Result<string>>
    {
        private readonly IIdentityService _context;

        public LogoutQueryHandler(IIdentityService context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(LogoutQuery request, CancellationToken cancellationToken)
        {
            await _context.Logout(request);

            return "Logged out successfully";
        }
    }
}
