using Forum.Data.Repositories.Interfaces;
using Forum.GraphQL.Types.UserTypes;
using Forum.Helpers;
using Forum.Models.Posts;
using Forum.Models.User;
using GraphQL;
using GraphQL.Types;

namespace Forum.GraphQL.Types.PostTypes
{
    public class PostMutation : ObjectGraphType
    {
        private readonly IPostRepository repo;

        public PostMutation(IPostRepository Repo)
        {
            repo = Repo;

            Field<StringGraphType>("createPost")
                .Argument<NonNullGraphType<PostInputGraphType>>("Post")
                .ResolveAsync(async context =>
                {
                    PostInput post = context.GetArgument<PostInput>("Post");

                    if (post.Title.Length == 0)
                    {
                        context.Errors.Add(new ExecutionError("Invalid input"));
                        return null;
                    }

                    repo.CreatePost(post);
                    return "Post created successfully";
                }).AuthorizeWithPolicy("Authorized");

            Field<StringGraphType>("updatePost")
                .Argument<StringGraphType>("Text")
                .Argument<NonNullGraphType<IntGraphType>>("Id")
                .ResolveAsync(async context =>
                {
                    string text = context.GetArgument<string>("Text");
                    int id = context.GetArgument<int>("Id");

                    Post post = repo.GetPostById(id);
                    if (post.User_Id != AccountHelper.GetUserIdFromClaims(context.User))
                    {
                        context.Errors.Add(new ExecutionError("Cannot edit someone else's post"));
                        return null;
                    }

                    repo.UpdatePost(text, id);
                    return "Post updated successfully";
                }).AuthorizeWithPolicy("Authorized");

            Field<StringGraphType>("deletePost")
                .Argument<NonNullGraphType<IntGraphType>>("Id")
                .ResolveAsync(async context =>
                {
                    int id = context.GetArgument<int>("Id");

                    Post post = repo.GetPostById(id);
                    if (post.User_Id != AccountHelper.GetUserIdFromClaims(context.User))
                    {
                        context.Errors.Add(new ExecutionError("Cannot edit someone else's post"));
                        return null;
                    }

                    repo.DeletePost(id);
                    return "Post deleted successfully";
                }).AuthorizeWithPolicy("Authorized");
        }
    }
}
