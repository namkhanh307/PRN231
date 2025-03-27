using GraphQL.Types;
using SPHealthSupportSystem_Repositories.Models;

namespace SPHSS_GraghQLAPIService.GraphQLs
{
    public class TopicType : ObjectGraphType<Topic>
    {
        public TopicType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Description, nullable: true);

            Field<StringGraphType>(
                "createdAt",
                resolve: context => context.Source.CreateAt.Value.ToString("yyyy-MM-dd HH:mm:ss")
                );

            Field<StringGraphType>(
                "updateAt",
                resolve: context => context.Source.UpdateAt.HasValue
                    ? context.Source.UpdateAt.Value.ToString("yyyy-MM-dd HH:mm:ss")
                    : null
            );

            // Nếu muốn lấy danh sách sản phẩm trong danh mục
            Field<ListGraphType<PsychologyTheoryType>>(
                "psychologyTheories",
                resolve: context => context.Source.PsychologyTheories
            );



        }
    }
}
