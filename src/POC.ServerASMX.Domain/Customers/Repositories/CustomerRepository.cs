using Dapper;
using POC.ServerASMX.Domain.Customers.Entities;
using POC.ServerASMX.Domain.Customers.Interfaces.Repositories;
using POC.ServerASMX.Domain.Customers.Queries.Results;
using POC.ServerASMX.Infra.Data.DataContexts;
using POC.ServerASMX.Infra.Data.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace POC.ServerASMX.Domain.Customers.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _dataContext;
        private readonly DynamicParameters _parameters;

        public CustomerRepository()
        {
            _dataContext = new DataContext();
            _parameters = new DynamicParameters();
        }

        public async Task<long> InsertAsync(Customer client)
        {
            _parameters.Add("Name", client.Name, DbType.String);
            _parameters.Add("Birth", client.Birth, DbType.DateTime);
            _parameters.Add("Gender", client.Gender, DbType.Int16);
            _parameters.Add("CashBalance", client.CashBalance, DbType.Decimal);
            _parameters.Add("Active", client.Active, DbType.Boolean);
            _parameters.Add("CreationDate", client.CreationDate, DbType.DateTime);
            _parameters.Add("ChangeDate", client.ChangeDate, DbType.DateTime);

            return await _dataContext.Connection.ExecuteScalarAsync<long>(CustomerQueries.Insert, _parameters);
        }

        public async Task UpdateAsync(Customer client)
        {
            _parameters.Add("Id", client.Id, DbType.Int64);
            _parameters.Add("Name", client.Name, DbType.String);
            _parameters.Add("Birth", client.Birth, DbType.DateTime);
            _parameters.Add("Gender", client.Gender, DbType.Int16);
            _parameters.Add("CashBalance", client.CashBalance, DbType.Decimal);
            _parameters.Add("Active", client.Active, DbType.Boolean);
            _parameters.Add("CreationDate", client.CreationDate, DbType.DateTime);
            _parameters.Add("ChangeDate", client.ChangeDate, DbType.DateTime);

            await _dataContext.Connection.ExecuteAsync(CustomerQueries.Update, _parameters);
        }

        public async Task DeleteAsync(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            await _dataContext.Connection.ExecuteAsync(CustomerQueries.Delete, _parameters);
        }

        public async Task ChangeActivityStateAsync(long id, bool active)
        {
            _parameters.Add("Id", id, DbType.Int64);
            _parameters.Add("Active", active, DbType.Boolean);
            _parameters.Add("ChangeDate", DateTime.Now, DbType.DateTime);

            await _dataContext.Connection.ExecuteAsync(CustomerQueries.ChangeActivityState, _parameters);
        }

        public async Task<CustomerQueryResult> GetAsync(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            return (await _dataContext.Connection.QueryAsync<CustomerQueryResult>(CustomerQueries.Get, _parameters)).FirstOrDefault();
        }

        public async Task<List<CustomerQueryResult>> ListAsync()
        {
            return (await _dataContext.Connection.QueryAsync<CustomerQueryResult>(CustomerQueries.List, _parameters)).ToList();
        }

        public async Task<bool> CheckIdAsync(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            return (await _dataContext.Connection.QueryAsync<bool>(CustomerQueries.CheckId, _parameters)).FirstOrDefault();
        }
    }
}