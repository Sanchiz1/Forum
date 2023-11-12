using Application.Common.Interfaces;
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
    public class UpdateCommentCommand : IRequest
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
    {
        private readonly ICommentRepository _context;

        public UpdateCommentCommandHandler(ICommentRepository context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken) => await _context.UpdateCommentAsync(request);
    }
}
