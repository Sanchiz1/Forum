using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Replies.Commands
{
    public class CreateReplyCommand : IRequest<Result<string>>
    {
        public string Text { get; set; }
        public int Comment_Id { get; set; }
        public int? Reply_User_Id { get; set; }
        public int User_Id { get; set; }
    }
    public class CreateReplyCommandHandler : IRequestHandler<CreateReplyCommand, Result<string>>
    {
        private readonly IReplyRepository _context;

        public CreateReplyCommandHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(CreateReplyCommand request, CancellationToken cancellationToken)
        {
            if (request.User_Id == 0)
            {
                return new Exception("Permission denied");
            }
            await _context.CreateReplyAsync(request);

            return "Reply created succesfully";
        }
    }
    public class CreateReplyCommandValidator : AbstractValidator<CreateReplyCommand>
    {
        public CreateReplyCommandValidator()
        {
            RuleFor(c => c.Text)
                .MaximumLength(1000)
                .NotEmpty();
            RuleFor(c => c.Comment_Id)
                .NotEmpty();
            RuleFor(c => c.User_Id)
                .NotEmpty();
        }
    }
}
