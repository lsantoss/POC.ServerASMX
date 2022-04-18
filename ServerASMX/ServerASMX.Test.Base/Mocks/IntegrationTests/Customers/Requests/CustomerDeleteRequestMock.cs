using ServerASMX.Test.Base.CustomerService;

namespace ServerASMX.Test.Base.Mocks.IntegrationTests.Customers.Requests
{
    public static class CustomerDeleteRequestMock
    {
        public static CustomerDeleteCommand GetCustomerDeleteCommand() => new CustomerDeleteCommand()
        {
            Id = 1
        };
    }
}