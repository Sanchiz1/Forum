using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Common.Interfaces.Repositories;
using FluentValidation;
using Application.Common.Models;
using System;

namespace Application.UseCases.Comments.Commands
{
    public class CreateCommentCommand : IRequest<Result<string>>
    {
        public string Text { get; set; }
        public int Post_Id { get; set; }
        public int User_Id { get; set; }
    }
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Result<string>>
    {
        private readonly ICommentRepository _context;

        public CreateCommentCommandHandler(ICommentRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            if (request.User_Id == 0)
            {
                return new Exception("Permission denied");
            }
            await _context.CreateCommentAsync(request);

            return "Comment created successfully";
        }
    }
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(c => c.Text)
                .MaximumLength(1000)
                .NotEmpty();
            RuleFor(c => c.Post_Id)
                .NotEmpty();
            RuleFor(c => c.User_Id)
                .NotEmpty();
        }
    }
}