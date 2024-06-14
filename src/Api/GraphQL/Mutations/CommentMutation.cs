using Api.GraphQL.Types.CommentTypes;
using Api.Helpers;
using Application.UseCases.Comments.Commands;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Api.GraphQL.Mutations
{
    public class CommentMutation : ObjectGraphType
    {
        private readonly IMediator _mediator;

        public CommentMutation(IMediator mediator)
        {
            _mediator = mediator;

            Field<StringGraphType>("createComment")
                .Argument<NonNullGraphType<CreateCommentInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    CreateCommentCommand command = context.GetArgument<CreateCommentCommand>("input");

                    command.User_Id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("updateComment")
                .Argument<NonNullGraphType<UpdateCommentInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateCommentCommand command = context.GetArgument<UpdateCommentCommand>("input");

                    command.Account_Id = AccountHelper.GetUserIdFromClaims(context.User!);
                    command.Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("deleteComment")
                .Argument<NonNullGraphType<DeleteCommentInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeleteCommentCommand command = context.GetArgument<DeleteCommentCommand>("input");

                    command.Account_Id = AccountHelper.GetUserIdFromClaims(context.User!);
                    command.Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
            Field<StringGraphType>("likeComment")
                .Argument<NonNullGraphType<LikeCommentInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    LikeCommentCommand command = context.GetArgument<LikeCommentCommand>("input");

                    command.User_Id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
        }
    }
}
