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

namespace Application.UseCases.Replies.Commands
{
    public class LikeReplyCommand : IRequest<Result<string>>
    {
        public int Reply_Id { get; set; }
        public int User_Id { get; set; }
    }
    public class LikeReplyCommandHandler : IRequestHandler<LikeReplyCommand, Result<string>>
    {
        private readonly IReplyRepository _context;

        public LikeReplyCommandHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(LikeReplyCommand request, CancellationToken cancellationToken)
        {
            await _context.LikeReplyAsync(request);

            return "Reply liked succesfully";
        }
    }
    public class LikeReplyCommandValidator : AbstractValidator<LikeReplyCommand>
    {
        public LikeReplyCommandValidator()
        {
            RuleFor(c => c.Reply_Id)
                .NotEmpty();
            RuleFor(c => c.User_Id)
                .NotEmpty();
        }
    }
}
