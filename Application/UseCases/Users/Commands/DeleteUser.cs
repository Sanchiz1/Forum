using Application.Common.Constants;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Commands
{
    public class DeleteUserCommand : IRequest<Result<string>>
    {
        public string Password { get; set; }
        public int User_Id { get; set; }
        public int Account_Id { get; set; } = 0;
        public string Account_Role { get; set; } = "";
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<string>>
    {
        private readonly IUserRepository _context;

        public DeleteUserCommandHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Account_Role != Role.Admin && request.User_Id != request.Account_Id)
            {
                return new Exception("You don`t have permissions for that action");
            }
            if (!(await _context.CheckUserPasswordAsync(request.Password, request.Account_Id)))
            {
                throw new WrongPasswordException();
            }
            await _context.DeleteUserAsync(request);

            return "Account has been deleted succesfully";
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
