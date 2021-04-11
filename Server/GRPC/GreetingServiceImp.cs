using Greeting;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Greeting.GreetingService;

namespace Server.GRPC
{
    public class GreetingServiceImp : GreetingServiceBase
    {
        public override Task<GreetingResponse> Greet(GreetingRequest request, ServerCallContext context)
        {
            string result = $"{request.Greeting.FirstName} {request.Greeting.LastName}";


            return Task.FromResult(new GreetingResponse() { Result = result });
        }

        public override async Task GreetManyTimes(GreetManyTimesRequest request, IServerStreamWriter<GreetManyTimesResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine("the server received the request ");
            string result = $"{request.Greeting.FirstName} {request.Greeting.LastName}";

            for (int i = 0; i < 10; i++)
            {
               await responseStream.WriteAsync(new GreetManyTimesResponse() { Result = result + i });
            }
        }
    }
}
