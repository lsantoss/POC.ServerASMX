using ServerASMX.Domain.Customers.Commands.Input;
using ServerASMX.Domain.Customers.Entities;
using ServerASMX.Test.Base.Mocks.UnitTests.Customers.Commands.Input;
using ServerASMX.Test.Base.Mocks.UnitTests.Customers.Entities;

namespace ServerASMX.Test.Base.Mocks.UnitTests
{
    public class MocksUnitTest
    {
        public Customer Customer { get; }
        public Customer CustomerEdited { get; }
        public CustomerAddCommand CustomerAddCommand { get; }
        public CustomerUpdateCommand CustomerUpdateCommand { get; }
        public CustomerDeleteCommand CustomerDeleteCommand { get; }
        public CustomerActivityStateCommand CustomerActivityStateCommand { get; }

        public MocksUnitTest()
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