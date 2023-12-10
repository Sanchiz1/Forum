using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Common.Interfaces.Repositories;
using Application.UseCases.Posts.Commands;
using FluentValidation;
using Application.Common.Exceptions;

namespace Application.UseCases.Comments.Commands
{
    public class CreateCommentCommand : IRequest
    {
        public string Text { get; set; }
        public int Post_Id { get; set; }
        public int User_Id { get; set; }
    }
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
    {
        private readonly ICommentRepository _context;

        public CreateCommentCommandHandler(ICommentRepository context)
        {
            _context = context;
        }

        public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            if (request.User_Id == 0)
            {
                throw new PermissionException();
            }
            await _context.CreateCommentAsync(request);
        }
    }
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(c => c.Text)
                .MaximumLength(500)
                .NotEmpty();
            RuleFor(c => c.Post_Id)
                .NotEmpty();
            RuleFor(c => c.User_Id)
                .NotEmpty();
        }
    }
}