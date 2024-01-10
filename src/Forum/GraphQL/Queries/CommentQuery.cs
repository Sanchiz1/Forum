using Application.UseCases.Comments.Queries;
using Forum.GraphQL.Types.CommentTypes;
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
                    GetCommentsQuery query = new GetCommentsQuery()
                    {
                        Post_Id = context.GetArgument<GetCommentsQuery>("input").Post_Id,
                        Next = context.GetArgument<GetCommentsQuery>("input").Next,
                        Offset = context.GetArgument<GetCommentsQuery>("input").Offset,
                        Order = context.GetArgument<GetCommentsQuery>("input").Order,
                        User_Timestamp = context.GetArgument<GetCommentsQuery>("input").User_Timestamp,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
            Field<CommentGraphType>("comment")
                .Argument<NonNullGraphType<GetCommentByIdInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetCommentByIdQuery query = new GetCommentByIdQuery()
                    {
                        Id = context.GetArgument<GetCommentByIdQuery>("input").Id,
                        User_id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
        }
    }
}
