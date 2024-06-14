using Api.GraphQL.Queries;
using GraphQL.Types;

namespace Api.GraphQL.Schemas
{
    public class IdentitySchema : Schema
    {
        public IdentitySchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<IdentityQuery>();
        }
    }
}
