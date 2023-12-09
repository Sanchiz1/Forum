using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.UseCases.Categories.Commands;
using Application.UseCases.Users.Queries;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Commands
{
    public class CreateUserCommand : IRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string Password { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _context;

        public CreateUserCommandHandler(IUserRepository context)
        {
            _context = context;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _context.GetUserByUsernameAsync(new GetUserByUsernameQuery() { Username = request.Username}) != null)
            {
                throw new UserAlreadyExistsException("User with this username already exists");
            }

            if (await _context.GetUserByEmailAsync(new GetUserByEmailQuery() { Email = request.Username }) != null)
            {
                throw new UserAlreadyExistsException("User with this email already exists");
            }

            await _context.CreateUserAsync(request);
        }
    }
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
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
            RuleFor(c => c.Password)
                .MaximumLength(50)
                .MinimumLength(8)
                .NotEmpty();
        }
    }
}
