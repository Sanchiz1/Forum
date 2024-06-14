using Application.Common.Models;
using GraphQL.Types;

namespace Api.GraphQL.Types.IdentityTypes
{
    public class TokenGraphType : ObjectGraphType<Token>
    {
        public TokenGraphType()
        {
            Field(t => t.Value, nullable: false);
            Field(t => t.Issued_at, nullable: false);
            Field(t => t.Expires_at, nullable: false);
        }
    }
}
