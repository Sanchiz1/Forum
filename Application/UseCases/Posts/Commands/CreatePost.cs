using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using Application.Common.Interfaces.Repositories;
using FluentValidation;
using Application.Common.Models;

namespace Application.UseCases.Posts.Commands
{
    public class CreatePostCommand : IRequest
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int User_Id { get; set; }
    }
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand>
    {
        private readonly IPostRepository _context;

        public CreatePostCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task Handle(CreatePostCommand request, CancellationToken cancellationToken) => await _context.CreatePostAsync(request);
    }
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(c => c.Title)
                .MaximumLength(100)
                .NotEmpty();
            RuleFor(c => c.Text)
                .MaximumLength(500);
            RuleFor(c => c.User_Id)
                .NotEmpty();
        }
    }
}