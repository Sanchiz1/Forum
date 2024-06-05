using Application.UseCases.Posts.Queries;
using Forum.GraphQL.Types.PostTypes;
using Forum.GraphQL.Types.UserTypes;
using Forum.Helpers;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Forum.GraphQL.Queries
{
    public class PostQuery : ObjectGraphType
    {
        private readonly IMediator _mediator;
        public PostQuery(IMediator mediator)
        {
            _mediator = mediator;

            Field<ListGraphType<PostGraphType>>("searchedPosts")
                .Argument<NonNullGraphType<GetSearchedPostsInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetSearchedPostsQuery query = context.GetArgument<GetSearchedPostsQuery>("input");

                    query.User_Id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<ListGraphType<PostGraphType>>("posts")
                .Argument<NonNullGraphType<GetPostsInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetPostsQuery query = context.GetArgument<GetPostsQuery>("input");

                    query.User_Id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<ListGraphType<PostGraphType>>("userPosts")
                .Argument<NonNullGraphType<GetUserPostsInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetUserPostsQuery query = context.GetArgument<GetUserPostsQuery>("input");

                    query.User_Id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<PostGraphType>("post")
                .Argument<NonNullGraphType<GetPostByIdInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetPostByIdQuery query = context.GetArgument<GetPostByIdQuery>("input");

                    query.User_id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
        }
    }
}
