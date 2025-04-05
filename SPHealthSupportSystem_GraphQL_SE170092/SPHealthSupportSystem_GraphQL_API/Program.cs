using GraphQL;
using GraphQL.Types;
using SPHealthSupportSystem_Services;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IPsychologyTheoryService, PsychologyTheoryService>();
builder.Services.AddScoped<ITopicService, TopicService>();

// Add services to the container.
builder.Services.AddScoped<PsychologyTheoryMutation>();
//builder.Services.AddScoped<CategoryMutation>();
builder.Services.AddScoped<PsychologyTheoryQuery>();
builder.Services.AddScoped<TopicQuery>();

builder.Services.AddScoped<ISchema, AppSchema>(sp => new AppSchema(sp));

builder.Services.AddGraphQL(builder =>
{
    builder.AddSystemTextJson();
    builder.AddGraphTypes();
});


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("https://localhost:7075") // Allow Blazor WebAssembly
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // If using authentication
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorClient");

// Middleware GraphQL
app.UseGraphQL<ISchema>();
app.UseGraphQLPlayground("/ui/playground"); // Thêm Playground để test API GraphQL

app.UseAuthorization();
app.MapControllers();

app.Run();
