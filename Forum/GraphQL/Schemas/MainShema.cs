using Forum.GraphQL.Types;
using GraphQL;
using GraphQL.Types;

namespace TimeTracker.GraphQL.Schemas
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
