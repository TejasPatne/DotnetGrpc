using System;
using Grpc.Core;

namespace GrpcService1.Services;

public class CustomersService : Customers.CustomersBase
{
    private readonly ILogger<CustomersService> _logger;

    public CustomersService(ILogger<CustomersService> logger)
    {
        _logger = logger;
    }

    public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
    {
        CustomerModel customer = new CustomerModel();

        if (request.UserId == 1)
        {
            customer.FirstName = "Tejas";
            customer.LastName = "Patne";
            customer.EmailAddress = "X7qI0@example.com";
            customer.IsAlive = true;
            customer.Age = 21;
        }
        else if (request.UserId == 2)
        {
            customer.FirstName = "Tushar";
            customer.LastName = "Patne";
            customer.EmailAddress = "X7qI1@example.com";
            customer.IsAlive = true;
            customer.Age = 25;
        }
        else
        {
            customer.FirstName = "Parth";
            customer.LastName = "Patne";
            customer.EmailAddress = "X7qI2@example.com";
            customer.IsAlive = true;
            customer.Age = 17;
        }

        return Task.FromResult(customer);
    }

    public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
    {
        List<CustomerModel> customers = new List<CustomerModel> 
        {
            new CustomerModel
            {
                FirstName = "Tejas",
                LastName = "Patne",
                EmailAddress = "X7qI0@example.com",
                IsAlive = true,
                Age = 21
            },
            new CustomerModel
            {
                FirstName = "Tushar",
                LastName = "Patne",
                EmailAddress = "X7qI1@example.com",
                IsAlive = true,
                Age = 25
            },
            new CustomerModel
            {
                FirstName = "Parth",
                LastName = "Patne",
                EmailAddress = "X7qI2@example.com",
                IsAlive = true,
                Age = 17
            }
        };

        foreach(var cust in customers)
        {
            await Task.Delay(2000);
            await responseStream.WriteAsync(cust);
        }
    }
}