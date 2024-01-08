using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Common.ViewModels;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Posts.Queries
{
    public record GetPostByIdQuery : IRequest<Result<PostViewModel>>
    {
        public int Id { get; set; }
        public int User_id { get; set; } = 0;
    }
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Result<PostViewModel>>
    {
        private readonly IPostRepository _postContext;
        private readonly ICategoryRepository _categoryContext;

        public GetPostByIdQueryHandler(IPostRepository postContext, ICategoryRepository categoryContext)
        {
            _postContext = postContext;
            _categoryContext = categoryContext;
        }

        public async Task<Result<PostViewModel>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postContext.GetPostByIdAsync(request);
            if (post == null) return post;
            post.Categories = await _categoryContext.GetPostCategoriesAsync(new Categories.Queries.GetPostCategoriesQuery { Post_Id = post.Post.Id });
            return post;
        }
    }
}
