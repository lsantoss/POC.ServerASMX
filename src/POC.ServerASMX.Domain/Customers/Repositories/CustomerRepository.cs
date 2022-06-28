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

        public long Insert(Customer client)
        {
            _parameters.Add("Name", client.Name, DbType.String);
            _parameters.Add("Birth", client.Birth, DbType.DateTime);
            _parameters.Add("Gender", client.Gender, DbType.Int16);
            _parameters.Add("CashBalance", client.CashBalance, DbType.Decimal);
            _parameters.Add("Active", client.Active, DbType.Boolean);
            _parameters.Add("CreationDate", client.CreationDate, DbType.DateTime);
            _parameters.Add("ChangeDate", client.ChangeDate, DbType.DateTime);

            return _dataContext.Connection.ExecuteScalar<long>(CustomerQueries.Insert, _parameters);
        }

        public void Update(Customer client)
        {
            _parameters.Add("Id", client.Id, DbType.Int64);
            _parameters.Add("Name", client.Name, DbType.String);
            _parameters.Add("Birth", client.Birth, DbType.DateTime);
            _parameters.Add("Gender", client.Gender, DbType.Int16);
            _parameters.Add("CashBalance", client.CashBalance, DbType.Decimal);
            _parameters.Add("Active", client.Active, DbType.Boolean);
            _parameters.Add("CreationDate", client.CreationDate, DbType.DateTime);
            _parameters.Add("ChangeDate", client.ChangeDate, DbType.DateTime);

            _dataContext.Connection.Execute(CustomerQueries.Update, _parameters);
        }

        public void Delete(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            _dataContext.Connection.Execute(CustomerQueries.Delete, _parameters);
        }

        public void ChangeActivityState(long id, bool active)
        {
            _parameters.Add("Id", id, DbType.Int64);
            _parameters.Add("Active", active, DbType.Boolean);
            _parameters.Add("ChangeDate", DateTime.Now, DbType.DateTime);

            _dataContext.Connection.Execute(CustomerQueries.ChangeActivityState, _parameters);
        }

        public CustomerQueryResult Get(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            return _dataContext.Connection.Query<CustomerQueryResult>(CustomerQueries.Get, _parameters).FirstOrDefault();
        }

        public List<CustomerQueryResult> List()
        {
            return _dataContext.Connection.Query<CustomerQueryResult>(CustomerQueries.List, _parameters).ToList();
        }

        public bool CheckId(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            return _dataContext.Connection.Query<bool>(CustomerQueries.CheckId, _parameters).FirstOrDefault();
        }
    }
}