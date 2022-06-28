using POC.ServerASMX.Domain.Customers.Commands.Input;

namespace POC.ServerASMX.Test.Tools.Mocks.Customers.Commands.Input
{
    public static class CustomerActivityStateCommandMock
    {
        public static CustomerActivityStateCommand GetCustomerActivityStateCommand() => new CustomerActivityStateCommand()
        {
            Id = 1,
            Active = false
        };
    }
}
