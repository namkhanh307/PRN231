using GraphQL;
using GraphQL.Types;
using SPHealthSupportSystem_Services;

public class PsychologyTheoryQuery : ObjectGraphType
{
    public PsychologyTheoryQuery(IPsychologyTheoryService service)
    {

        Field<ListGraphType<PsychologyTheoryType>>(
           "psychologyTheories",
           resolve: context =>
           {
               var psychologyTheories = service.GetAll().Result;
               if (psychologyTheories == null)
               {
                   throw new ExecutionError("No psychologyTheory found.");
               }
               return psychologyTheories;
           }
       );


        Field<PsychologyTheoryType>(
            "psychologyTheory",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
            resolve: context =>
            {
                var blog = service.GetById(context.GetArgument<int>("id"));
                if (blog == null)
                {
                    throw new ExecutionError("psychologyTheory not found.");
                }
                return blog;
            }
        );

        Field<ListGraphType<PsychologyTheoryType>>(
            "searchPsychologyTheories",
            arguments: new QueryArguments(
                new QueryArgument<StringGraphType> { Name = "name" },
                new QueryArgument<StringGraphType> { Name = "topic" },
                new QueryArgument<StringGraphType> { Name = "author" }
            ),
            resolve: context =>
            {
                var name = context.GetArgument<string>("name", null);
                var topic = context.GetArgument<string>("topic", null);
                var author = context.GetArgument<string>("author", null);

                var blog = service.Search(name, topic, author).Result;
                if (blog == null)
                {
                    throw new ExecutionError("psychologyTheory not found.");
                }
                return blog;
            }
        );

    }
}
