using Api.GraphQL.Types.CommentTypes;
using Api.Helpers;
using Application.UseCases.Comments.Queries;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Api.GraphQL.Queries
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
                    GetCommentsQuery query = context.GetArgument<GetCommentsQuery>("input");

                    query.User_Id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<CommentGraphType>("comment")
                .Argument<NonNullGraphType<GetCommentByIdInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetCommentByIdQuery query = context.GetArgument<GetCommentByIdQuery>("input");

                    query.User_id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
        }
    }
}
