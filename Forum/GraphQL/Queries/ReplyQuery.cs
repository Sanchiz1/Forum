using Forum.GraphQL.Types.UserTypes;
using GraphQL.Types;
using GraphQL;
using Forum.GraphQL.Types.ReplyTypes;
using MediatR;
using Application.UseCases.Posts.Queries;
using Forum.Helpers;
using Application.UseCases.Replies.Queries;
using Application.Common.Models;

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
                    GetRepliesQuery query = new GetRepliesQuery()
                    {
                        Comment_Id = context.GetArgument<GetRepliesQuery>("input").Comment_Id,
                        Next = context.GetArgument<GetRepliesQuery>("input").Next,
                        Offset = context.GetArgument<GetRepliesQuery>("input").Offset,
                        Order = context.GetArgument<GetRepliesQuery>("input").Order,
                        User_Timestamp = context.GetArgument<GetRepliesQuery>("input").User_Timestamp,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
            Field<PostGraphType>("reply")
                .Argument<NonNullGraphType<GetReplyByIdInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetReplyByIdQuery query = new GetReplyByIdQuery()
                    {
                        Id = context.GetArgument<GetReplyByIdQuery>("input").Id
                    };

                    var result = await _mediator.Send(query);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
        }
    }
}
