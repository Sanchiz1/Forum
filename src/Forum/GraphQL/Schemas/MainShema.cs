using Forum.GraphQL.Mutations;
using Forum.GraphQL.Queries;
using GraphQL.Types;

namespace Forum.GraphQL.Schemas
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
