using ServerASMX.Domain.Clients.Entities;
using ServerASMX.Domain.Clients.Queries.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerASMX.Domain.Clients.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<long> Insert(Client client);
        Task Update(Client client);
        Task Delete(long id);

        Task<ClientQueryResult> Get(long id);
        Task<List<ClientQueryResult>> List();

        Task<bool> CheckId(long id);
    }
}