using Grpc.Net.Client;
using SPHealthSupportSystem_gRPCService.Protos;

namespace SPHSS_gRPC_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Welcome to gRPC Client!");

            using var channel = GrpcChannel.ForAddress("https://localhost:7132");
            var client = new PsychologyTheoryService.PsychologyTheoryServiceClient(channel);

            Console.WriteLine("Get All Psychology Theory");
            var psychologyTheories = client.GetAll(new Empty());
            if (psychologyTheories != null && psychologyTheories.Theories.Count > 0)
            {
                foreach (var pt in psychologyTheories.Theories)
                {
                    Console.WriteLine(string.Format("{0} - {1}", pt.Id, pt.Description));
                }
            }
            else
            {
                Console.WriteLine("No Psychology Theory found");
            }
            Console.WriteLine("\r\nGetById{Id={1}):");
            var psychologyTheory = client.GetById(new PsychologyTheoryRequest { Id = 1 });
            Console.WriteLine(string.Format("{0} - {1}", psychologyTheory.Id, psychologyTheory.Description));

            Console.ReadLine();
        }
    }
}
