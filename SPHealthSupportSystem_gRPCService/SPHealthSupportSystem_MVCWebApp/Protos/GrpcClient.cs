using Grpc.Net.Client;
using SPHealthSupportSystem_gRPCService.Protos;

namespace SPHealthSupportSystem_MVCWebApp.Protos
{
    public class GrpcClient
    {
        private readonly PsychologyTheoryService.PsychologyTheoryServiceClient _client;

        public GrpcClient(string grpcServerUrl)
        {
            // Create a gRPC channel. (If using HTTP/2 over TLS, ensure your URL starts with https)
            var channel = GrpcChannel.ForAddress(grpcServerUrl);
            _client = new PsychologyTheoryService.PsychologyTheoryServiceClient(channel);
        }

        public PsychologyTheoryService.PsychologyTheoryServiceClient Client => _client;
    }
}
