using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SPHealthSupportSystem_GraphQL_Client;
using SPHealthSupportSystem_GraphQL_Client.GraphQLService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IGraphQLClient>(sp =>
    new GraphQLHttpClient("https://localhost:7041/graphql", new NewtonsoftJsonSerializer()));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<GraphQLService>();
await builder.Build().RunAsync();
