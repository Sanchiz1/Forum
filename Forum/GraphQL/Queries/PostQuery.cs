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

            Field<ListGraphType<PostGraphType>>("posts")
                .Argument<NonNullGraphType<GetPostsInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetPostsQuery getPostsQuery = new GetPostsQuery()
                    {
                        Next = context.GetArgument<GetPostsQuery>("input").Next,
                        Offset = context.GetArgument<GetPostsQuery>("input").Offset,
                        Order = context.GetArgument<GetPostsQuery>("input").Order,
                        User_Timestamp = context.GetArgument<GetPostsQuery>("input").User_Timestamp,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };
                    return await _mediator.Send(getPostsQuery);
                });

            Field<PostGraphType>("post")
                .Argument<NonNullGraphType<GetPostByIdInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetPostByIdQuery getPostByIdQuery = new GetPostByIdQuery()
                    {
                        Id = context.GetArgument<GetPostByIdQuery>("input").Id,
                        User_id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };
                    return await _mediator.Send(getPostByIdQuery);
                });
        }
    }
}
