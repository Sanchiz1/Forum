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
                    GetSearchedPostsQuery query = new GetSearchedPostsQuery()
                    {
                        Next = context.GetArgument<GetSearchedPostsQuery>("input").Next,
                        Offset = context.GetArgument<GetSearchedPostsQuery>("input").Offset,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        User_Timestamp = context.GetArgument<GetSearchedPostsQuery>("input").User_Timestamp,
                        Search = context.GetArgument<GetSearchedPostsQuery>("input").Search
                    };


                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<ListGraphType<PostGraphType>>("posts")
                .Argument<NonNullGraphType<GetPostsInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetPostsQuery query = new GetPostsQuery()
                    {
                        Next = context.GetArgument<GetPostsQuery>("input").Next,
                        Offset = context.GetArgument<GetPostsQuery>("input").Offset,
                        Order = context.GetArgument<GetPostsQuery>("input").Order,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        User_Timestamp = context.GetArgument<GetPostsQuery>("input").User_Timestamp,
                        Categories = context.GetArgument<GetPostsQuery>("input").Categories,
                    };

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<ListGraphType<PostGraphType>>("userPosts")
                .Argument<NonNullGraphType<GetUserPostsInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetUserPostsQuery query = new GetUserPostsQuery()
                    {
                        Author_Username = context.GetArgument<GetUserPostsQuery>("input").Author_Username,
                        Next = context.GetArgument<GetUserPostsQuery>("input").Next,
                        Offset = context.GetArgument<GetUserPostsQuery>("input").Offset,
                        Order = context.GetArgument<GetUserPostsQuery>("input").Order,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        User_Timestamp = context.GetArgument<GetUserPostsQuery>("input").User_Timestamp,
                    };

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<PostGraphType>("post")
                .Argument<NonNullGraphType<GetPostByIdInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetPostByIdQuery query = new GetPostByIdQuery()
                    {
                        Id = context.GetArgument<GetPostByIdQuery>("input").Id,
                        User_id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
        }
    }
}
