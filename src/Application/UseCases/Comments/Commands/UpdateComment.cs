using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Comments.Commands
{
    public class UpdateCommentCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Account_Id { get; set; } = 0;
        public string Account_Role { get; set; } = "";
    }
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Result<string>>
    {
        private readonly ICommentRepository _context;

        public UpdateCommentCommandHandler(ICommentRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.GetCommentByIdAsync(new Queries.GetCommentByIdQuery() { Id = request.Id });

            if (post.Comment.User_Id != request.Account_Id)
            {
                return new Exception("Permission denied");
            }

            await _context.UpdateCommentAsync(request);

            return "Comment updated successfully";
        }
    }
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(c => c.Text)
                .MaximumLength(1000)
                .NotEmpty();
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
