using Dapper;
using ServerASMX.Domain.Clients.Entities;
using ServerASMX.Domain.Clients.Interfaces.Repositories;
using ServerASMX.Domain.Clients.Queries.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerASMX.Infra.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DynamicParameters _parameters = new DynamicParameters();
        //private readonly SettingsInfraData _settingsInfraData;

        public ClientRepository()
        {

        }

        public Task<long> Insert(Client client)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Client client)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ClientQueryResult> Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<ClientQueryResult>> List()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CheckId(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}