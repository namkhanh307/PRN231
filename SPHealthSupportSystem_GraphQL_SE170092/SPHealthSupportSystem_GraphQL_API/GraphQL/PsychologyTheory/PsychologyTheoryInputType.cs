using GraphQL.Types;

using SPHealthSupportSystem_Repositories.Models;

public class PsychologyTheoryInputType : InputObjectGraphType<PsychologyTheory>
{
    public PsychologyTheoryInputType()
    {
        Name = "psychologyTheoryInput";
        Field<NonNullGraphType<IntGraphType>>("id");
        Field<NonNullGraphType<StringGraphType>>("name");
        Field<NonNullGraphType<StringGraphType>>("description");
        Field<NonNullGraphType<IntGraphType>>("topicId");
        Field<NonNullGraphType<StringGraphType>>("author");
        Field<IntGraphType>("yearPublished");
        Field<NonNullGraphType<StringGraphType>>("theoryType");
        Field<NonNullGraphType<StringGraphType>>("principle");
        Field<NonNullGraphType<StringGraphType>>("application");
        Field<NonNullGraphType<StringGraphType>>("relatedTheory");
        Field<NonNullGraphType<StringGraphType>>("criticism");
    }
}
