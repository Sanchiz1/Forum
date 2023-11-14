using Application.Common.Interfaces.Repositories;
using Application.UseCases.Users.Queries;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Commands
{
    public class CreateUserCommand : IRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Bio { get; set; }
        public string? Password { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _context;

        public CreateUserCommandHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _context.GetUserByUsernameAsync(new GetUserByUsernameQuery() { Username = request.Username}) != null)
            {
                throw new Exception("User with this username already exists");
            }

            if (await _context.GetUserByEmailAsync(new GetUserByEmailQuery() { Email = request.Username }) != null)
            {
                throw new Exception("User with this email already exists");
            }

            await _context.CreateUserAsync(request);
        }
    }
}
