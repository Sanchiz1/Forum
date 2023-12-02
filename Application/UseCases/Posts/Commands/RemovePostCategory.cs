using Application.Common.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Posts.Commands
{
    public class RemovePostCategoryCommand : IRequest
    {
        public int Post_Id { get; set; }
        public int Category_Id { get; set; }
    }
    public class RemovePostCategoryCommandHandler : IRequestHandler<RemovePostCategoryCommand>
    {
        private readonly IPostRepository _context;

        public RemovePostCategoryCommandHandler(IPostRepository context)
        {
            _context = context;
        }

        public async Task Handle(RemovePostCategoryCommand request, CancellationToken cancellationToken) => await _context.RemovePostCategoryAsync(request);
    }
}
