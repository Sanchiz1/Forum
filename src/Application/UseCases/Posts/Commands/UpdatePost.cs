using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Common.Interfaces.Repositories;
using FluentValidation;
using Application.UseCases.Posts.Queries;
using Application.Common.Models;
using System;

namespace Application.UseCases.Posts.Commands
{
    public record UpdatePostCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Account_Id { get; set; }
    }
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Result<string>>
    {
        private readonly IPostRepository _context;

        public UpdatePostCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.GetPostByIdAsync(request.Id);

            if (post.Post.User_Id != request.Account_Id)
            {
                return new Exception("Permission denied");
            }

            await _context.UpdatePostAsync(request);

            return "Post has been updated successfully";
        }
    }
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
            RuleFor(c => c.Text)
                .MaximumLength(2000);
        }
    }
}