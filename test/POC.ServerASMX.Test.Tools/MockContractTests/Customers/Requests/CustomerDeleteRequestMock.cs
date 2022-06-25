using POC.ServerASMX.Test.Base.CustomerService;

namespace POC.ServerASMX.Test.Tools.MockContractTests.Customers.Requests
{
    public static class CustomerDeleteRequestMock
    {
        public static CustomerDeleteCommand GetCustomerDeleteCommand() => new CustomerDeleteCommand()
        {
            Id = 1
        };
    }
}