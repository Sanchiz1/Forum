using Forum.Models;
using GraphQL.Types;

namespace Forum.GraphQL.Types.UserTypes
{
    public class UserGraphType : ObjectGraphType<User>
    {
        public UserGraphType()
        {
            Field(i => i.Id, type: typeof(IntGraphType));
            Field(i => i.Username, type: typeof(StringGraphType));
            Field(i => i.Email, type: typeof(StringGraphType));
            Field(i => i.Bio, type: typeof(StringGraphType));
            Field(i => i.Registered_At, type: typeof(DateTimeGraphType));
            Field(i => i.Password, type: typeof(StringGraphType));
        }
    }
}
