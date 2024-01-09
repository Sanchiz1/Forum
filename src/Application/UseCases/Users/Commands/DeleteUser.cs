using Application.Common.Constants;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Common.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Commands
{
    public record DeleteUserCommand : IRequest<Result<string>>
    {
        public string Password { get; set; }
        public int User_Id { get; set; }
        public int Account_Id { get; set; } = 0;
        public string Account_Role { get; set; } = "";
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<string>>
    {
        private readonly IUserRepository _context;
        private readonly IHashingService _hasher;

        public DeleteUserCommandHandler(IUserRepository context, IHashingService hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public async Task<Result<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Account_Role != Role.Admin && request.User_Id != request.Account_Id)
            {
                return new Exception("Permission denied");
            }

            string userSalt = await _context.GetUserSaltAsync(request.User_Id);
            string userPassword = _hasher.ComputeHash(request.Password, userSalt);

            if (userPassword != await _context.GetUserPasswordAsync(request.User_Id))
            {
                return new Exception("Wrong password");
            }
            await _context.DeleteUserAsync(request);

            return "Account has been deleted successfully";
        }
    }
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(c => c.User_Id)
                .NotEmpty();
        }
    }
}
