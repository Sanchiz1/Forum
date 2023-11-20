using Application.Common.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Category.Commands
{
    public class DeleteCategoryCommand : IRequest
    {
        public string Title { get; set; }
    }
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _context;

        public DeleteCategoryCommandHandler(ICategoryRepository context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) => await _context.DeleteCategoryAsync(request);
    }
}
