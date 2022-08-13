using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Domain.Customers.Entities;
using POC.ServerASMX.Infra.Data.Customers.DTOs;
using POC.ServerASMX.Test.Tools.Mocks.Customers.Commands.Input;
using POC.ServerASMX.Test.Tools.Mocks.Customers.DTOs;
using POC.ServerASMX.Test.Tools.Mocks.Customers.Entities;

namespace POC.ServerASMX.Test.Tools.Mocks
{
    public class MockData
    {
        public Customer Customer { get; }
        public Customer CustomerEdited { get; }
        public CustomerDTO CustomerDTO { get; }
        public CustomerDTO CustomerDTOEdited { get; }
        public CustomerAddCommand CustomerAddCommand { get; }
        public CustomerUpdateCommand CustomerUpdateCommand { get; }
        public CustomerDeleteCommand CustomerDeleteCommand { get; }
        public CustomerActivityStateCommand CustomerActivityStateCommand { get; }

        public MockData()
        {
            Customer = CustomerMock.GetCustomer();
            CustomerEdited = CustomerMock.GetCustomerEdited();
            CustomerDTO = CustomerDTOMock.GetCustomerDTO();
            CustomerDTOEdited = CustomerDTOMock.GetCustomerDTOEdited();
            CustomerAddCommand = CustomerAddCommandMock.GetCustomerAddCommand();
            CustomerUpdateCommand = CustomerUpdateCommandMock.GetCustomerUpdateCommand();
            CustomerDeleteCommand = CustomerDeleteCommandMock.GetCustomerDeleteCommand();
            CustomerActivityStateCommand = CustomerActivityStateCommandMock.GetCustomerActivityStateCommand();
        }
    }
}