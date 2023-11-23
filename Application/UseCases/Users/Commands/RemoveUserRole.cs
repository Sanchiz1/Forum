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
    public class RemoveUserRoleCommand : IRequest
    {
        public int User_Id { get; set; }
        public int Role_Id { get; set; }
    }
    public class RemoveUserRoleCommandHandler : IRequestHandler<RemoveUserRoleCommand>
    {
        private readonly IUserRepository _context;

        public RemoveUserRoleCommandHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken) => await _context.RemoveUserRoleAsync(request);
    }
}
