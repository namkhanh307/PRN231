using GraphQL.Types;
using SPHealthSupportSystem_Repositories.Models;

namespace SPHSS_GraghQLAPIService.GraphQLs
{
    public class PsychologyTheoryType : ObjectGraphType<PsychologyTheory>
    {
        [Obsolete]
        public PsychologyTheoryType()
        {
            Field(x => x.Id, nullable: false);
            Field(x => x.Name, nullable: false);
            Field(x => x.Description, nullable: false);

            Field<StringGraphType>("CreateAt", description: "The created time of the theory in 'yyyy-MM-dd' format.");
            Field<StringGraphType>("UpdateAt", description: "The updated time of the theory in 'yyyy-MM-dd' format.");
            Field(x => x.TopicId, nullable: true);
            Field(x => x.YearPublished, nullable: true);
            Field(x => x.TheoryType, nullable: true);
            Field(x => x.Principle, nullable: true);
            Field(x => x.Application, nullable: true);
            Field(x => x.RelatedTheory, nullable: true);
            Field(x => x.Criticism, nullable: true);
            // Bạn cũng có thể ánh xạ các đối tượng con nếu có
            Field<TopicType>("topic", resolve: context => context.Source.Topic);
        }
    }
}
