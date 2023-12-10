using Application.Common.Exceptions;
using Application.UseCases.Categories.Commands;
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
                    CreatePostCommand createPostCommand = new CreatePostCommand()
                    {
                        Title = context.GetArgument<CreatePostCommand>("input").Title,
                        Text = context.GetArgument<CreatePostCommand>("input").Text,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };
                    try
                    {
                        await _mediator.Send(createPostCommand);
                    }
                    catch (PermissionException ex)
                    {
                        context.Errors.Add(new ExecutionError("You don`t have permissions for that action"));
                        return null;
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
                    UpdatePostCommand updatePostCommand = new UpdatePostCommand()
                    {
                        Id = context.GetArgument<UpdatePostCommand>("input").Id,
                        Text = context.GetArgument<UpdatePostCommand>("input").Text,
                        Account_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };
                    try
                    {
                        await _mediator.Send(updatePostCommand);
                    }
                    catch (PermissionException ex)
                    {
                        context.Errors.Add(new ExecutionError("You don`t have permissions for that action"));
                        return null;
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
                    AddPostCategoryCommand addPostCategoryCommand = new AddPostCategoryCommand()
                    {
                        Post_Id = context.GetArgument<AddPostCategoryCommand>("input").Post_Id,
                        Category_Id = context.GetArgument<AddPostCategoryCommand>("input").Category_Id,
                        Account_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };
                    try
                    {
                        await _mediator.Send(addPostCategoryCommand);
                    }
                    catch (PermissionException ex)
                    {
                        context.Errors.Add(new ExecutionError("You don`t have permissions for that action"));
                        return null;
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
                    RemovePostCategoryCommand removePostCategoryCommand = new RemovePostCategoryCommand()
                    {
                        Post_Id = context.GetArgument<RemovePostCategoryCommand>("input").Post_Id,
                        Category_Id = context.GetArgument<RemovePostCategoryCommand>("input").Category_Id,
                        Account_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };
                    try
                    {
                        await _mediator.Send(removePostCategoryCommand);
                    }
                    catch (PermissionException ex)
                    {
                        context.Errors.Add(new ExecutionError("You don`t have permissions for that action"));
                        return null;
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
                    DeletePostCommand deletePostCommand = new DeletePostCommand()
                    {
                        Id = context.GetArgument<DeletePostCommand>("input").Id,
                        Account_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };
                    try
                    {
                        await _mediator.Send(deletePostCommand);
                    }
                    catch (PermissionException ex)
                    {
                        context.Errors.Add(new ExecutionError("You don`t have permissions for that action"));
                        return null;
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
