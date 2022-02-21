using Dapper;
using ServerASMX.Domain.Clients.Entities;
using ServerASMX.Domain.Clients.Interfaces.Repositories;
using ServerASMX.Domain.Clients.Queries.Results;
using ServerASMX.Domain.Clients.Repositories.Queries;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ServerASMX.Domain.Clients.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString;
        private readonly DynamicParameters _parameters;

        public ClientRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            _parameters = new DynamicParameters();
        }

        public async Task<long> Insert(Client client)
        {
            _parameters.Add("Name", client.Name, DbType.String);
            _parameters.Add("Birth", client.Birth, DbType.DateTime);
            _parameters.Add("Gender", client.Gender, DbType.Int16);
            _parameters.Add("CashBalance", client.CashBalance, DbType.Decimal);
            _parameters.Add("Active", client.Active, DbType.Boolean);
            _parameters.Add("CreationDate", client.CreationDate, DbType.DateTime);
            _parameters.Add("ChangeDate", client.ChangeDate, DbType.DateTime);

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteScalarAsync<long>(ClientQueries.Insert, _parameters);
            }
        }

        public async Task Update(Client client)
        {
            _parameters.Add("Id", client.Id, DbType.Int64);
            _parameters.Add("Name", client.Name, DbType.String);
            _parameters.Add("Birth", client.Birth, DbType.DateTime);
            _parameters.Add("Gender", client.Gender, DbType.Int16);
            _parameters.Add("CashBalance", client.CashBalance, DbType.Decimal);
            _parameters.Add("Active", client.Active, DbType.Boolean);
            _parameters.Add("CreationDate", client.CreationDate, DbType.DateTime);
            _parameters.Add("ChangeDate", client.ChangeDate, DbType.DateTime);

            using (var connection = new SqlConnection(_connectionString))
            {
                _ = await connection.ExecuteAsync(ClientQueries.Update, _parameters);
            }
        }

        public async Task Delete(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_connectionString))
            {
                _ = await connection.ExecuteAsync(ClientQueries.Delete, _parameters);
            }
        }

        public async Task<ClientQueryResult> Get(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_connectionString))
            {
                var result =  await connection.QueryAsync<ClientQueryResult>(ClientQueries.Get, _parameters);
                return result.FirstOrDefault();
            }
        }

        public async Task<List<ClientQueryResult>> List()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<ClientQueryResult>(ClientQueries.List, _parameters);
                return result.ToList();
            }
        }

        public async Task<bool> CheckId(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<bool>(ClientQueries.CheckId, _parameters);
                return result.FirstOrDefault();
            }
        }
    }
}