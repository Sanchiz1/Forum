using Forum.Data.Repositories.Interfaces;
using Forum.GraphQL.Types.UserTypes;
using Forum.Helpers;
using Forum.Models;
using GraphQL;
using GraphQL.Types;

namespace Forum.GraphQL.Types
{
    public class PostQuery : ObjectGraphType
    {
        private readonly IPostRepository repo;
        public PostQuery(IPostRepository Repo)
        {
            repo = Repo;

            Field<ListGraphType<PostGraphType>>("posts")
                .ResolveAsync(async context => repo.GetPosts());

            Field<PostGraphType>("post")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context =>
                {
                    int id = context.GetArgument<int>("id");
                    return repo.GetPostById(id);
                });
        }
    }
}
