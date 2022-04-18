using ServerASMX.Domain.Customers.Entities;
using ServerASMX.Domain.Customers.Enums;
using System;

namespace ServerASMX.Test.Base.Mocks.UnitTests.Customers.Entities
{
    public static class CustomerMock
    {
        public static Customer GetCustomer() => new Customer(1, "Lucas Santos", new DateTime(1995, 7, 14), EGender.Male, 1500.75m, true, DateTime.Now, null);
        public static Customer GetCustomerEdited() => new Customer(1, "Lucas S.", new DateTime(2020, 5, 20), EGender.Male, 2200.33m, true, DateTime.Now, null);
    }
}
