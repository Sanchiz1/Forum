using Application.UseCases.Posts.Commands;
using Application.UseCases.Posts.Queries;
using GraphQL.Types;

namespace Forum.GraphQL.Types.PostTypes
{
    public class GetPostsInputGraphType : InputObjectGraphType<GetPostsQuery>
    {
        public GetPostsInputGraphType()
        {
            Field(p => p.Next, type: typeof(IntGraphType));
            Field(p => p.Offset, type: typeof(IntGraphType));
            Field(p => p.User_Timestamp, type: typeof(DateTimeGraphType));
            Field(p => p.Order, type: typeof(StringGraphType));
        }
    }
    public class GetUserPostsInputGraphType : InputObjectGraphType<GetUserPostsQuery>
    {
        public GetUserPostsInputGraphType()
        {
            Field(p => p.Author_Username, type: typeof(StringGraphType));
            Field(p => p.Next, type: typeof(IntGraphType));
            Field(p => p.Offset, type: typeof(IntGraphType));
            Field(p => p.User_Timestamp, type: typeof(DateTimeGraphType));
            Field(p => p.Order, type: typeof(StringGraphType));
        }
    }
    public class GetPostByIdInputGraphType : InputObjectGraphType<GetPostByIdQuery>
    {
        public GetPostByIdInputGraphType()
        {
            Field(p => p.Id, type: typeof(IntGraphType));
        }
    }
    public class CreatePostInputGraphType : InputObjectGraphType<CreatePostCommand>
    {
        public CreatePostInputGraphType()
        {
            Field(c => c.User_Id, type: typeof(IntGraphType));
            Field(c => c.Title, type: typeof(StringGraphType));
            Field(c => c.Text, type: typeof(StringGraphType));
        }
    }
    public class UpdatePostInputGraphType : InputObjectGraphType<UpdatePostCommand>
    {
        public UpdatePostInputGraphType()
        {
            Field(c => c.Id, type: typeof(IntGraphType));
            Field(c => c.Text, type: typeof(StringGraphType));
        }
    }
    public class AddPostCategoryInputGraphType : InputObjectGraphType<AddPostCategoryCommand>
    {
        public AddPostCategoryInputGraphType()
        {
            Field(c => c.Post_Id, type: typeof(IntGraphType));
            Field(c => c.Category_Id, type: typeof(IntGraphType));
        }
    }
    public class RemovePostCategoryInputGraphType : InputObjectGraphType<RemovePostCategoryCommand>
    {
        public RemovePostCategoryInputGraphType()
        {
            Field(c => c.Post_Id, type: typeof(IntGraphType));
            Field(c => c.Category_Id, type: typeof(IntGraphType));
        }
    }
    public class DeletePostInputGraphType : InputObjectGraphType<DeletePostCommand>
    {
        public DeletePostInputGraphType()
        {
            Field(c => c.Id, type: typeof(IntGraphType));
        }
    }
    public class LikePostInputGraphType : InputObjectGraphType<LikePostCommand>
    {
        public LikePostInputGraphType()
        {
            Field(c => c.Post_Id, type: typeof(IntGraphType));
        }
    }
}
