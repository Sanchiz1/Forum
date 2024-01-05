using Application.Common.Constants;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
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
    public class ChangeUserPasswordCommand : IRequest<Result<string>>
    {
        public string Password { get; set; }
        public string New_Password { get; set; }
        public int User_Id { get; set; }
    }

    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, Result<string>>
    {
        private readonly IUserRepository _context;
        private readonly IHashingService _hasher;

        public ChangeUserPasswordCommandHandler(IUserRepository context, IHashingService hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public async Task<Result<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            string userSalt = await _context.GetUserSaltAsync(request.User_Id);
            string userPassword = _hasher.ComputeHash(request.Password, userSalt);

            if (userPassword != await _context.GetUserPasswordAsync(request.User_Id))
            {
                return new Exception("Wrong password");
            }


            string Salt = _hasher.GenerateSalt();
            string Password = _hasher.ComputeHash(request.New_Password, Salt);
            await _context.ChangeUserPasswordAsync(Password, Salt, request.User_Id);
            return "Password has been changed successfully";
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
