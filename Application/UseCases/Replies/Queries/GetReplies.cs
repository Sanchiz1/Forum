using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Common.ViewModels;
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
    public class GetRepliesQuery : IRequest<Result<List<ReplyViewModel>>>
    {
        public int Comment_Id { get; set; }
        public int Next { get; set; }
        public int Offset { get; set; }
        public DateTime User_Timestamp { get; set; }
        public string Order { get; set; } = "Date";
        public int User_Id { get; set; } = 0;
    }
    public class GetRepliesQueryHandler : IRequestHandler<GetRepliesQuery, Result<List<ReplyViewModel>>>
    {
        private readonly IReplyRepository _context;

        public GetRepliesQueryHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task<Result<List<ReplyViewModel>>> Handle(GetRepliesQuery request, CancellationToken cancellationToken) => await _context.GetRepliesAsync(request);
    }
}
