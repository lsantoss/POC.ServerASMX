using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Infra.Enums;
using System;

namespace POC.ServerASMX.Test.Tools.Mocks.Customers.Commands.Input
{
    public static class CustomerAddCommandMock
    {
        public static CustomerAddCommand GetCustomerAddCommand() => new CustomerAddCommand()
        {
            Name = "Lucas Santos",
            Birth = new DateTime(2000, 10, 5),
            Gender = EGender.Male,
            CashBalance = 1500.75m
        };
    }
}
