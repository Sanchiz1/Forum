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
    public class GetCategoryByIdQuery : IRequest<Result<CategoryDto>>
    {
        public int Id { get; set; }
    }
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
    {
        private readonly ICategoryRepository _context;

        public GetCategoryByIdQueryHandler(ICategoryRepository context)
        {
            _context = context;
        }

        public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken) => await _context.GetCategoryByIdAsync(request);
    }
}
