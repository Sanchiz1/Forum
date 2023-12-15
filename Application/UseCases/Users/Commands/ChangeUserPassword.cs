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
    public class ChangeUserPasswordCommand : IRequest
    {
        public string Password { get; set; }
        public string New_Password { get; set; }
        public int User_Id { get; set; }
    }

    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand>
    {
        private readonly IUserRepository _context;

        public ChangeUserPasswordCommandHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!(await _context.CheckUserPasswordAsync(request.Password, request.User_Id)))
            {
                throw new WrongPasswordException();
            }
            await _context.ChangeUserPasswordAsync(request);
        }
    }
    public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordCommandValidator()
        {
            RuleFor(c => c.Password)
                .MaximumLength(50)
                .MinimumLength(8)
                .NotEmpty();
            RuleFor(c => c.User_Id)
                .NotEmpty();
        }
    }
}
