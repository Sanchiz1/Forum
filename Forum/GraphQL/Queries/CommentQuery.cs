using Application.UseCases.Comments.Queries;
using Application.UseCases.Posts.Queries;
using Forum.GraphQL.Types.CommentTypes;
using Forum.GraphQL.Types.UserTypes;
using Forum.Helpers;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Forum.GraphQL.Queries
{
    public class CommentQuery : ObjectGraphType
    {
        private readonly IMediator _mediator;
        public CommentQuery(IMediator mediator)
        {
            _mediator = mediator;

            Field<ListGraphType<CommentGraphType>>("comments")
                .Argument<NonNullGraphType<GetCommentsInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetCommentsQuery getCommentsQuery = new GetCommentsQuery()
                    {
                        Next = context.GetArgument<GetPostsQuery>("input").Next,
                        Offset = context.GetArgument<GetPostsQuery>("input").Offset,
                        Order = context.GetArgument<GetPostsQuery>("input").Order,
                        User_Timestamp = context.GetArgument<GetPostsQuery>("input").User_Timestamp,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };
                    return await _mediator.Send(getCommentsQuery);
                });
            Field<PostGraphType>("post")
                .Argument<NonNullGraphType<GetCommentByIdInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetCommentByIdQuery getCommentByIdQuery = new GetCommentByIdQuery()
                    {
                        Id = context.GetArgument<GetCommentByIdQuery>("input").Id,
                        User_id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };
                    return await _mediator.Send(getCommentByIdQuery);
                });
        }
    }
}
