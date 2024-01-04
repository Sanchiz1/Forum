using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Common.Interfaces.Repositories;
using FluentValidation;
using Application.Common.Models;

namespace Application.UseCases.Posts.Commands
{
    public class LikePostCommand : IRequest<Result<string>>
    {
        public int Post_Id { get; set; }
        public int User_Id { get; set; }
    }
    public class LikePostCommandHandler : IRequestHandler<LikePostCommand, Result<string>>
    {
        private readonly IPostRepository _context;

        public LikePostCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(LikePostCommand request, CancellationToken cancellationToken) 
        { 
            await _context.LikePostAsync(request);

            return "Post liked successfully";
        }
    }
    public class LikePostCommandValidator : AbstractValidator<LikePostCommand>
    {
        public LikePostCommandValidator()
        {
            RuleFor(c => c.Post_Id)
                .NotEmpty();
            RuleFor(c => c.User_Id)
                .NotEmpty();
        }
    }
}