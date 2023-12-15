using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Common.Interfaces.Repositories;
using FluentValidation;
using Application.Common.Exceptions;
using Application.UseCases.Posts.Queries;

namespace Application.UseCases.Posts.Commands
{
    public class UpdatePostCommand : IRequest
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Account_Id { get; set; } = 0;
        public string Account_Role { get; set; } = "";
    }
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
    {
        private readonly IPostRepository _context;

        public UpdatePostCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.GetPostByIdAsync(new GetPostByIdQuery() { Id = request.Id });

            if (post.Post.User_Id != request.Account_Id)
            {
                throw new PermissionException();
            }

            await _context.UpdatePostAsync(request);
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