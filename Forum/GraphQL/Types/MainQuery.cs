using Forum.Data.Repositories.Interfaces;
using Forum.GraphQL.Types.CommentTypes;
using Forum.GraphQL.Types.PostTypes;
using Forum.GraphQL.Types.ReplyTypes;
using Forum.GraphQL.Types.UserTypes;
using Forum.Helpers;
using Forum.Models;
using GraphQL;
using GraphQL.Types;

namespace Forum.GraphQL.Types
{
    public class MainQuery : ObjectGraphType
    {
        private readonly IUserRepository repo;
        public MainQuery(IUserRepository Repo)
        {
            repo = Repo;
            Field<ListGraphType<UserGraphType>>("users")
                .ResolveAsync(async context => repo.GetUsers());

            Field<UserGraphType>("userById")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context =>
                {
                    int id = context.GetArgument<int>("id");
                    return repo.GetUserById(id);
                }); 

            Field<UserGraphType>("userByUsername")
                .Argument<NonNullGraphType<StringGraphType>>("username")
                .ResolveAsync(async context =>
                {
                    string username = context.GetArgument<string>("username");
                    return repo.GetUserByUsername(username);
                });

            Field<UserGraphType>("account")
                .ResolveAsync(async context =>
                {
                    var a = context.User!;
                    var userId = AccountHelper.GetUserIdFromClaims(context.User!);
                    var user = repo.GetUserById(userId);
                    return user;
                }).AuthorizeWithPolicy("Authorized");

            Field<PostQuery>("posts")
                .Resolve(context => new { });


            Field<CommentQuery>("comments")
                .Resolve(context => new { });

            Field<ReplyQuery>("replies")
                .Resolve(context => new { });
        }
    }
}
