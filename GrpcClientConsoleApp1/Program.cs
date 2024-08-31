using Grpc.Core;
using Grpc.Net.Client;
using GrpcService1;

namespace GrpcClientConsoleApp1;

public class Program
{
    public static async Task Main(string[] args)
    {
        /*
        var input = new HelloRequest { Name = "Tejas" };
        var channel = GrpcChannel.ForAddress("http://localhost:5058");
        var client = new Greeter.GreeterClient(channel);

        var reply = await client.SayHelloAsync(input);
        Console.WriteLine(reply.Message);
        */

        var input = new CustomerLookupModel { UserId = 1 };
        var channel = GrpcChannel.ForAddress("http://localhost:5058");
        var client = new Customers.CustomersClient(channel);

        /*
        int i;
        do
        {
            var reply = await client.GetCustomerInfoAsync(input);

            Console.WriteLine($"{reply.FirstName} {reply.LastName} {reply.EmailAddress} {reply.IsAlive} {reply.Age} \n");
            
            i = Convert.ToInt32(Console.ReadLine());
            input.UserId = i;
        } while( i != 0 && i <= 3);
        */ 

        using (var call = client.GetNewCustomers(new NewCustomerRequest()))
        {
            while (await call.ResponseStream.MoveNext())
            {
                var currentCustomer = call.ResponseStream.Current;

                Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName} {currentCustomer.EmailAddress} {currentCustomer.IsAlive} {currentCustomer.Age}");
            }
        }
    }
}