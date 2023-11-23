using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.UseCases.Users.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Commands
{
    public class AddUserRoleCommand : IRequest
    {
        public int User_Id { get; set; }
        public int Role_Id { get; set; }
    }
    public class AddUserRoleCommandHandler : IRequestHandler<AddUserRoleCommand>
    {
        private readonly IUserRepository _context;

        public AddUserRoleCommandHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task Handle(AddUserRoleCommand request, CancellationToken cancellationToken) => await _context.AddUserRoleAsync(request);
    }
}
