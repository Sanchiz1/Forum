using Application.Common.Interfaces.Services;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Identity.Queries
{
    public record RefreshTokenQuery : IRequest<LoginResponse>
    {
        public string Token { get; set; }
    }
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, LoginResponse>
    {
        private readonly IIdentityService _context;

        public RefreshTokenQueryHandler(IIdentityService context)
        {
            _context = context;
        }

        public async Task<LoginResponse> Handle(RefreshTokenQuery request, CancellationToken cancellationToken) => await _context.RefreshToken(request);
    }
}