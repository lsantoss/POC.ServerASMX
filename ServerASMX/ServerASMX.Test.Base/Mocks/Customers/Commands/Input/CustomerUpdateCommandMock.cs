using ServerASMX.Domain.Customers.Commands.Input;
using ServerASMX.Domain.Customers.Enums;
using System;

namespace ServerASMX.Test.Base.Mocks.Customers.Commands.Input
{
    public static class CustomerUpdateCommandMock
    {
        public static CustomerUpdateCommand GetCustomerUpdateCommand() => new CustomerUpdateCommand()
        {
            Id = 1,
            Name = "Lucas Santos",
            Birth = new DateTime(1995, 7, 14),
            Gender = EGender.Male,
            CashBalance = 1500.75m
        };
    }
}
