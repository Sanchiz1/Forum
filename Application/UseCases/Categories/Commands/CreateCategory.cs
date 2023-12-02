using Application.Common.Interfaces.Repositories;
using Application.UseCases.Comments.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Categories.Commands
{
    public class CreateCategoryCommand : IRequest
    {
        public string Title { get; set; }
    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _context;

        public CreateCategoryCommandHandler(ICategoryRepository context)
        {
            _context = context;
        }

        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken) => await _context.CreateCategoryAsync(request);
    }
}
