using Application.Common.Models;
using GraphQL.Types;

namespace Api.GraphQL.Types.IdentityTypes
{
    public class LoginResponseGraphType : ObjectGraphType<LoginResponse>
    {
        public LoginResponseGraphType()
        {
            Field(l => l.Access_Token, nullable: true, type: typeof(TokenGraphType));
            Field(l => l.User_Id, nullable: false);
            Field(l => l.Refresh_Token, nullable: true, type: typeof(TokenGraphType));
        }
    }
}
