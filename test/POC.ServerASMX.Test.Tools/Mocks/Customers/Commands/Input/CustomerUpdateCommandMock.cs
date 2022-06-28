using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Domain.Customers.Enums;
using System;

namespace POC.ServerASMX.Test.Tools.Mocks.Customers.Commands.Input
{
    public static class CustomerUpdateCommandMock
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
