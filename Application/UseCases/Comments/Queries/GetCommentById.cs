using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
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
    public class GetCommentByIdQuery : IRequest<CommentViewModel>
    {
        public int Id { get; set; }
        public int User_id { get; set; } = 0;
    }
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, CommentViewModel>
    {
        private readonly ICommentRepository _context;

        public GetCommentByIdQueryHandler(ICommentRepository context)
        {
            _context = context;
        }

        public async Task<CommentViewModel> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken) => await _context.GetCommentByIdAsync(request);
    }
}
