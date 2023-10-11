using Forum.Models;
using GraphQL.Types;

namespace Forum.GraphQL.Types.IdentityTypes
{
    public class LoginOutputGraphType : ObjectGraphType<LoginOutput>
    {
        public LoginOutputGraphType()
        {
            Field(l => l.access_token, nullable: true, type: typeof(TokenGraphType));
            Field(l => l.user_id, nullable: false);
            Field(l => l.refresh_token, nullable: true, type: typeof(TokenGraphType));
        }
    }
}
