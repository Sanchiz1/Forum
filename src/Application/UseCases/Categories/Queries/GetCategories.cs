using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(ICategoryRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken) 
            => _mapper.Map<List<CategoryDto>>(await _context.GetCategoriesAsync(request));
    }
}
