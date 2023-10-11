using GraphQL.Types;
using TimeTracker.GraphQL.Types.IdentityTypes;

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
