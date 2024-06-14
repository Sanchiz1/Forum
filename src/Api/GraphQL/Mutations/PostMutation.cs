using Api.GraphQL.Types.PostTypes;
using Api.Helpers;
using Application.UseCases.Comments.Commands;
using Application.UseCases.Posts.Commands;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Api.GraphQL.Mutations
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
                    CreatePostCommand command = context.GetArgument<CreatePostCommand>("input");

                    command.Account_Id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("updatePost")
                .Argument<NonNullGraphType<UpdatePostInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdatePostCommand command = context.GetArgument<UpdatePostCommand>("input");

                    command.Account_Id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("addPostCategory")
                .Argument<NonNullGraphType<AddPostCategoryInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    AddPostCategoryCommand command = context.GetArgument<AddPostCategoryCommand>("input");

                    command.Account_Id = AccountHelper.GetUserIdFromClaims(context.User!);
                    command.Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("removePostCategory")
                .Argument<NonNullGraphType<RemovePostCategoryInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    RemovePostCategoryCommand command = context.GetArgument<RemovePostCategoryCommand>("input");

                    command.Account_Id = AccountHelper.GetUserIdFromClaims(context.User!);
                    command.Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("deletePost")
                .Argument<NonNullGraphType<DeletePostInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeletePostCommand command = context.GetArgument<DeletePostCommand>("input");

                    command.Account_Id = AccountHelper.GetUserIdFromClaims(context.User!);
                    command.Account_Role = AccountHelper.GetUserRoleFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });

            Field<StringGraphType>("likePost")
                .Argument<NonNullGraphType<LikePostInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    LikePostCommand command = context.GetArgument<LikePostCommand>("input");

                    command.User_Id = AccountHelper.GetUserIdFromClaims(context.User!);

                    var result = await _mediator.Send(command);

                    return result.Match((res) => res, (ex) => throw new ExecutionError(ex.Message.ToString()));
                });
        }
    }
}
