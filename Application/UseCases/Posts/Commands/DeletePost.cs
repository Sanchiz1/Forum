using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Common.Interfaces.Repositories;

namespace Application.UseCases.Posts.Commands
{
    public class DeletePostCommand : IRequest
    {
        public int Id {  get; set; }
    }
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IPostRepository _context;

        public DeletePostCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken) => await _context.DeletePostAsync(request);
    }
}