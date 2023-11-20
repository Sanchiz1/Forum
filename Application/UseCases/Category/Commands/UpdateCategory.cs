using Application.Common.Interfaces.Repositories;
using Application.UseCases.Comments.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Category.Commands
{
    public class UpdateCategoryCommand : IRequest
    {
        public string Title { get; set; }
    }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _context;

        public UpdateCategoryCommandHandler(ICategoryRepository context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken) => await _context.UpdateCategoryAsync(request);
    }
}
