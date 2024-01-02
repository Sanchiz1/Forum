using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<Result<List<CategoryDto>>>
    {
    }
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<List<CategoryDto>>>
    {
        private readonly ICategoryRepository _context;

        public GetAllCategoriesQueryHandler(ICategoryRepository context)
        {
            _context = context;
        }

        public async Task<Result<List<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken) => await _context.GetAllCategoriesAsync(request);
    }
}
