using ServerASMX.Domain.Clients.Entities;
using ServerASMX.Domain.Clients.Queries.Results;
using System.Collections.Generic;

namespace ServerASMX.Domain.Clients.Interfaces.Repositories
{
    public interface IClientRepository
    {
        long Insert(Client client);
        void Update(Client client);
        void Delete(long id);

        ClientQueryResult Get(long id);
        List<ClientQueryResult> List();

        bool CheckId(long id);
    }
}