using Application.Common.Interfaces.Services;
using Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Comments.Queries
{
    public class LoginQuery : IRequest<Result<LoginResponse>>
    {
        public string Username_Or_Email { get; set; }
        public string Password { get; set; }
    }
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<LoginResponse>>
    {
        private readonly IIdentityService _context;

        public LoginQueryHandler(IIdentityService context)
        {
            _context = context;
        }

        public async Task<Result<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken) => await _context.Login(request);
    }
}
