using ServerASMX.Test.Base.CustomerService;

namespace ServerASMX.Test.Base.Mocks.IntegrationTests.Customers.Requests
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