using POC.ServerASMX.Test.Base.CustomerService;

namespace POC.ServerASMX.Test.Base.Mocks.IntegrationTests.Customers.Requests
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