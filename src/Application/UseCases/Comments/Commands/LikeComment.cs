using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Comments.Commands
{
    public class LikeCommentCommand : IRequest<Result<string>>
    {
        public int Comment_Id { get; set; }
        public int User_Id { get; set; }
    }
    public class LikeCommentCommandHandler : IRequestHandler<LikeCommentCommand, Result<string>>
    {
        private readonly ICommentRepository _context;

        public LikeCommentCommandHandler(ICommentRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(LikeCommentCommand request, CancellationToken cancellationToken)
        {
            await _context.LikeCommentAsync(request);

            return "Comment liked successfully";
        }
    }
    public class LikeCommentCommandValidator : AbstractValidator<LikeCommentCommand>
    {
        public LikeCommentCommandValidator()
        {
            RuleFor(c => c.Comment_Id)
                .NotEmpty();
            RuleFor(c => c.User_Id)
                .NotEmpty();
        }
    }
}
