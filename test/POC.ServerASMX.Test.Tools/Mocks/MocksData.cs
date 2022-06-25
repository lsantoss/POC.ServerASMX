using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Domain.Customers.Entities;
using POC.ServerASMX.Test.Base.Mocks.UnitTests.Customers.Commands.Input;
using POC.ServerASMX.Test.Base.Mocks.UnitTests.Customers.Entities;

namespace POC.ServerASMX.Test.Base.Mocks.UnitTests
{
    public class MocksData
    {
        public Customer Customer { get; }
        public Customer CustomerEdited { get; }
        public CustomerAddCommand CustomerAddCommand { get; }
        public CustomerUpdateCommand CustomerUpdateCommand { get; }
        public CustomerDeleteCommand CustomerDeleteCommand { get; }
        public CustomerActivityStateCommand CustomerActivityStateCommand { get; }

        public MocksData()
        {
            Customer = CustomerMock.GetCustomer();
            CustomerEdited = CustomerMock.GetCustomerEdited();
            CustomerAddCommand = CustomerAddCommandMock.GetCustomerAddCommand();
            CustomerUpdateCommand = CustomerUpdateCommandMock.GetCustomerUpdateCommand();
            CustomerDeleteCommand = CustomerDeleteCommandMock.GetCustomerDeleteCommand();
            CustomerActivityStateCommand = CustomerActivityStateCommandMock.GetCustomerActivityStateCommand();
        }
    }
}