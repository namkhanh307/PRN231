using GraphQL.Types;
using SPHealthSupportSystem_Repositories.Models;

public class TopicType : ObjectGraphType<Topic>
{
    public TopicType()
    {
        Field(x => x.Id, type: typeof(IntGraphType)).Description("ID của chủ đề");
        Field(x => x.Name).Description("Tên chủ đề");
        Field(x => x.Description).Description("Mô tả chủ đề");
        Field(x => x.CreateAt, nullable: true, type: typeof(DateTimeGraphType)).Description("Ngày tạo");
        Field(x => x.UpdateAt, nullable: true, type: typeof(DateTimeGraphType)).Description("Ngày cập nhật");
        Field<ListGraphType<PsychologyTheoryType>>(
            "psychologyTheories",
            resolve: context => context.Source.PsychologyTheories
        );
    }
}
