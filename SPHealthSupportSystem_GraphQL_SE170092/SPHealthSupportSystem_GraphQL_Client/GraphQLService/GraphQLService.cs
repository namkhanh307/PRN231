using GraphQL;
using GraphQL.Client.Abstractions;
using Newtonsoft.Json;
using SPHealthSupportSystem_GraphQL_Client.Models;
using System.Text.Json;
using System.Threading.Tasks;

namespace SPHealthSupportSystem_GraphQL_Client.GraphQLService
{
    public class GraphQLService
    {
        private readonly IGraphQLClient _graphQLClient;

        public GraphQLService(IGraphQLClient graphQLClient)
        {
            _graphQLClient = graphQLClient ?? throw new ArgumentNullException(nameof(graphQLClient));
        }

        public async Task<T> ExecuteQueryAsync<T>(GraphQLRequest request)
        {
            try
            {
                var response = await _graphQLClient.SendQueryAsync<T>(request);

                if (response == null)
                {
                    throw new ApplicationException("GraphQL response is null.");
                }

                if (response.Errors != null && response.Errors.Any())
                {
                    string errorMessages = string.Join(", ", response.Errors.Select(e => e.Message));
                    throw new ApplicationException($"GraphQL Error: {errorMessages}");
                }

                if (response.Data == null)
                {
                    throw new ApplicationException("GraphQL response data is null.");
                }

                return response.Data;
            }
            catch (HttpRequestException httpEx)
            {
                throw new ApplicationException($"HTTP Request Error: {httpEx.Message}", httpEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Unknown Error in GraphQL request: {ex.Message}", ex);
            }
        }

        public async Task<List<PsychologyTheory>> GetPsychologyTheoriesAsync()
        {
            var query = new GraphQLRequest
            {
                Query = @"
                {
                    psychologyTheories {
                        id
                        name
                        description
                        topicId
                        author
                        yearPublished
                        theoryType
                        principle
                        application
                        relatedTheory
                        criticism
                    }
                }"
            };

            try
            {
                var response = await _graphQLClient.SendQueryAsync<PsychologyTheoriesResponse>(query);
                Console.WriteLine("Response Data: " + JsonConvert.SerializeObject(response.Data));
                if (response?.Data?.list == null)
                {
                    throw new ApplicationException("No data received from GraphQL API.");
                }

                var theories = response.Data.list;

       
                foreach (var theory in theories)
                {
                    var topic = await GetTopicByIdAsync(theory.TopicId);
                    theory.TopicName = topic?.Name;  
                }
                return theories;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> CreatePsychologyTheoryAsync(PsychologyTheory theory)
        {
            var mutation = new GraphQLRequest
            {
                Query = @"
        mutation($input: psychologyTheoryInput!) {
             psychologyTheory {
                createPsychologyTheory(psychologyTheoryInput: $input)
            }
        }",
                  Variables = new
                {
                    input = new
                    {
                        id = theory.Id,
                        name = theory.Name,
                        description = theory.Description,
                        topicId = theory.TopicId,
                        author = theory.Author,
                        yearPublished = theory.YearPublished.HasValue ? theory.YearPublished.Value : (int?)null,
                        theoryType = theory.TheoryType,
                        principle = theory.Principle,
                        application = theory.Application,
                        relatedTheory = theory.RelatedTheory,
                        criticism = theory.Criticism
                    }
                }
            };

            var response = await _graphQLClient.SendMutationAsync<dynamic>(mutation);
            if (response.Errors != null && response.Errors.Any())
            {
                throw new ApplicationException($"GraphQL Error: {string.Join(", ", response.Errors.Select(e => e.Message))}");
            }

            return response.Data.psychologyTheory.createPsychologyTheory;
        }


        public async Task<int> UpdatePsychologyTheoryAsync(PsychologyTheory theory)
        {
            var mutation = new GraphQLRequest
            {
                Query = @"
        mutation($input: psychologyTheoryInput!) {
            psychologyTheory {
                updatePsychologyTheory(psychologyTheoryInput: $input)
            }
        }",
                Variables = new
                {
                    input = new
                    {
                        id = theory.Id,
                        name = theory.Name,
                        description = theory.Description,
                        topicId = theory.TopicId,
                        author = theory.Author,
                        yearPublished = theory.YearPublished,
                        theoryType = theory.TheoryType,
                        principle = theory.Principle,
                        application = theory.Application,
                        relatedTheory = theory.RelatedTheory,
                        criticism = theory.Criticism
                    }
                }
            };

            var response = await _graphQLClient.SendMutationAsync<dynamic>(mutation);

            if (response.Errors != null && response.Errors.Any())
            {
                foreach (var error in response.Errors)
                {
                    Console.WriteLine($"Error: {error.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Response Data: {response.Data.updatePsychologyTheory}");
            }

            if (response.Errors != null && response.Errors.Any())
            {
                throw new ApplicationException($"GraphQL Error: {string.Join(", ", response.Errors.Select(e => e.Message))}");
            }

            return response.Data.psychologyTheory.updatePsychologyTheory;
        }


        public async Task<bool> DeletePsychologyTheoryAsync(int id)
        {
            var mutation = new GraphQLRequest
            {
                Query = @"
                    mutation($id: Int!) {
                        psychologyTheory{
                            deletePsychologyTheory(id: $id)
                    }
                }",
                Variables = new { id }
            };

            var response = await _graphQLClient.SendMutationAsync<dynamic>(mutation);
            return response.Data.deletePsychologyTheory ?? false;
        }

        public async Task<Topic> GetTopicByIdAsync(int topicId)
        {
            var query = new GraphQLRequest
            {
                Query = @"
        query($id: Int!) {
            topic(id: $id) {
                id
                name
            }
        }",
                Variables = new { id = topicId }
            };

            try
            {
                var response = await _graphQLClient.SendQueryAsync<TopicResponse>(query);
                return response?.Data?.Topic;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task<List<Topic>> GetAllTopicsAsync()
        {
            var query = new GraphQLRequest
            {
                Query = @"
        query {
            topics {
                id
                name
            }
        }"
            };

            var response = await _graphQLClient.SendQueryAsync<TopicsResponse>(query);
            return response?.Data?.list ?? new List<Topic>();
        }
        public class TopicResponse
        {
            public Topic Topic { get; set; }
        }

     
        public class PsychologyTheoriesResponse
        {
            [JsonProperty("psychologyTheories")]
            public List<PsychologyTheory> list { get; set; } = new();
        }
        public class TopicsResponse
        {
            [JsonProperty("topics")]
            public List<Topic> list { get; set; } = new();
        }
        private class CreateResponse
        {
            public int id { get; set; }
        }

        private class UpdateResponse
        {
            public int id { get; set; }
        }

   
    }
}
