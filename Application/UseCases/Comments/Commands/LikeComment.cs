using Application.Common.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Comments.Commands
{
    public record LikeCommentCommand : IRequest
    {
        public int Comment_Id { get; set; }
        public int User_Id { get; set; }
    }
    public class LikeCommentCommandHandler : IRequestHandler<LikeCommentCommand>
    {
        private readonly ICommentRepository _context;

        public LikeCommentCommandHandler(ICommentRepository context)
        {
            _context = context;
        }

        public async Task Handle(LikeCommentCommand request, CancellationToken cancellationToken) => await _context.LikeCommentAsync(request);
    }
}
