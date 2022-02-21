using ServerASMX.Domain.Core.Commands.Result;
using ServerASMX.Domain.Customers.Commands.Input;

namespace ServerASMX.Domain.Customers.Interfaces.Handlers
{
    public interface ICustomerHandler
    {
        CommandResult Handler(CustomerAddCommand command);
        CommandResult Handler(CustomerUpdateCommand command);
        CommandResult Handler(CustomerDeleteCommand command);
    }
}