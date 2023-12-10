using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.UseCases.Users.Queries;
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
    public class UpdateUserCommand : IRequest
    {
        public int User_Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _context;

        public UpdateUserCommandHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userCheck = (await _context.GetUserByIdAsync(new GetUserByIdQuery() { User_Id = request.User_Id }));

            if (userCheck.User.Id != request.User_Id)
            {
                throw new PermissionException();
            }

            var usernameCheck = (await _context.GetUserByUsernameAsync(new GetUserByUsernameQuery() { Username = request.Username }));
            var emailCheck = (await _context.GetUserByEmailAsync(new GetUserByEmailQuery() { Email = request.Email }));

            if (!(usernameCheck?.User.Id == request.User_Id || usernameCheck == null))
            {
                throw new UserAlreadyExistsException("User with this username already exists");
            }

            if (!(emailCheck?.User.Id == request.User_Id || emailCheck == null))
            {
                throw new UserAlreadyExistsException("User with this email already exists");
            }

            await _context.UpdateUserAsync(request);
        }
    }
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(c => c.User_Id)
                .NotEmpty();
            RuleFor(c => c.Username)
                .Matches("[a-zA-Z0-9_.]+$")
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(c => c.Email)
                .Matches("(?=.{0,64}$)[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(c => c.Bio)
                .MaximumLength(100);
        }
    }
}