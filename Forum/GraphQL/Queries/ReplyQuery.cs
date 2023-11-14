using Forum.GraphQL.Types.UserTypes;
using GraphQL.Types;
using GraphQL;
using Forum.GraphQL.Types.ReplyTypes;
using MediatR;
using Application.UseCases.Posts.Queries;
using Forum.Helpers;
using Application.UseCases.Replies.Queries;

namespace Forum.GraphQL.Queries
{
    public class ReplyQuery : ObjectGraphType
    {
        private readonly IMediator _mediator;
        public ReplyQuery(IMediator mediator)
        {
            _mediator = mediator;

            Field<ListGraphType<ReplyGraphType>>("replies")
                .Argument<NonNullGraphType<GetRepliesInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetRepliesQuery getRepliesQuery = new GetRepliesQuery()
                    {
                        Next = context.GetArgument<GetPostsQuery>("input").Next,
                        Offset = context.GetArgument<GetPostsQuery>("input").Offset,
                        Order = context.GetArgument<GetPostsQuery>("input").Order,
                        User_Timestamp = context.GetArgument<GetPostsQuery>("input").User_Timestamp,
                        User_Id = AccountHelper.GetUserIdFromClaims(context.User!)
                    };
                    return await _mediator.Send(getRepliesQuery);
                });
            Field<PostGraphType>("reply")
                .Argument<NonNullGraphType<GetReplyByIdInputGraphType>>("input")
                .ResolveAsync(async context =>
                {
                    GetReplyByIdQuery getReplyByIdQuery = new GetReplyByIdQuery()
                    {
                        Id = context.GetArgument<GetPostByIdQuery>("input").Id
                    };
                    return await _mediator.Send(getReplyByIdQuery);
                });
        }
    }
}
