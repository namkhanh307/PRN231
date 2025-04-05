using GraphQL;
using GraphQL.Types;

public class AppQuery : ObjectGraphType
{
    public AppQuery(PsychologyTheoryQuery theoQuery, TopicQuery topicQuery)
    {
        AddField(theoQuery.GetField("psychologyTheories"));
        AddField(theoQuery.GetField("psychologyTheory"));
        AddField(theoQuery.GetField("searchPsychologyTheories"));
        AddField(topicQuery.GetField("topics"));
        AddField(topicQuery.GetField("topic"));

    }
}
