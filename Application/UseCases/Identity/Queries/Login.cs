using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Comments.Queries
{
    public class LoginQuery : IRequest<LoginResponse>
    {
        public string Username_Or_Email { get; set; }
        public string Hashed_Password { get; set; }
    }
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
    {
        private readonly IIdentityService _context;

        public LoginQueryHandler(IIdentityService context)
        {
            _context = context;
        }

        public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken) => await _context.Login(request);
    }
}
