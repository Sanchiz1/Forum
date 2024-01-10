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
    public class GetPostCategoriesQuery : IRequest<Result<List<CategoryDto>>>
    {
        public int Post_Id { get; set; }
    }
    public class GetPostCategoriesQueryHandler : IRequestHandler<GetPostCategoriesQuery, Result<List<CategoryDto>>>
    {
        private readonly ICategoryRepository _context;
        private readonly IMapper _mapper;

        public GetPostCategoriesQueryHandler(ICategoryRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<CategoryDto>>> Handle(GetPostCategoriesQuery request, CancellationToken cancellationToken) 
            => _mapper.Map<List<CategoryDto>>(await _context.GetPostCategoriesAsync(request));
    }
}
