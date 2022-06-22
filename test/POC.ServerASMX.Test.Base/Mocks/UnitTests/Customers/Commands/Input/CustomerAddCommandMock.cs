using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Domain.Customers.Enums;
using System;

namespace POC.ServerASMX.Test.Base.Mocks.UnitTests.Customers.Commands.Input
{
    public static class CustomerAddCommandMock
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
