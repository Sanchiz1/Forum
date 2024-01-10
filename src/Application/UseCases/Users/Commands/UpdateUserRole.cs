using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Domain.Constants;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Commands
{
    public record UpdateUserRoleCommand : IRequest<Result<string>>
    {
        public int User_Id { get; set; }
        public int? Role_Id { get; set; }
        public string Account_Role { get; set; } = "";
    }
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, Result<string>>
    {
        private readonly IUserRepository _context;

        public UpdateUserRoleCommandHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.Account_Role != Roles.Admin)
            {
                return new Exception("Permission denied");
            }
            await _context.UpdateUserRoleAsync(request);
            return "Role has been updated succesfully";
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
