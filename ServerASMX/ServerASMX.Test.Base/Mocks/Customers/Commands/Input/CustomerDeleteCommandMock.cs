using ServerASMX.Domain.Customers.Commands.Input;

namespace ServerASMX.Test.Base.Mocks.Customers.Commands.Input
{
    public static class CustomerDeleteCommandMock
    {
        public static CustomerDeleteCommand GetCustomerDeleteCommand() => new CustomerDeleteCommand()
        {
            Id = 1
        };
    }
}
