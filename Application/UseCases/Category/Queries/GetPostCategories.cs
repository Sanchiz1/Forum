using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Category.Queries
{
    public class GetPostCategoriesQuery : IRequest<List<CategoryDto>>
    {
        public int Post_Id { get; set; }
        public int Next { get; set; }
        public int Offset { get; set; }
        public string Order { get; set; } = "Date";
    }
    public class GetPostCategoriesQueryHandler : IRequestHandler<GetPostCategoriesQuery, List<CategoryDto>>
    {
        private readonly ICategoryRepository _context;

        public GetPostCategoriesQueryHandler(ICategoryRepository context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> Handle(GetPostCategoriesQuery request, CancellationToken cancellationToken) => await _context.GetPostCategoriesAsync(request);
    }
}
