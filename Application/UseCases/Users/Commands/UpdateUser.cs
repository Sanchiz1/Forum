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
    public record UpdateUserCommand : IRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Bio { get; set; }
        public string? Password { get; set; }
        public int User_Id {  get; set; }
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _context;

        public UpdateUserCommandHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken) => await _context.UpdateUserAsync(request);
    }
}
