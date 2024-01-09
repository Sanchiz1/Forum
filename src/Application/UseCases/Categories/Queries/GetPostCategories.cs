using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Categories.Queries
{
    public class GetPostCategoriesQuery : IRequest<Result<List<CategoryDto>>>
    {
        public int Post_Id { get; set; }
    }
    public class GetPostCategoriesQueryHandler : IRequestHandler<GetPostCategoriesQuery, Result<List<CategoryDto>>>
    {
        private readonly ICategoryRepository _context;

        public GetPostCategoriesQueryHandler(ICategoryRepository context)
        {
            _context = context;
        }

        public async Task<Result<List<CategoryDto>>> Handle(GetPostCategoriesQuery request, CancellationToken cancellationToken) => await _context.GetPostCategoriesAsync(request);
    }
}
