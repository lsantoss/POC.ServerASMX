using ServerASMX.Domain.Customers.Commands.Input;
using ServerASMX.Domain.Customers.Entities;
using ServerASMX.Test.Base.Mocks.Customers.Commands.Input;
using ServerASMX.Test.Base.Mocks.Customers.Entities;

namespace ServerASMX.Test.Base.Mocks
{
    public class MocksTest
    {
        public Customer Customer1 { get; }
        public Customer Customer2 { get; }
        public Customer Customer3 { get; }
        public CustomerAddCommand CustomerAddCommand { get; }
        public CustomerUpdateCommand CustomerUpdateCommand { get; }
        public CustomerDeleteCommand CustomerDeleteCommand { get; }
        public CustomerActivityStateCommand CustomerActivityStateCommand { get; }

        public MocksTest()
        {
            Customer1 = CustomerMock.GetCustomer1();
            Customer2 = CustomerMock.GetCustomer2();
            Customer3 = CustomerMock.GetCustomer3();
            CustomerAddCommand = CustomerAddCommandMock.GetCustomerAddCommand();
            CustomerUpdateCommand = CustomerUpdateCommandMock.GetCustomerUpdateCommand();
            CustomerDeleteCommand = CustomerDeleteCommandMock.GetCustomerDeleteCommand();
            CustomerActivityStateCommand = CustomerActivityStateCommandMock.GetCustomerActivityStateCommand();
        }
    }
}