using Api.GraphQL.Mutations;
using Api.GraphQL.Queries;
using GraphQL.Types;

namespace Api.GraphQL.Schemas
{
    public class MainShema : Schema
    {
        public MainShema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<MainQuery>();
            Mutation = provider.GetRequiredService<MainMutation>();
        }
    }
}
