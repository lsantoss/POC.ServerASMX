using Dapper;
using ServerASMX.Domain.Clients.Entities;
using ServerASMX.Domain.Clients.Interfaces.Repositories;
using ServerASMX.Domain.Clients.Queries.Results;
using System.Collections.Generic;

namespace ServerASMX.Infra.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DynamicParameters _parameters = new DynamicParameters();
        //private readonly SettingsInfraData _settingsInfraData;

        public ClientRepository()
        {

        }

        public long Insert(Client client)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Client client)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public ClientQueryResult Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public List<ClientQueryResult> List()
        {
            throw new System.NotImplementedException();
        }

        public bool CheckId(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}