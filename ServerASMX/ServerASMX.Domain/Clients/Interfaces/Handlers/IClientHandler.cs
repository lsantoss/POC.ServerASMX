using ServerASMX.Domain.Clients.Commands.Input;
using ServerASMX.Domain.Core.Commands.Interfaces;

namespace ServerASMX.Domain.Clients.Interfaces.Handlers
{
    public interface IClientHandler
    {
        ICommandResult Handler(ClientAddCommand command);
        ICommandResult Handler(ClientUpdateCommand command);
        ICommandResult Handler(ClientDeleteCommand command);
    }
}