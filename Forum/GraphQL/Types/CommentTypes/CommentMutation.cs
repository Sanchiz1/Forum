using Forum.Data.Repositories.Interfaces;
using Forum.GraphQL.Types.UserTypes;
using Forum.Helpers;
using Forum.Models.Comments;
using GraphQL;
using GraphQL.Types;

namespace Forum.GraphQL.Types.CommentTypes
{
    public class CommentMutation : ObjectGraphType
    {
        private readonly ICommentRepository repo;

        public CommentMutation(ICommentRepository Repo)
        {
            repo = Repo;

            Field<StringGraphType>("createComment")
                .Argument<NonNullGraphType<CommentInputGraphType>>("Comment")
                .ResolveAsync(async context =>
                {
                    CommentInput comment = context.GetArgument<CommentInput>("Comment");

                    if (comment.Text.Length == 0)
                    {
                        context.Errors.Add(new ExecutionError("Invalid input"));
                        return null;
                    }

                    repo.CreateComment(comment);
                    return "Comment created successfully";
                });

            Field<StringGraphType>("updateComment")
                .Argument<StringGraphType>("Text")
                .Argument<NonNullGraphType<IntGraphType>>("Id")
                .ResolveAsync(async context =>
                {
                    string text = context.GetArgument<string>("Text");
                    int id = context.GetArgument<int>("Id");

                    Comment post = repo.GetCommentById(id);
                    if (post.User_Id != AccountHelper.GetUserIdFromClaims(context.User))
                    {
                        context.Errors.Add(new ExecutionError("Cannot edit someone else's comment"));
                        return null;
                    }

                    repo.UpdateComment(text, id);
                    return "Comment updated successfully";
                });

            Field<StringGraphType>("deleteComment")
                .Argument<NonNullGraphType<IntGraphType>>("Id")
                .ResolveAsync(async context =>
                {
                    int id = context.GetArgument<int>("Id");

                    Comment post = repo.GetCommentById(id);
                    if (post.User_Id != AccountHelper.GetUserIdFromClaims(context.User))
                    {
                        context.Errors.Add(new ExecutionError("Cannot delete someone else's comment"));
                        return null;
                    }

                    repo.DeleteComment(id);
                    return "Comment deleted successfully";
                });
        }
    }
}
