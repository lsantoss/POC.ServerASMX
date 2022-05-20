using POC.ServerASMX.Domain.Core.Commands.Result;
using POC.ServerASMX.Domain.Customers.Commands.Input;

namespace POC.ServerASMX.Domain.Customers.Interfaces.Handlers
{
    public interface ICustomerHandler
    {
        CommandResult Handle(CustomerAddCommand command);
        CommandResult Handle(CustomerUpdateCommand command);
        CommandResult Handle(CustomerActivityStateCommand command);
        CommandResult Handle(CustomerDeleteCommand command);
    }
}