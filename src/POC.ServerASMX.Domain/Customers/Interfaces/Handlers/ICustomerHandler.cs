using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Infra.Commands.Result;

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