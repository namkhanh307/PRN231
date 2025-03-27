using GraphQL;
using GraphQL.Types;
using SPHealthSupportSystem_Repositories.Models;
using SPHealthSupportSystem_Services;

namespace SPHSS_GraghQLAPIService.GraphQLs
{
    public class PsychologyTheoryQuery : ObjectGraphType
    {
        public PsychologyTheoryQuery(IPsychologyTheoryService psychologyTheoryService, ITopicService topicService)
        {
            Field<ListGraphType<PsychologyTheoryType>>(
                "psychologyTheories",
                description: "Retrieve all theories.",
                resolve: context =>
                {
                    var result = psychologyTheoryService.GetAll().Result;
                    Console.WriteLine($"[GraphQL Resolver] Resolving {result.Count} theories");
                    return result;
                }
            );


            // Lấy  theo ID
            Field<PsychologyTheoryType>(
                "psychologyTheory",
                description: "Retrieve by ID.",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return psychologyTheoryService.GetById(id);
                }
            );

            // Tìm kiếm 
            Field<ListGraphType<PsychologyTheoryType>>(
                "searchPsychologyTheories",
                description: "Search by name, description, author",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "name", Description = "Filter by name" },
                    new QueryArgument<StringGraphType> { Name = "description", Description = "Filter by description" },
                    new QueryArgument<StringGraphType> { Name = "author", Description = "Filter by author." }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name", null);
                    var topicName = context.GetArgument<string>("topicName", null);
                    var author = context.GetArgument<string>("author", null);
                    return psychologyTheoryService.Search(name, topicName, author).Result;
                }
            );


            Field<ListGraphType<TopicType>>(
                "topics",
                description: "Retrieve all topics.",
                resolve: context =>
                {
                    try
                    {
                        var topics = topicService.GetAll().Result;
                        return topics;
                    }
                    catch (Exception ex)
                    {
                        throw new ExecutionError($"Error retrieving topics: {ex.Message}");
                    }
                }
            );


        }

    }
}