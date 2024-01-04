using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Common.ViewModels;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Queries
{
    public class GetUserByCredentialsQuery : IRequest<Result<UserViewModel>>
    {
        public string Username_Or_Email { get; set; }
        public string Password { get; set; }
    }
    public class GetUserByCredentialsQueryHandler : IRequestHandler<GetUserByCredentialsQuery, Result<UserViewModel>>
    {
        private readonly IUserRepository _context;

        public GetUserByCredentialsQueryHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task<Result<UserViewModel>> Handle(GetUserByCredentialsQuery request, CancellationToken cancellationToken) => await _context.GetUserByCredentialsAsync(request);
    }
}
