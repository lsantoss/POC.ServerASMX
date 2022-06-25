using POC.ServerASMX.Domain.Customers.Commands.Input;

namespace POC.ServerASMX.Test.Base.Mocks.UnitTests.Customers.Commands.Input
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
