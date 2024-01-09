using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Common.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Replies.Queries
{
    public class GetReplyByIdQuery : IRequest<Result<ReplyViewModel>>
    {
        public int Id { get; set; }
        public int User_Id { get; set; } = 0;
    }
    public class GetReplyByIdQueryHandler : IRequestHandler<GetReplyByIdQuery, Result<ReplyViewModel>>
    {
        private readonly IReplyRepository _context;

        public GetReplyByIdQueryHandler(IReplyRepository context)
        {
            _context = context;
        }

        public async Task<Result<ReplyViewModel>> Handle(GetReplyByIdQuery request, CancellationToken cancellationToken) => await _context.GetReplyByIdAsync(request);
    }
}
