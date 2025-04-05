using GraphQL.Types;

public class AppMutation : ObjectGraphType
{
    public AppMutation(PsychologyTheoryMutation theoMutation)
    {
        Field<PsychologyTheoryMutation>("psychologyTheory", resolve: context => theoMutation);
   
    }
}
