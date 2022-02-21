using ServerASMX.Domain.Core.Commands.Result;
using ServerASMX.Domain.Customers.Commands.Input;
using System.Threading.Tasks;

namespace ServerASMX.Domain.Customers.Interfaces.Handlers
{
    public interface ICustomerHandler
    {
        Task<CommandResult> Handler(CustomerAddCommand command);
        Task<CommandResult> Handler(CustomerUpdateCommand command);
        Task<CommandResult> Handler(CustomerDeleteCommand command);
    }
}