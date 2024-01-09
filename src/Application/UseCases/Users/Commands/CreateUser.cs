using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Common.Models;
using Application.UseCases.Users.Queries;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Commands
{
    public record CreateUserCommand : IRequest<Result<string>>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string Password { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
    {
        private readonly IUserRepository _context;
        private readonly IHashingService _hasher;

        public CreateUserCommandHandler(IUserRepository context, IHashingService hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _context.GetUserByUsernameAsync(new GetUserByUsernameQuery() { Username = request.Username }) != null)
            {
                return new Exception("User with this username already exists");
            }

            if (await _context.GetUserByEmailAsync(new GetUserByEmailQuery() { Email = request.Username }) != null)
            {
                return new Exception("User with this email already exists");
            }

            string Salt = _hasher.GenerateSalt();
            string Password = _hasher.ComputeHash(request.Password, Salt);
            await _context.CreateUserAsync(request.Username, request.Email, request.Bio, Password, Salt);

            return "Account has been created successfully";
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
