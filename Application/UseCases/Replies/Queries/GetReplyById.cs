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

namespace Application.UseCases.Replies.Queries
{
    public class GetReplyByIdQuery : IRequest<ReplyViewModel>
    {
        public int Id { get; set; }
        public int User_Id { get; set; } = 0;
    }
    public class GetReplyByIdQueryHandler : IRequestHandler<GetReplyByIdQuery, ReplyViewModel>
    {
        private readonly IReplyRepository _context;

        public GetReplyByIdQueryHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task<ReplyViewModel> Handle(GetReplyByIdQuery request, CancellationToken cancellationToken) => await _context.GetReplyByIdAsync(request);
    }
}
