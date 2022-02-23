using ServerASMX.Domain.Core.Commands.Result;
using ServerASMX.Domain.Customers.Commands.Input;

namespace ServerASMX.Domain.Customers.Interfaces.Handlers
{
    public interface ICustomerHandler
    {
        CommandResult Handle(CustomerAddCommand command);
        CommandResult Handle(CustomerUpdateCommand command);
        CommandResult Handle(CustomerActivityStateCommand command);
        CommandResult Handle(CustomerDeleteCommand command);
    }
}