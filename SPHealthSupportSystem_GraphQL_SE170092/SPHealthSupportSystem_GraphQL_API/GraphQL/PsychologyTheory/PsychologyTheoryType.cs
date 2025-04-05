using GraphQL.Types;
using SPHealthSupportSystem_Repositories.Models;

public class PsychologyTheoryType : ObjectGraphType<PsychologyTheory>
{
    public PsychologyTheoryType()
    {
        Name = "PsychologyTheory";

        Field(x => x.Id, type: typeof(IntGraphType)).Description("ID của lý thuyết");
        Field(x => x.Name).Description("Tên lý thuyết");
        Field(x => x.Description).Description("Mô tả lý thuyết");
        Field(x => x.TopicId).Description("ID của chủ đề");
        Field(x => x.Author).Description("Tác giả");
        Field(x => x.YearPublished, nullable: true).Description("Năm xuất bản");
        Field(x => x.TheoryType).Description("Loại lý thuyết");
        Field(x => x.Principle).Description("Nguyên tắc");
        Field(x => x.Application).Description("Ứng dụng");
        Field(x => x.RelatedTheory).Description("Lý thuyết liên quan");
        Field(x => x.Criticism).Description("Phản biện");
        Field(x => x.CreateAt, nullable: true).Description("Ngày tạo");
        Field(x => x.UpdateAt, nullable: true).Description("Ngày cập nhật");
    }
}
