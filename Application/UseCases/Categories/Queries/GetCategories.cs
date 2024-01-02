using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
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
    public class GetCategoriesQuery : IRequest<Result<List<CategoryDto>>>
    {
        public int Next { get; set; }
        public int Offset { get; set; }
        public string Search { get; set; } = "%";
    }
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Result<List<CategoryDto>>>
    {
        private readonly ICategoryRepository _context;

        public GetCategoriesQueryHandler(ICategoryRepository context)
        {
            _context = context;
        }

        public async Task<Result<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken) => await _context.GetCategoriesAsync(request);
    }
}
