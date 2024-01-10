using Application.Common.DTOs.ViewModels;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Comments.Queries
{
    public class GetCommentsQuery : IRequest<Result<List<CommentViewModelDto>>>
    {
        public int Post_Id { get; set; }
        public int Next { get; set; }
        public int Offset { get; set; }
        public DateTime User_Timestamp { get; set; }
        public string Order { get; set; } = "Date";
        public int User_Id { get; set; } = 0;
    }
    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, Result<List<CommentViewModelDto>>>
    {
        private readonly ICommentRepository _context;
        private readonly IMapper _mapper;

        public GetCommentsQueryHandler(ICommentRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<CommentViewModelDto>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken) 
            => _mapper.Map<List<CommentViewModelDto>>(await _context.GetCommentsAsync(request));
    }
}
