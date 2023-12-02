using Application.Common.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Commands
{
    public class UpdateUserRoleCommand : IRequest
    {
        public int User_Id { get; set; }
        public int? Role_Id { get; set; }
    }
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand>
    {
        private readonly IUserRepository _context;

        public UpdateUserRoleCommandHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken) => await _context.UpdateUserRoleAsync(request);
    }
}
