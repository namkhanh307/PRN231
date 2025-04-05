using GraphQL;
using GraphQL.Types;
using SPHealthSupportSystem_Services;
using System.Reflection.Metadata;

public class TopicQuery : ObjectGraphType
{
    public TopicQuery(ITopicService service)
    {
        // Lấy tất cả danh mục
        Field<ListGraphType<TopicType>>(
            "topics",
            resolve: context => service.GetAll().Result
        );

        Field<TopicType>(
            "topic",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
            resolve: context =>
            {
                var item = service.GetById(context.GetArgument<int>("id"));

                if (item == null)
                {
                    throw new ExecutionError("item not found.");
                }
                return item;
            }
        );

    }
}
