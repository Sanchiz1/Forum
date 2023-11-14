using Application.UseCases.Replies.Commands;
using Forum.GraphQL.Types.ReplyTypes;
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

                    await _mediator.Send(createReplyCommand);
                    return "Reply created successfully";
                });

            Field<StringGraphType>("updateReply")
                .Argument<NonNullGraphType<UpdateReplyInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    UpdateReplyCommand updateReplyCommand = context.GetArgument<UpdateReplyCommand>("input");

                    await _mediator.Send(updateReplyCommand);
                    return "Reply created successfully";
                });

            Field<StringGraphType>("deleteReply")
                .Argument<NonNullGraphType<DeleteReplyInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    DeleteReplyCommand deleteReplyCommand = context.GetArgument<DeleteReplyCommand>("input");

                    await _mediator.Send(deleteReplyCommand);
                    return "Reply deleted successfully";
                });

            Field<StringGraphType>("likeReply")
                .Argument<NonNullGraphType<LikeReplyInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    LikeReplyCommand likeReplyCommand = context.GetArgument<LikeReplyCommand>("input");
                    await _mediator.Send(likeReplyCommand);

                    return "Reply liked successfully";
                });
        }
    }
}
