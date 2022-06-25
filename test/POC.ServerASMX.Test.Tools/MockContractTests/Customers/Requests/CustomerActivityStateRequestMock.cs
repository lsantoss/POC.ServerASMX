using POC.ServerASMX.Test.Base.CustomerService;

namespace POC.ServerASMX.Test.Tools.MockContractTests.Customers.Requests
{
    public static class CustomerActivityStateRequestMock
    {
        public static CustomerActivityStateCommand GetCustomerActivityStateCommand() => new CustomerActivityStateCommand()
        {
            Id = 1,
            Active = false
        };
    }
}