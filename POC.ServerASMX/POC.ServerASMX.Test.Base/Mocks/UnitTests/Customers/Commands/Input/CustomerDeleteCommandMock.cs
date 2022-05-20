using POC.ServerASMX.Domain.Customers.Commands.Input;

namespace POC.ServerASMX.Test.Base.Mocks.UnitTests.Customers.Commands.Input
{
    public static class CustomerDeleteCommandMock
    {
        public static CustomerDeleteCommand GetCustomerDeleteCommand() => new CustomerDeleteCommand()
        {
            Id = 1
        };
    }
}
