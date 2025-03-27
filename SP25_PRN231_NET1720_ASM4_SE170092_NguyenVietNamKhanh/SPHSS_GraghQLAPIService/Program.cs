using GraphQL;
using GraphQL.Types;
using GraphQL.Server;
using SPHealthSupportSystem_Repositories;
using SPHealthSupportSystem_Services;
using SPHSS_GraghQLAPIService.GraphQLs;
using System.Text.Json.Serialization;
using GraphQL.MicrosoftDI;

namespace SPHSS_GraghQLAPIService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register your repositories and services
            builder.Services.AddScoped<PsychologyTheoryRepository>();
            builder.Services.AddScoped<UserAccountRepository>();
            builder.Services.AddScoped<IPsychologyTheoryService, PsychologyTheoryService>();
            builder.Services.AddScoped<ITopicService, TopicService>();
            builder.Services.AddScoped<UserAccountService>();

            builder.Services.AddScoped<PsychologyTheoryInputType>();
            builder.Services.AddScoped<PsychologyTheoryMutation>();
            builder.Services.AddScoped<PsychologyTheoryQuery>();
            builder.Services.AddScoped<PsychologyTheorySchema>();
            builder.Services.AddScoped<PsychologyTheoryType>();
            builder.Services.AddScoped<TopicType>();

            // Register GraphQL schema and types
            builder.Services.AddScoped<ISchema, PsychologyTheorySchema>(services => new PsychologyTheorySchema(new SelfActivatingServiceProvider(services)));

            // Register GraphQL.NET server
            builder.Services.AddGraphQL(builder => // Add configuration action
            {
                builder.AddSystemTextJson(); // Add JSON serializer
                builder.AddGraphTypes(); // Register GraphQL types
            });

            var app = builder.Build();

            // Enable middleware (Swagger, GraphQL, etc.)
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Enable GraphQL HTTP endpoint at /graphql
            app.UseGraphQL<ISchema>();

            // Optional: GraphQL Playground UI
            app.UseGraphQLPlayground(); // available at /ui/playground

            app.Run();
        }
    }
}
