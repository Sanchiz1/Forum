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
                .Resolve(context => repo.GetPosts());

            Field<ListGraphType<PostGraphType>>("pagedPosts")
                .Argument<NonNullGraphType<IntGraphType>>("offset")
                .Argument<NonNullGraphType<IntGraphType>>("next")
                .Argument<NonNullGraphType<StringGraphType>>("order")
                .Resolve(context =>
                {
                    int offset = context.GetArgument<int>("offset");
                    int next = context.GetArgument<int>("next");
                    string order = context.GetArgument<string>("order");
                    return repo.GetPagedSortedPosts(offset, next, order);
                });

            Field<PostGraphType>("post")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .Resolve(context =>
                {
                    int id = context.GetArgument<int>("id");
                    return repo.GetPostById(id);
                });
        }
    }
}
