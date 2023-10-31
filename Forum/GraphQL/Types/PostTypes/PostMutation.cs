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
        }
    }
}
