using POC.ServerASMX.Infra.Data.Customers.DTOs;
using POC.ServerASMX.Infra.Enums;
using System;

namespace POC.ServerASMX.Test.Tools.Mocks.Customers.DTOs
{
    public static class CustomerDTOMock
    {
        public static CustomerDTO GetCustomerDTO() => new CustomerDTO(1, "Lucas Santos", new DateTime(2000, 10, 5), EGender.Male, 1500.75m, true, DateTime.Now, null);
        public static CustomerDTO GetCustomerDTOEdited() => new CustomerDTO(1, "Lucas S.", new DateTime(2020, 5, 20), EGender.Male, 2200.33m, true, DateTime.Now, null);
    }
}
