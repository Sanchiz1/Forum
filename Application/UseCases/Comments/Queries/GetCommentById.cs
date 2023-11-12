using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Comments.Queries
{
    public class GetCommentByIdQuery : IRequest<Comment>
    {
        public int Id { get; set; }
    }
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, Comment>
    {
        private readonly ICommentRepository _context;

        public GetCommentByIdQueryHandler(ICommentRepository context)
        {
            _context = context;
        }

        public async Task<Comment> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken) => await _context.GetCommentByIdAsync(request);
    }
}
