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
                    CreatePostCommand command = new CreatePostCommand()
                    {
                        Title = context.GetArgument<CreatePostCommand>("input").Title,
                        Text = context.GetArgument<CreatePostCommand>("input").Text,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("updatePost")
                .Argument<NonNullGraphType<UpdatePostInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdatePostCommand command = new UpdatePostCommand()
                    {
                        Id = context.GetArgument<UpdatePostCommand>("input").Id,
                        Text = context.GetArgument<UpdatePostCommand>("input").Text,
                        Account_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("addPostCategory")
                .Argument<NonNullGraphType<AddPostCategoryInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    AddPostCategoryCommand command = new AddPostCategoryCommand()
                    {
                        Post_Id = context.GetArgument<AddPostCategoryCommand>("input").Post_Id,
                        Category_Id = context.GetArgument<AddPostCategoryCommand>("input").Category_Id,
                        Account_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("removePostCategory")
                .Argument<NonNullGraphType<RemovePostCategoryInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    RemovePostCategoryCommand command = new RemovePostCategoryCommand()
                    {
                        Post_Id = context.GetArgument<RemovePostCategoryCommand>("input").Post_Id,
                        Category_Id = context.GetArgument<RemovePostCategoryCommand>("input").Category_Id,
                        Account_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
            Field<StringGraphType>("deletePost")
                .Argument<NonNullGraphType<DeletePostInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeletePostCommand command = new DeletePostCommand()
                    {
                        Id = context.GetArgument<DeletePostCommand>("input").Id,
                        Account_Id = AccountHelper.GetUserIdFromClaims(context.User!),
                        Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
            Field<StringGraphType>("likePost")
                .Argument<NonNullGraphType<LikePostInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    LikePostCommand command = new LikePostCommand()
                    {
                        Post_Id = context.GetArgument<LikePostCommand>("input").Post_Id,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
        }
    }
}
