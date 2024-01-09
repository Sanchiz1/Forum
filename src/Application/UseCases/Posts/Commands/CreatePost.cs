using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using Application.Common.Interfaces.Repositories;
using FluentValidation;
using Application.Common.Models;

namespace Application.UseCases.Posts.Commands
{
    public record CreatePostCommand : IRequest<Result<string>>
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int Account_Id { get; set; }
    }
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Result<string>>
    {
        private readonly IPostRepository _context;

        public CreatePostCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(CreatePostCommand request, CancellationToken cancellationToken) 
        { 
            if(request.Account_Id == 0)
            {
                return new Exception("Permission denied");
            }
            await _context.CreatePostAsync(request);

            return "Post has been created successfully";
        }
    }
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(c => c.Title)
                .MaximumLength(100)
                .NotEmpty();
            RuleFor(c => c.Text)
                .MaximumLength(2000);
            RuleFor(c => c.Account_Id)
                .NotEmpty();
        }
    }
}