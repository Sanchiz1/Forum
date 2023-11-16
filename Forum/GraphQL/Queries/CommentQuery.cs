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
        private readonly ILogger _logger;
        public CommentQuery(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;

            Field<ListGraphType<CommentGraphType>>("comments")
                .Argument<NonNullGraphType<GetCommentsInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetCommentsQuery getCommentsQuery = new GetCommentsQuery()
                    {
                        Post_Id = context.GetArgument<GetCommentsQuery>("input").Post_Id,
                        Next = context.GetArgument<GetCommentsQuery>("input").Next,
                        Offset = context.GetArgument<GetCommentsQuery>("input").Offset,
                        Order = context.GetArgument<GetCommentsQuery>("input").Order,
                        User_Timestamp = context.GetArgument<GetCommentsQuery>("input").User_Timestamp,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };
                    return await _mediator.Send(getCommentsQuery);
                });
            Field<CommentGraphType>("comment")
                .Argument<NonNullGraphType<GetCommentByIdInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetCommentByIdQuery getCommentByIdQuery = new GetCommentByIdQuery()
                    {
                        Id = context.GetArgument<GetCommentByIdQuery>("input").Id,
                        User_id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };

                    try
                    {
                        return await _mediator.Send(getCommentByIdQuery);

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Logging in");
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }
                });
        }
    }
}
