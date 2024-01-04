using Application.Common.Exceptions;
using Application.UseCases.Comments.Commands;
using Application.UseCases.Posts.Commands;
using Application.UseCases.Replies.Commands;
using FluentValidation;
using Forum.GraphQL.Types.ReplyTypes;
using Forum.Helpers;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Forum.GraphQL.Mutations
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
                    CreateReplyCommand command = new CreateReplyCommand()
                    {
                        Text = context.GetArgument<CreateReplyCommand>("input").Text,
                        Comment_Id = context.GetArgument<CreateReplyCommand>("input").Comment_Id,
                        Reply_User_Id = context.GetArgument<CreateReplyCommand>("input").Reply_User_Id,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("updateReply")
                .Argument<NonNullGraphType<UpdateReplyInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateReplyCommand command = new UpdateReplyCommand()
                    {
                        Id = context.GetArgument<UpdateReplyCommand>("input").Id,
                        Text = context.GetArgument<UpdateReplyCommand>("input").Text,
                        Account_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("deleteReply")
                .Argument<NonNullGraphType<DeleteReplyInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeleteReplyCommand command = new DeleteReplyCommand()
                    {
                        Id = context.GetArgument<DeleteReplyCommand>("input").Id,
                        Account_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("likeReply")
                .Argument<NonNullGraphType<LikeReplyInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    LikeReplyCommand command = new LikeReplyCommand()
                    {
                        Reply_Id = context.GetArgument<LikeReplyCommand>("input").Reply_Id,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
        }
    }
}
