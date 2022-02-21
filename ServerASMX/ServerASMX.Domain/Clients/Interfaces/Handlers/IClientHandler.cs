using ServerASMX.Domain.Clients.Commands.Input;
using ServerASMX.Domain.Core.Commands.Result;

namespace ServerASMX.Domain.Clients.Interfaces.Handlers
{
    public interface IClientHandler
    {
        CommandResult Handler(ClientAddCommand command);
        CommandResult Handler(ClientUpdateCommand command);
        CommandResult Handler(ClientDeleteCommand command);
    }
}