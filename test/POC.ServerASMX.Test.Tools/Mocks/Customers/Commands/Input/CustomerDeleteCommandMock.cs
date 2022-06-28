using POC.ServerASMX.Domain.Customers.Commands.Input;

namespace POC.ServerASMX.Test.Tools.Mocks.Customers.Commands.Input
{
    public static class CustomerDeleteCommandMock
    {
        public static CustomerDeleteCommand GetCustomerDeleteCommand() => new CustomerDeleteCommand()
        {
            Id = 1
        };
    }
}
