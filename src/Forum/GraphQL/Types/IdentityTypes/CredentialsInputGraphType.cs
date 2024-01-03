using Application.UseCases.Comments.Queries;
using Application.UseCases.Identity.Queries;
using GraphQL.Types;

namespace Forum.GraphQL.Types.IdentityTypes
{
    public class LoginInputGraphType : InputObjectGraphType<LoginQuery>
    {
        public LoginInputGraphType()
        {
            Field(l => l.Username_Or_Email, nullable: false);
            Field(l => l.Password, nullable: false);
        }
    }
    public class RefreshTokenInputGraphType : InputObjectGraphType<RefreshTokenQuery>
    {
        public RefreshTokenInputGraphType()
        {
            Field(l => l.Token, nullable: false);
        }
    }
    public class LogoutInputGraphType : InputObjectGraphType<LogoutQuery>
    {
        public LogoutInputGraphType()
        {
            Field(l => l.Token, nullable: false);
        }
    }
}
