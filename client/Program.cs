using Dummy;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        const string connection = "127.0.0.1:50051";
        static async Task Main(string[] args)
        {
            Grpc.Core.Channel channel= new Grpc.Core.Channel(connection, Grpc.Core.ChannelCredentials.Insecure);

            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == System.Threading.Tasks.TaskStatus.RanToCompletion)
                    Console.WriteLine("The client connected to server: " + connection);
                else
                    Console.WriteLine(task.Status.ToString());
            });

            var client = new Greeting.GreetingService.GreetingServiceClient(channel);  //new DummyService.DummyServiceClient(channel);

            //var greetingObject = new Greeting.Greeting() { FirstName = "Ahmed", LastName = "Tawfik" };
            //var responseTask = client.GreetAsync(new Greeting.GreetingRequest() { Greeting =  greetingObject}).ResponseAsync;
            //Console.WriteLine(responseTask.Result.Result);


            var greetingObject = new Greeting.Greeting() { FirstName = "Ahmed", LastName = "Tawfik" };
            var responseStream = client.GreetManyTimes(new Greeting.GreetManyTimesRequest() { Greeting = greetingObject });

            while (await responseStream.ResponseStream.MoveNext())
            {
                Console.WriteLine(  responseStream.ResponseStream.Current.Result);

               await Task.Delay(2000);
            }


            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
