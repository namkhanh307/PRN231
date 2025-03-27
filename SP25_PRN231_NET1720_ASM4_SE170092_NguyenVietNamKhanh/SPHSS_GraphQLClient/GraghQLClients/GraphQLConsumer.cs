using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace SPHSS_GraphQLClient.GraghQLClients
{
    public class GraphQLConsumer
    {
        private static string APIEndPoint = "https://localhost:7079/graphql/";
        private readonly GraphQLHttpClient _graphqlClient = new GraphQLHttpClient(APIEndPoint, new NewtonsoftJsonSerializer());
    }
}
