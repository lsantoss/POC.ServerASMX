using NUnit.Framework;
using ServerASMX.Test.Integration.CustomerService;
using System;

namespace ServerASMX.Test.Integration.Customers.Services
{
    internal class CustomerServiceTest
    {
        [Test]
        public async void Add_Success()
        {
            var client = new CustomerServiceSoapClient();

            var command = new CustomerAddCommand()
            {
                Name = "Lucas",
                Birth = new DateTime(1995, 07, 14),
                Gender = EGender.Male,
                CashBalance = 2151.32m
            };

            var response = await client.AddAsync(command);
            var result = response?.Body?.AddResult;
        }
    }
}