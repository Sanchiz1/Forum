using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Replies.Commands
{
    public class UpdateReplyCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Account_Id { get; set; } = 0;
        public string Account_Role { get; set; } = "";
    }
    public class UpdateReplyCommandHandler : IRequestHandler<UpdateReplyCommand, Result<string>>
    {
        private readonly IReplyRepository _context;

        public UpdateReplyCommandHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(UpdateReplyCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.GetReplyByIdAsync(new Queries.GetReplyByIdQuery() { Id = request.Id });

            if (post.Reply.User_Id != request.Account_Id)
            {
                return new Exception("Permission denied");
            }

            await _context.UpdateReplyAsync(request);

            return "Reply updated succesfully";
        }
    }
    public class UpdateReplyCommandValidator : AbstractValidator<UpdateReplyCommand>
    {
        public UpdateReplyCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
            RuleFor(c => c.Text)
                .MaximumLength(1000)
                .NotEmpty();
        }
    }
}
