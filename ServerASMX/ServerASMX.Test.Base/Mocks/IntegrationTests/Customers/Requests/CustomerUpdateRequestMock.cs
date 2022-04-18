using ServerASMX.Test.Base.CustomerService;
using System;

namespace ServerASMX.Test.Base.Mocks.IntegrationTests.Customers.Requests
{
    public static class CustomerUpdateRequestMock
    {
        public static CustomerUpdateCommand GetCustomerUpdateCommand() => new CustomerUpdateCommand()
        {
            Id = 1,
            Name = "Lucas S.",
            Birth = new DateTime(1996, 3, 10),
            Gender = EGender.Male,
            CashBalance = 2200.33m
        };
    }
}