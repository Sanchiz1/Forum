using Application.UseCases.Posts.Commands;
using FluentValidation;
using Forum.GraphQL.Types.PostTypes;
using Forum.Helpers;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Forum.GraphQL.Mutations
{
    public class PostMutation : ObjectGraphType
    {
        private readonly IMediator _mediator;

        public PostMutation(IMediator mediator)
        {
            _mediator = mediator;

            Field<StringGraphType>("createPost")
                .Argument<NonNullGraphType<CreatePostInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    CreatePostCommand createPostCommand = context.GetArgument<CreatePostCommand>("input");
                    try
                    {
                        await _mediator.Send(createPostCommand);
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
                    return "Post created successfully";
                });

            Field<StringGraphType>("updatePost")
                .Argument<NonNullGraphType<UpdatePostInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdatePostCommand updatePostCommand = context.GetArgument<UpdatePostCommand>("input");
                    try
                    {
                        await _mediator.Send(updatePostCommand);
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
                    return "Post updated successfully";
                });

            Field<StringGraphType>("addPostCategory")
                .Argument<NonNullGraphType<AddPostCategoryInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    AddPostCategoryCommand addPostCategoryCommand = context.GetArgument<AddPostCategoryCommand>("input");
                    try
                    {
                        await _mediator.Send(addPostCategoryCommand);
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
                    return "Post category added successfully";
                });

            Field<StringGraphType>("removePostCategory")
                .Argument<NonNullGraphType<RemovePostCategoryInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    RemovePostCategoryCommand removePostCategoryCommand = context.GetArgument<RemovePostCategoryCommand>("input");
                    try
                    {
                        await _mediator.Send(removePostCategoryCommand);
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
                    return "Post category removed successfully";
                });
            Field<StringGraphType>("deletePost")
                .Argument<NonNullGraphType<DeletePostInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeletePostCommand deletePostCommand = context.GetArgument<DeletePostCommand>("input");
                    try
                    {
                        await _mediator.Send(deletePostCommand);
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

                    return "Post deleted successfully";
                });
            Field<StringGraphType>("likePost")
                .Argument<NonNullGraphType<LikePostInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    LikePostCommand likePostCommand = new LikePostCommand()
                    {
                        Post_Id = context.GetArgument<LikePostCommand>("input").Post_Id,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };
                    try
                    {
                        await _mediator.Send(likePostCommand);
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

                    return "Post liked successfully";
                });
        }
    }
}
