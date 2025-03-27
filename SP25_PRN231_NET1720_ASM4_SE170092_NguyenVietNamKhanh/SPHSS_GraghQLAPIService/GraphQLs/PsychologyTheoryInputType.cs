using GraphQL.Types;
using SPHealthSupportSystem_Repositories.Models;

namespace SPHSS_GraghQLAPIService.GraphQLs
{
    public class PsychologyTheoryInputType : InputObjectGraphType<PsychologyTheory>
    {
        public PsychologyTheoryInputType()
        {
            Name = "PsychologyTheory"; // Tên của input type
            Description = "Input type for creating or updating an psychologyTheory.";

            Field(x => x.Id, nullable: false).Description("Id (Required for create operations).");
            Field(x => x.Name, nullable: false).Description("Name of the theory");
            Field(x => x.Description, nullable: false).Description("Description of the theory");

            Field<StringGraphType>("CreateAt", description: "The created time of the theory in 'yyyy-MM-dd' format.");
            Field<StringGraphType>("UpdateAt", description: "The updated time of the theory in 'yyyy-MM-dd' format.");

            Field(x => x.TopicId, nullable: true).Description("TopicId specify which topic does this thoery belongs to");
            Field(x => x.YearPublished, nullable: true).Description("Year Published");
            Field(x => x.TheoryType, nullable: true).Description("Theory Type");
            Field(x => x.Principle, nullable: true).Description("Principle");
            Field(x => x.Application, nullable: true).Description("Application");
            Field(x => x.RelatedTheory, nullable: true).Description("RelatedTheory");
            Field(x => x.Criticism, nullable: true).Description("Criticism");

        }
    }
}
