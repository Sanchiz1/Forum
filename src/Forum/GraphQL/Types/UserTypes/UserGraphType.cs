using Application.Common.ViewModels;
using GraphQL.Types;

namespace Forum.GraphQL.Types.UserTypes
{
    public class UserGraphType : ObjectGraphType<UserViewModelDto>
    {
        public UserGraphType()
        {
            Field(i => i.User.Id, type: typeof(IntGraphType));
            Field(i => i.User.Username, type: typeof(StringGraphType));
            Field(i => i.User.Email, type: typeof(StringGraphType));
            Field(i => i.User.Bio, type: typeof(StringGraphType), nullable: true);
            Field(i => i.User.Registered_At, type: typeof(DateTimeGraphType));
            Field(i => i.Posts, type: typeof(IntGraphType));
            Field(i => i.Comments, type: typeof(IntGraphType));
            Field(i => i.Role_Id, type: typeof(IntGraphType));
            Field(i => i.Role, type: typeof(StringGraphType));
        }
    }
}
