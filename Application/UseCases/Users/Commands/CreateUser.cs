using Application.Common.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Commands
{
    public record CreateUserCommand : IRequest
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

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken) => await _context.CreateUserAsync(request);
    }
}
