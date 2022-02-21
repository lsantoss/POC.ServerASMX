using Dapper;
using ServerASMX.Domain.Customers.Entities;
using ServerASMX.Domain.Customers.Interfaces.Repositories;
using ServerASMX.Domain.Customers.Queries.Results;
using ServerASMX.Domain.Customers.Repositories.Queries;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ServerASMX.Domain.Customers.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;
        private readonly DynamicParameters _parameters;

        public CustomerRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            _parameters = new DynamicParameters();
        }

        public async Task<long> Insert(Customer client)
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
                return await connection.ExecuteScalarAsync<long>(CustomerQueries.Insert, _parameters);
            }
        }

        public async Task Update(Customer client)
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
                _ = await connection.ExecuteAsync(CustomerQueries.Update, _parameters);
            }
        }

        public async Task Delete(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_connectionString))
            {
                _ = await connection.ExecuteAsync(CustomerQueries.Delete, _parameters);
            }
        }

        public async Task<CustomerQueryResult> Get(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<CustomerQueryResult>(CustomerQueries.Get, _parameters);
                return result.FirstOrDefault();
            }
        }

        public async Task<List<CustomerQueryResult>> List()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<CustomerQueryResult>(CustomerQueries.List, _parameters);
                return result.ToList();
            }
        }

        public async Task<bool> CheckId(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<bool>(CustomerQueries.CheckId, _parameters);
                return result.FirstOrDefault();
            }
        }
    }
}