using Application.Common.DTOs.ViewModels;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Comments.Queries
{
    public class GetCommentByIdQuery : IRequest<Result<CommentViewModelDto>>
    {
        public int Id { get; set; }
        public int User_id { get; set; } = 0;
    }
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, Result<CommentViewModelDto>>
    {
        private readonly ICommentRepository _context;
        private readonly IMapper _mapper;

        public GetCommentByIdQueryHandler(ICommentRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<CommentViewModelDto>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken) 
            => _mapper.Map<CommentViewModelDto>(await _context.GetCommentByIdAsync(request));
    }
}
