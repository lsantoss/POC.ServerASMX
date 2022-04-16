using ServerASMX.Domain.Customers.Commands.Input;
using ServerASMX.Domain.Customers.Entities;
using ServerASMX.Test.Base.Mocks.Customers.Commands.Input;
using ServerASMX.Test.Base.Mocks.Customers.Entities;

namespace ServerASMX.Test.Base.Mocks
{
    public class MocksTest
    {
        public Customer Customer { get; }
        public Customer CustomerEdited { get; }
        public CustomerAddCommand CustomerAddCommand { get; }
        public CustomerUpdateCommand CustomerUpdateCommand { get; }
        public CustomerDeleteCommand CustomerDeleteCommand { get; }
        public CustomerActivityStateCommand CustomerActivityStateCommand { get; }

        public MocksTest()
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