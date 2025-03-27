using GraphQL;
using GraphQL.Types;
using SPHealthSupportSystem_Repositories.Models;
using SPHealthSupportSystem_Services;

namespace SPHSS_GraghQLAPIService.GraphQLs
{
    public class PsychologyTheoryMutation : ObjectGraphType
    {
        public PsychologyTheoryMutation(IPsychologyTheoryService psychologyTheoryService)
        {
            // Tạo đơn hàng mới
            Field<IntGraphType>(
                "createPsychologyTheory",
                description: "Create a new order.",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<PsychologyTheoryInputType>> { Name = "psychologyTheory" }),
                resolve: context =>
                {
                    var psychologyTheory = context.GetArgument<PsychologyTheory>("psychologyTheory");

                    // Kiểm tra dữ liệu đầu vào
                    if (psychologyTheory == null)
                    {
                        throw new ExecutionError("PsychologyTheory input cannot be null.");
                    }

                    // Tạo đơn hàng mới
                    return psychologyTheoryService.Create(psychologyTheory).Result;
                }
            );

            // Cập nhật đơn hàng
            Field<IntGraphType>(
                "updatePsychologyTheory",
                description: "Update an existing psychology theory.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<PsychologyTheoryInputType>> { Name = "psychologyTheory" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var psychologyTheory = context.GetArgument<PsychologyTheory>("psychologyTheory");

                    // Kiểm tra dữ liệu đầu vào
                    if (psychologyTheory == null)
                    {
                        throw new ExecutionError("PsychologyTheory input cannot be null.");
                    }

                    // Đảm bảo ID của đơn hàng được cập nhật
                    psychologyTheory.Id = id;

                    // Cập nhật đơn hàng
                    return psychologyTheoryService.Update(psychologyTheory).Result;
                }
            );

            // Xóa đơn hàng theo ID
            Field<BooleanGraphType>(
                "deletePsychologyTheory",
                description: "Delete an PsychologyTheory by ID.",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");

                    // Xóa đơn hàng
                    return psychologyTheoryService.Delete(id).Result;
                }
            );
        }
    }
}