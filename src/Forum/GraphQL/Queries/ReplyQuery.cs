using Forum.GraphQL.Types.UserTypes;
using GraphQL.Types;
using GraphQL;
using Forum.GraphQL.Types.ReplyTypes;
using MediatR;
using Forum.Helpers;
using Application.UseCases.Replies.Queries;

namespace Forum.GraphQL.Queries
{
    public class ReplyQuery : ObjectGraphType
    {
        private readonly IMediator _mediator;
        public ReplyQuery(IMediator mediator)
        {
            _mediator = mediator;

            Field<ListGraphType<ReplyGraphType>>("replies")
                .Argument<NonNullGraphType<GetRepliesInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetRepliesQuery query = context.GetArgument<GetRepliesQuery>("input");

                    query.User_Id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<PostGraphType>("reply")
                .Argument<NonNullGraphType<GetReplyByIdInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetReplyByIdQuery query = context.GetArgument<GetReplyByIdQuery>("input");

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
        }
    }
}
