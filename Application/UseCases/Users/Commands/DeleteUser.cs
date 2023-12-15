using Application.Common.Constants;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
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
    public class DeleteUserCommand : IRequest
    {
        public string Password { get; set; }
        public int User_Id { get; set; }
        public int Account_Id { get; set; } = 0;
        public string Account_Role { get; set; } = "";
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _context;

        public DeleteUserCommandHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken) 
        {
            if(request.Account_Role == Role.Admin)
            {
                await _context.DeleteUserAsync(request);
                return;
            }
            if(request.User_Id != request.Account_Id)
            {
                throw new PermissionException();
            }
            if(!(await _context.CheckUserPasswordAsync(request.Password, request.User_Id)))
            {
                throw new WrongPasswordException();
            }
            await _context.DeleteUserAsync(request); 
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
