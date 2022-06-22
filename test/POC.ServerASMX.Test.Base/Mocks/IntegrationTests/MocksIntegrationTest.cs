using POC.ServerASMX.Test.Base.CustomerService;
using POC.ServerASMX.Test.Base.Mocks.IntegrationTests.Customers.Requests;

namespace POC.ServerASMX.Test.Base.Mocks.IntegrationTests
{
    public class MocksIntegrationTest
    {
        public CustomerAddCommand CustomerAddCommand { get; }
        public CustomerUpdateCommand CustomerUpdateCommand { get; }
        public CustomerDeleteCommand CustomerDeleteCommand { get; }
        public CustomerActivityStateCommand CustomerActivityStateCommand { get; }

        public MocksIntegrationTest()
        {
            CustomerAddCommand = CustomerAddRequestMock.GetCustomerAddCommand();
            CustomerUpdateCommand = CustomerUpdateRequestMock.GetCustomerUpdateCommand();
            CustomerDeleteCommand = CustomerDeleteRequestMock.GetCustomerDeleteCommand();
            CustomerActivityStateCommand = CustomerActivityStateRequestMock.GetCustomerActivityStateCommand();
        }
    }
}