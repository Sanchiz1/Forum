using Forum.Models;
using GraphQL.Types;

namespace Forum.GraphQL.Types.IdentityTypes
{
    public class CredentialsInputGraphType : InputObjectGraphType<Credentials>
    {
        public CredentialsInputGraphType()
        {
            Field(l => l.LoginOrEmail, nullable: false);
            Field(l => l.Password, nullable: false);
        }
    }
}
