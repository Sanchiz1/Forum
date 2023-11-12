using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Common.Interfaces.Repositories;

namespace Application.UseCases.Comments.Commands
{
    public class CreateCommentCommand : IRequest
    {
        public string Text { get; set; }
        public int Post_Id { get; set; }
        public int User_Id { get; set; }
    }
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
    {
        private readonly ICommentRepository _context;

        public CreateCommentCommandHandler(ICommentRepository context)
        {
            _context = context;
        }

        public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken) => await _context.CreateCommentAsync(request);
    }
}