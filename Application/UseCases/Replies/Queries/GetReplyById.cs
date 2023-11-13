﻿using Application.Common.Interfaces.Repositories;
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
    public class GetReplyByIdQuery : IRequest<Reply>
    {
        public int Id { get; set; }
    }
    public class GetReplyByIdQueryHandler : IRequestHandler<GetReplyByIdQuery, Reply>
    {
        private readonly IReplyRepository _context;

        public GetReplyByIdQueryHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task<Reply> Handle(GetReplyByIdQuery request, CancellationToken cancellationToken) => await _context.GetReplyByIdAsync(request);
    }
}