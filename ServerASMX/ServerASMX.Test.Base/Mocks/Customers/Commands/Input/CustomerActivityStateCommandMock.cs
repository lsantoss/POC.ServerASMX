using ServerASMX.Domain.Customers.Commands.Input;

namespace ServerASMX.Test.Base.Mocks.Customers.Commands.Input
{
    public static class CustomerActivityStateCommandMock
    {
        public static CustomerActivityStateCommand GetCustomerActivityStateCommand() => new CustomerActivityStateCommand()
        {
            Id = 1,
            Active = true
        };
    }
}
