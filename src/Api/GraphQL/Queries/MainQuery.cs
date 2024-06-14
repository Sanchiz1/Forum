using GraphQL.Types;

namespace Api.GraphQL.Queries
{
    public class MainQuery : ObjectGraphType
    {
        public MainQuery()
        {

            Field<UserQuery>("users")
                .Resolve(context => new { });

            Field<PostQuery>("posts")
                .Resolve(context => new { });

            Field<CommentQuery>("comments")
                .Resolve(context => new { });

            Field<ReplyQuery>("replies")
                .Resolve(context => new { });

            Field<CategoryQuery>("categories")
                .Resolve(context => new { });

            Field<StatisticsQuery>("statistics")
                .Resolve(context => new { });
        }
    }
}
