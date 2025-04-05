using GraphQL;
using GraphQL.Types;
using System.Threading.Tasks;
using SPHealthSupportSystem_Services;
using SPHealthSupportSystem_Repositories.Models;


public class PsychologyTheoryMutation : ObjectGraphType
{
    public PsychologyTheoryMutation(IPsychologyTheoryService service)
    {

        Field<IntGraphType>(
                "createPsychologyTheory",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PsychologyTheoryInputType>> { Name = "psychologyTheoryInput" }
                ),
                resolve: context =>
                {
                    var theoryInput = context.GetArgument<PsychologyTheory>("psychologyTheoryInput");

                    var theory = new PsychologyTheory
                    {
                        Id = theoryInput.Id,
                        Name = theoryInput.Name,
                        Description = theoryInput.Description,
                        TopicId = theoryInput.TopicId,
                        Author = theoryInput.Author,
                        YearPublished = theoryInput.YearPublished,
                        TheoryType = theoryInput.TheoryType,
                        Principle = theoryInput.Principle,
                        Application = theoryInput.Application,
                        RelatedTheory = theoryInput.RelatedTheory,
                        Criticism = theoryInput.Criticism,
                        CreateAt = theoryInput.CreateAt ?? DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow
                    };

                    return service.Create(theory).GetAwaiter().GetResult();
                }
            );


        Field<IntGraphType>(
            "updatePsychologyTheory",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<PsychologyTheoryInputType>> { Name = "psychologyTheoryInput" }
            ),
            resolve: context =>
            {
                var theoryInput = context.GetArgument<PsychologyTheory>("psychologyTheoryInput");

                var existingTheory = service.GetById(theoryInput.Id);

                if (existingTheory == null)
                {
                    throw new ExecutionError("Psychology Theory không tồn tại!");
                }

                // Cập nhật các trường dữ liệu
                existingTheory.Name = theoryInput.Name;
                existingTheory.Description = theoryInput.Description;
                existingTheory.TopicId = theoryInput.TopicId;
                existingTheory.Author = theoryInput.Author;
                existingTheory.YearPublished = theoryInput.YearPublished;
                existingTheory.TheoryType = theoryInput.TheoryType;
                existingTheory.Principle = theoryInput.Principle;
                existingTheory.Application = theoryInput.Application;
                existingTheory.RelatedTheory = theoryInput.RelatedTheory;
                existingTheory.Criticism = theoryInput.Criticism;
                existingTheory.UpdateAt = DateTime.UtcNow;

                return service.Update(existingTheory).Result;
            }
        );


        Field<BooleanGraphType>(
            "deletePsychologyTheory",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
            ),
            resolve: context =>
            {
                int id = context.GetArgument<int>("id");
                var existingBlog = service.GetById(id);

                if (existingBlog == null)
                {
                    throw new ExecutionError("PsychologyTheory không tồn tại!");
                }

                return service.Delete(id).Result;
            }
        );
    }
}
