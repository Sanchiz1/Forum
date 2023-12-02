using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Categories.Queries
{
    public class GetCategoriesQuery : IRequest<List<CategoryDto>>
    {
        public int Next { get; set; }
        public int Offset { get; set; }
    }
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
    {
        private readonly ICategoryRepository _context;

        public GetCategoriesQueryHandler(ICategoryRepository context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken) => await _context.GetCategoriesAsync(request);
    }
}
