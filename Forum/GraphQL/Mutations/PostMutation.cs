using Application.UseCases.Posts.Commands;
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

                    await _mediator.Send(createPostCommand);
                    return "Post created successfully";
                });

            Field<StringGraphType>("updatePost")
                .Argument<NonNullGraphType<UpdatePostInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdatePostCommand updatePostCommand = context.GetArgument<UpdatePostCommand>("input");
                    await _mediator.Send(updatePostCommand);
                    return "Post updated successfully";
                });

            Field<StringGraphType>("deletePost")
                .Argument<NonNullGraphType<DeletePostInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeletePostCommand deletePostCommand = context.GetArgument<DeletePostCommand>("input");
                    await _mediator.Send(deletePostCommand);

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
                    await _mediator.Send(likePostCommand);

                    return "Post liked successfully";
                });
        }
    }
}
