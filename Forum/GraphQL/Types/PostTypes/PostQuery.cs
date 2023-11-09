using Forum.Data.Repositories.Interfaces;
using Forum.GraphQL.Types.UserTypes;
using Forum.Helpers;
using Forum.Models;
using GraphQL;
using GraphQL.Types;

namespace Forum.GraphQL.Types.PostTypes
{
    public class PostQuery : ObjectGraphType
    {
        private readonly IPostRepository repo;
        public PostQuery(IPostRepository Repo)
        {
            repo = Repo;

            Field<ListGraphType<PostGraphType>>("posts")
                .Resolve(context =>
                {
                    var userId = AccountHelper.GetUserIdFromClaims(context.User!);
                    return repo.GetPosts(userId);
                });

            Field<ListGraphType<PostGraphType>>("pagedPosts")
                .Argument<NonNullGraphType<IntGraphType>>("offset")
                .Argument<NonNullGraphType<IntGraphType>>("next")
                .Argument<NonNullGraphType<StringGraphType>>("order")
                .Argument<NonNullGraphType<DateTimeGraphType>>("user_timestamp")
                .Resolve(context =>
                {
                    int offset = context.GetArgument<int>("offset");
                    int next = context.GetArgument<int>("next");
                    string order = context.GetArgument<string>("order");
                    DateTime user_timestamp = context.GetArgument<DateTime>("user_timestamp");
                    var userId = AccountHelper.GetUserIdFromClaims(context.User!);
                    return repo.GetPagedSortedPosts(next, offset, user_timestamp, order, userId);
                });

            Field<PostGraphType>("post")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .Resolve(context =>
                {
                    int id = context.GetArgument<int>("id");
                    var userId = AccountHelper.GetUserIdFromClaims(context.User!);
                    return repo.GetPostById(id);
                });
        }
    }
}
