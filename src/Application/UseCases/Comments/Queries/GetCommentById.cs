using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Common.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Comments.Queries
{
    public class GetCommentByIdQuery : IRequest<Result<CommentViewModel>>
    {
        public int Id { get; set; }
        public int User_id { get; set; } = 0;
    }
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, Result<CommentViewModel>>
    {
        private readonly ICommentRepository _context;

        public GetCommentByIdQueryHandler(ICommentRepository context)
        {
            _context = context;
        }

        public async Task<Result<CommentViewModel>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken) => await _context.GetCommentByIdAsync(request);
    }
}
