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
                    CreateCommentCommand createCommentCommand = context.GetArgument<CreateCommentCommand>("input");
                    try
                    {
                        await _mediator.Send(createCommentCommand);
                    }
                    catch (ValidationException ex)
                    {
                        context.Errors.Add(new ExecutionError("Invalid input"));
                        return null;
                    }
                    catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }
                    return "Comment created successfully";
                });

            Field<StringGraphType>("updateComment")
                .Argument<NonNullGraphType<UpdateCommentInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateCommentCommand updateCommentCommand = context.GetArgument<UpdateCommentCommand>("input");
                    try
                    {
                        await _mediator.Send(updateCommentCommand);
                    }
                    catch (ValidationException ex)
                    {
                        context.Errors.Add(new ExecutionError("Invalid input"));
                        return null;
                    }
                    catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }
                    return "Comment updated successfully";
                });

            Field<StringGraphType>("deleteComment")
                .Argument<NonNullGraphType<DeleteCommentInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeleteCommentCommand deleteCommentCommand = context.GetArgument<DeleteCommentCommand>("input");
                    try
                    {
                        await _mediator.Send(deleteCommentCommand);
                    }
                    catch (ValidationException ex)
                    {
                        context.Errors.Add(new ExecutionError("Invalid input"));
                        return null;
                    }
                    catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }
                    return "Comment deleted successfully";
                });
            Field<StringGraphType>("likeComment")
                .Argument<NonNullGraphType<LikeCommentInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    LikeCommentCommand likeCommentCommand = new LikeCommentCommand()
                    {
                        Comment_Id = context.GetArgument<LikeCommentCommand>("input").Comment_Id,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    }; try
                    {
                        await _mediator.Send(likeCommentCommand);
                    }
                    catch (ValidationException ex)
                    {
                        context.Errors.Add(new ExecutionError("Invalid input"));
                        return null;
                    }
                    catch (Exception ex)
                    {
                        context.Errors.Add(new ExecutionError("Sorry, internal error occurred"));
                        return null;
                    }

                    return "Comment liked successfully";
                });
        }
    }
}
