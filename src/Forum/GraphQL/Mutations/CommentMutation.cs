using Application.Common.Exceptions;
using Application.UseCases.Comments.Commands;
using Application.UseCases.Posts.Commands;
using FluentValidation;
using Forum.GraphQL.Types.CommentTypes;
using Forum.Helpers;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Forum.GraphQL.Mutations
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
                    CreateCommentCommand command = new CreateCommentCommand()
                    {
                        Text = context.GetArgument<CreateCommentCommand>("input").Text,
                        Post_Id = context.GetArgument<CreateCommentCommand>("input").Post_Id,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("updateComment")
                .Argument<NonNullGraphType<UpdateCommentInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateCommentCommand command = new UpdateCommentCommand()
                    {
                        Id = context.GetArgument<UpdateCommentCommand>("input").Id,
                        Text = context.GetArgument<UpdateCommentCommand>("input").Text,
                        Account_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("deleteComment")
                .Argument<NonNullGraphType<DeleteCommentInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeleteCommentCommand command = new DeleteCommentCommand()
                    {
                        Id = context.GetArgument<DeleteCommentCommand>("input").Id,
                        Account_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
            Field<StringGraphType>("likeComment")
                .Argument<NonNullGraphType<LikeCommentInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    LikeCommentCommand command = new LikeCommentCommand()
                    {
                        Comment_Id = context.GetArgument<LikeCommentCommand>("input").Comment_Id,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
        }
    }
}
