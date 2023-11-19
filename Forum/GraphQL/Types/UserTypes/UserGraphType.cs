using Application.Common.ViewModels;
using Domain.Entities;
using GraphQL.Types;

namespace Forum.GraphQL.Types.UserTypes
{
    public class UserGraphType : ObjectGraphType<UserViewModel>
    {
        public UserGraphType()
        {
            Field(i => i.User.Id, type: typeof(IntGraphType));
            Field(i => i.User.Username, type: typeof(StringGraphType));
            Field(i => i.User.Email, type: typeof(StringGraphType));
            Field(i => i.User.Bio, type: typeof(StringGraphType), nullable: true);
            Field(i => i.User.Registered_At, type: typeof(DateTimeGraphType));
        }
    }
}
