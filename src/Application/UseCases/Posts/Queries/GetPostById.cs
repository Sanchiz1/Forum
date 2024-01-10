using Application.Common.DTOs.ViewModels;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Posts.Queries
{
    public record GetPostByIdQuery : IRequest<Result<PostViewModelDto>>
    {
        public int Id { get; set; }
        public int User_id { get; set; } = 0;
    }
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Result<PostViewModelDto>>
    {
        private readonly IPostRepository _postContext;
        private readonly ICategoryRepository _categoryContext;
        private readonly IMapper _mapper;

        public GetPostByIdQueryHandler(IPostRepository postContext, ICategoryRepository categoryContext, IMapper mapper)
        {
            _postContext = postContext;
            _categoryContext = categoryContext;
            _mapper = mapper;
        }

        public async Task<Result<PostViewModelDto>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postContext.GetPostByIdAsync(request.Id);
            if (post != null)
            {
                post.Categories = await _categoryContext.GetPostCategoriesAsync(new Categories.Queries.GetPostCategoriesQuery { Post_Id = post.Post.Id });
            }
            return _mapper.Map<PostViewModelDto>(post);
        }
    }
}
