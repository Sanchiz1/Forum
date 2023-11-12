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
    public class GetUserByUsernameQuery : IRequest<User>
    {
        public string Username { get; set; }
    }
    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, User>
    {
        private readonly IUserRepository _context;

        public GetUserByUsernameQueryHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task<User> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken) => await _context.GetUserByUsernameAsync(request);
    }
}
