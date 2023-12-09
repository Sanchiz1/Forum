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
                    CreateReplyCommand createReplyCommand = context.GetArgument<CreateReplyCommand>("input");
                    try
                    {
                        await _mediator.Send(createReplyCommand);
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
                    return "Reply created successfully";
                });

            Field<StringGraphType>("updateReply")
                .Argument<NonNullGraphType<UpdateReplyInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateReplyCommand updateReplyCommand = context.GetArgument<UpdateReplyCommand>("input");
                    try
                    {
                        await _mediator.Send(updateReplyCommand);
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
                    return "Reply created successfully";
                });

            Field<StringGraphType>("deleteReply")
                .Argument<NonNullGraphType<DeleteReplyInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeleteReplyCommand deleteReplyCommand = context.GetArgument<DeleteReplyCommand>("input");
                    try
                    {
                        await _mediator.Send(deleteReplyCommand);
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
                    return "Reply deleted successfully";
                });

            Field<StringGraphType>("likeReply")
                .Argument<NonNullGraphType<LikeReplyInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    LikeReplyCommand likeReplyCommand = new LikeReplyCommand()
                    {
                        Reply_Id = context.GetArgument<LikeReplyCommand>("input").Reply_Id,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    }; try
                    {
                        await _mediator.Send(likeReplyCommand);
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
                    };

                    return "Reply liked successfully";
                });
        }
    }
}
