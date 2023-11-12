using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Replies.Queries
{
    public class GetRepliesQuery : IRequest<List<Reply>>
    {
        public int Comment_Id { get; set; }
        public int Next { get; set; }
        public int Offset { get; set; }
        public DateTime User_timestamp { get; set; }
        public string Order = "Date";
        public int User_id = 0;
    }
    public class GetRepliesQueryHandler : IRequestHandler<GetRepliesQuery, List<Reply>>
    {
        private readonly IReplyRepository _context;

        public GetRepliesQueryHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task<List<Reply>> Handle(GetRepliesQuery request, CancellationToken cancellationToken) => await _context.GetRepliesAsync(request);
    }
}
