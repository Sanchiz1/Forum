using Api.GraphQL.Types.ReplyTypes;
using Api.Helpers;
using Application.UseCases.Posts.Commands;
using Application.UseCases.Replies.Commands;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Api.GraphQL.Mutations
{
    public class ReplyMutation : ObjectGraphType
    {
        private readonly IMediator _mediator;

        public ReplyMutation(IMediator mediator)
        {
            _mediator = mediator;

            Field<StringGraphType>("createReply")
                .Argument<NonNullGraphType<CreateReplyInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    CreateReplyCommand command = context.GetArgument<CreateReplyCommand>("input");

                    command.User_Id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("updateReply")
                .Argument<NonNullGraphType<UpdateReplyInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateReplyCommand command = context.GetArgument<UpdateReplyCommand>("input");

                    command.Account_Id = AccountHelper.GetUserIdFromClaims(context.User!);
                    command.Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("deleteReply")
                .Argument<NonNullGraphType<DeleteReplyInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeleteReplyCommand command = context.GetArgument<DeleteReplyCommand>("input");

                    command.Account_Id = AccountHelper.GetUserIdFromClaims(context.User!);
                    command.Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("likeReply")
                .Argument<NonNullGraphType<LikeReplyInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    LikeReplyCommand command = context.GetArgument<LikeReplyCommand>("input");

                    command.User_Id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
        }
    }
}
