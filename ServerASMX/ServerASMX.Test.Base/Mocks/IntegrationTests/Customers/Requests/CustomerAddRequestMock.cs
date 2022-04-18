using ServerASMX.Test.Base.CustomerService;
using System;

namespace ServerASMX.Test.Base.Mocks.IntegrationTests.Customers.Requests
{
    public static class CustomerAddRequestMock
    {
        public static CustomerAddCommand GetCustomerAddCommand() => new CustomerAddCommand()
        {
            Name = "Lucas Santos",
            Birth = new DateTime(1995, 7, 14),
            Gender = EGender.Male,
            CashBalance = 1500.75m
        };
    }
}