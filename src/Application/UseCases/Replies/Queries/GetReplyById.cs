using Application.Common.DTOs.ViewModels;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Replies.Queries
{
    public class GetReplyByIdQuery : IRequest<Result<ReplyViewModelDto>>
    {
        public int Id { get; set; }
        public int User_Id { get; set; } = 0;
    }
    public class GetReplyByIdQueryHandler : IRequestHandler<GetReplyByIdQuery, Result<ReplyViewModelDto>>
    {
        private readonly IReplyRepository _context;
        private readonly IMapper _mapper;

        public GetReplyByIdQueryHandler(IReplyRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ReplyViewModelDto>> Handle(GetReplyByIdQuery request, CancellationToken cancellationToken) 
            => _mapper.Map<ReplyViewModelDto>(await _context.GetReplyByIdAsync(request));
    }
}
