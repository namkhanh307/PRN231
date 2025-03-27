using GraphQL.Types;

namespace SPHSS_GraghQLAPIService.GraphQLs
{
    public class PsychologyTheorySchema : Schema
    {
        public PsychologyTheorySchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<PsychologyTheoryQuery>();
            Mutation = serviceProvider.GetRequiredService<PsychologyTheoryMutation>();
        }
    }
}
