using Server.GRPC;
using System;


namespace Server
{
    class Program
    {
        const int port= 50051;
        static void Main(string[] args)
        {
            Grpc.Core.Server server = null;

            try
            {
                server = new Grpc.Core.Server()
                {
                    // bind grpc service with its implenetation that server can route the client call to that implementation.
                    Services = { Greeting.GreetingService.BindService(new GreetingServiceImp())},
                    Ports = { new Grpc.Core.ServerPort("localhost", port, Grpc.Core.ServerCredentials.Insecure) }
                };

                server.Start();
                Console.WriteLine("Server started on port : " + port);

                Console.WriteLine("Server started on port : " + server.Ports);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }finally
            {
                if(server != null)
                {
                    server.ShutdownAsync().Wait();
                }
            }
        }
    }
}
