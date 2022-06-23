using POC.ServerASMX.Infra.Commands.Result;
using POC.ServerASMX.Domain.Customers.Commands.Input;
using System.Threading.Tasks;

namespace POC.ServerASMX.Domain.Customers.Interfaces.Handlers
{
    public interface ICustomerHandler
    {
        Task<CommandResult> HandleAsync(CustomerAddCommand command);
        Task<CommandResult> HandleAsync(CustomerUpdateCommand command);
        Task<CommandResult> HandleAsync(CustomerActivityStateCommand command);
        Task<CommandResult> HandleAsync(CustomerDeleteCommand command);
    }
}