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
    public record GetCommentsQuery : IRequest<List<Comment>>
    {
        public int Post_Id { get; set; }
        public int Next { get; set; }
        public int Offset { get; set; }
        public DateTime User_timestamp { get; set; }
        public string Order = "Date";
        public int User_id = 0;
    }
    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, List<Comment>>
    {
        private readonly ICommentRepository _context;

        public GetCommentsQueryHandler(ICommentRepository context)
        {
            _context = context;
        }

        public async Task<List<Comment>> Handle(GetCommentsQuery request, CancellationToken cancellationToken) => await _context.GetCommentsAsync(request);
    }
}
