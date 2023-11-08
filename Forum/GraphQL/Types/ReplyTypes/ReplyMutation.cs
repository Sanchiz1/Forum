using Forum.Data.Repositories.Interfaces;
using Forum.GraphQL.Types.CommentTypes;
using Forum.Helpers;
using Forum.Models.Comments;
using Forum.Models.Replies;
using GraphQL;
using GraphQL.Types;

namespace Forum.GraphQL.Types.ReplyTypes
{
    public class ReplyMutation : ObjectGraphType
    {
        private readonly IReplyRepository repo;

        public ReplyMutation(IReplyRepository Repo)
        {
            repo = Repo;

            Field<StringGraphType>("createReply")
                .Argument<NonNullGraphType<ReplyInputGraphType>>("Reply")
                .ResolveAsync(async context =>
                {
                    ReplyInput reply = context.GetArgument<ReplyInput>("Reply");

                    if (reply.Text.Length == 0)
                    {
                        context.Errors.Add(new ExecutionError("Invalid input"));
                        return null;
                    }

                    repo.CreateReply(reply);
                    return "Comment created successfully";
                });

            Field<StringGraphType>("updateReply")
                .Argument<StringGraphType>("Text")
                .Argument<NonNullGraphType<IntGraphType>>("Id")
                .ResolveAsync(async context =>
                {
                    string text = context.GetArgument<string>("Text");
                    int id = context.GetArgument<int>("Id");

                    Reply post = repo.GetReplyById(id);
                    if (post.User_Id != AccountHelper.GetUserIdFromClaims(context.User))
                    {
                        context.Errors.Add(new ExecutionError("Cannot edit someone else's comment"));
                        return null;
                    }

                    repo.UpdateReply(text, id);
                    return "Comment updated successfully";
                });

            Field<StringGraphType>("deleteReply")
                .Argument<NonNullGraphType<IntGraphType>>("Id")
                .ResolveAsync(async context =>
                {
                    int id = context.GetArgument<int>("Id");

                    Reply post = repo.GetReplyById(id);
                    if (post.User_Id != AccountHelper.GetUserIdFromClaims(context.User))
                    {
                        context.Errors.Add(new ExecutionError("Cannot delete someone else's comment"));
                        return null;
                    }

                    repo.DeleteReply(id);
                    return "Comment deleted successfully";
                });
        }
    }
}
