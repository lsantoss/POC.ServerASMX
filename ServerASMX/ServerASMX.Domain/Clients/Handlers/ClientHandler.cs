using ServerASMX.Domain.Clients.Commands.Input;
using ServerASMX.Domain.Clients.Interfaces.Handlers;
using ServerASMX.Domain.Clients.Interfaces.Repositories;
using ServerASMX.Domain.Clients.Repositories;
using ServerASMX.Domain.Core.Commands.Result;
using System;

namespace ServerASMX.Domain.Clients.Handlers
{
    public class ClientHandler : IClientHandler
    {
        private readonly IClientRepository _repository;

        public ClientHandler()
        {
            _repository = new ClientRepository();
        }

        public CommandResult Handler(ClientAddCommand command)
        {
            throw new NotImplementedException();
        }

        public CommandResult Handler(ClientUpdateCommand command)
        {
            throw new NotImplementedException();
        }

        public CommandResult Handler(ClientDeleteCommand command)
        {
            throw new NotImplementedException();
        }
    }
}