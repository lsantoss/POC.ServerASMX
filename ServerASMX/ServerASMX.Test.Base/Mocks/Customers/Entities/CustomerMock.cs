using ServerASMX.Domain.Customers.Entities;
using ServerASMX.Domain.Customers.Enums;
using System;

namespace ServerASMX.Test.Base.Mocks.Customers.Entities
{
    public static class CustomerMock
    {
        public static Customer GetCustomer1() => new Customer(1, "Lucas Santos", new DateTime(1995, 7, 14), EGender.Male, 1500.75m, true, DateTime.Now, DateTime.Now.AddDays(1));
        public static Customer GetCustomer2() => new Customer(2, "Renato Silva", new DateTime(2020, 5, 20), EGender.Male, 10.12m, true, DateTime.Now, DateTime.Now.AddDays(3));
        public static Customer GetCustomer3() => new Customer(3, "Marcia Souza", new DateTime(2000, 2, 1), EGender.Female, 3789.89m, false, DateTime.Now, DateTime.Now.AddDays(2));
    }
}
