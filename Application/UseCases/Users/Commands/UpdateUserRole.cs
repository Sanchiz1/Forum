using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Commands
{
    public class UpdateUserRoleCommand : IRequest
    {
        public int User_Id { get; set; }
        public int? Role_Id { get; set; }
        public string Account_Role { get; set; } = "";
    }
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand>
    {
        private readonly IUserRepository _context;

        public UpdateUserRoleCommandHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.Account_Role != "Admin")
            {
                throw new PermissionException();
            }
            await _context.UpdateUserRoleAsync(request);
        }
    }
    public class UpdateUserRoleCommandValidator : AbstractValidator<UpdateUserRoleCommand>
    {
        public UpdateUserRoleCommandValidator()
        {
            RuleFor(c => c.User_Id)
                .NotEmpty();
        }
    }
}
