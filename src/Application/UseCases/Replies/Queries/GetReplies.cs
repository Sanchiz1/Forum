using Application.Common.DTOs.ViewModels;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Common.ViewModels;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Replies.Queries
{
    public class GetRepliesQuery : IRequest<Result<List<ReplyViewModelDto>>>
    {
        public int Comment_Id { get; set; }
        public int Next { get; set; }
        public int Offset { get; set; }
        public DateTime User_Timestamp { get; set; }
        public string Order { get; set; } = "Date";
        public int User_Id { get; set; } = 0;
    }
    public class GetRepliesQueryHandler : IRequestHandler<GetRepliesQuery, Result<List<ReplyViewModelDto>>>
    {
        private readonly IReplyRepository _context;
        private readonly IMapper _mapper;

        public GetRepliesQueryHandler(IReplyRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<ReplyViewModelDto>>> Handle(GetRepliesQuery request, CancellationToken cancellationToken) 
            => _mapper.Map<List<UserViewModelDto>>(await _context.GetRepliesAsync(request));
    }
}
