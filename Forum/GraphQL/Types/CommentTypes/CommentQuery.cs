using Forum.Data.Repositories.Interfaces;
using GraphQL;
using GraphQL.Types;

namespace Forum.GraphQL.Types.CommentTypes
{
    public class CommentQuery : ObjectGraphType
    {
        private readonly ICommentRepository repo;
        public CommentQuery(ICommentRepository Repo)
        {
            repo = Repo;

            Field<ListGraphType<CommentGraphType>>("comments")
                .Argument<NonNullGraphType<IntGraphType>>("post_id")
                .Argument<NonNullGraphType<IntGraphType>>("offset")
                .Argument<NonNullGraphType<IntGraphType>>("next")
                .Argument<NonNullGraphType<StringGraphType>>("order")
                .Argument<NonNullGraphType<DateTimeGraphType>>("user_timestamp")
                .Resolve(context =>
                {
                    int post_id = context.GetArgument<int>("post_id");
                    int offset = context.GetArgument<int>("offset");
                    int next = context.GetArgument<int>("next");
                    string order = context.GetArgument<string>("order");
                    DateTime user_timestamp = context.GetArgument<DateTime>("user_timestamp");
                    return repo.GetComments(post_id, next, offset, user_timestamp, order);
                });
        }
    }
}
