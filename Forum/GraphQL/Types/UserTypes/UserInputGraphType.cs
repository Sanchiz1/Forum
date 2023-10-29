using Forum.Models.User;
using GraphQL.Types;

namespace Forum.GraphQL.Types.UserTypes
{
    public class UserInputGraphType : InputObjectGraphType<UserInput>
    {
        public UserInputGraphType()
        {
            Field(i => i.Username, type: typeof(StringGraphType));
            Field(i => i.Email, type: typeof(StringGraphType));
            Field(i => i.Bio, type: typeof(StringGraphType), nullable: true);
            Field(i => i.Password, type: typeof(StringGraphType), nullable: true);
        }
    }
}
