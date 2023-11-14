using Forum.GraphQL.Queries;
using GraphQL.Types;

namespace TimeTracker.GraphQL.Schemas
{
    public class IdentitySchema : Schema
        {
            public IdentitySchema(IServiceProvider provider) : base(provider)
            {
                Query = provider.GetRequiredService<IdentityQuery>();
            }
        }
}
