using Application.Common.Interfaces.Repositories;
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
    public record GetUserByCredentialsQuery : IRequest<User>
    {
        public string Username_Or_Email { get; set; }
        public string Hashed_Password { get; set; }
    }
    public class GetUserByCredentialsQueryHandler : IRequestHandler<GetUserByCredentialsQuery, User>
    {
        private readonly IUserRepository _context;

        public GetUserByCredentialsQueryHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task<User> Handle(GetUserByCredentialsQuery request, CancellationToken cancellationToken) => await _context.GetUserByCredentialsAsync(request);
    }
}
