using POC.ServerASMX.Test.Base.CustomerService;

namespace POC.ServerASMX.Test.Base.Mocks.IntegrationTests.Customers.Requests
{
    public static class CustomerDeleteRequestMock
    {
        public static CustomerDeleteCommand GetCustomerDeleteCommand() => new CustomerDeleteCommand()
        {
            Id = 1
        };
    }
}