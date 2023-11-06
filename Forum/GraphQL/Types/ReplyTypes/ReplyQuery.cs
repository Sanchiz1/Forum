using Forum.Data.Repositories.Interfaces;
using Forum.GraphQL.Types.UserTypes;
using GraphQL.Types;
using GraphQL;

namespace Forum.GraphQL.Types.ReplyTypes
{
    public class ReplyQuery : ObjectGraphType
    {
        private readonly IReplyRepository repo;
        public ReplyQuery(IReplyRepository Repo)
        {
            repo = Repo;

            Field<ListGraphType<ReplyGraphType>>("replies")
                .Argument<NonNullGraphType<IntGraphType>>("post_Id")
                .Resolve(context =>
                {
                    int post_Id = context.GetArgument<int>("post_Id");
                    return repo.GetPostReplies(post_Id);
                });
            Field<ListGraphType<ReplyGraphType>>("replyReplies")
                .Argument<NonNullGraphType<IntGraphType>>("reply_Id")
                .Resolve(context =>
                {
                    int reply_Id = context.GetArgument<int>("reply_Id");
                    return repo.GetReplyReplies(reply_Id);
                });
        }
    }
}
