using Dapper;
using POC.ServerASMX.Infra.Data.Customers.DTOs;
using POC.ServerASMX.Infra.Data.Customers.Interfaces.Repositories;
using POC.ServerASMX.Infra.Data.Customers.Queries;
using POC.ServerASMX.Infra.Data.DataContexts;
using POC.ServerASMX.Infra.Data.DataContexts.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace POC.ServerASMX.Infra.Data.Customers.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDataContext _dataContext;
        private readonly DynamicParameters _parameters;

        public CustomerRepository()
        {
            _dataContext = new DataContext();
            _parameters = new DynamicParameters();
        }

        public long Insert(CustomerDTO customer)
        {
            _parameters.Add("Name", customer.Name, DbType.String);
            _parameters.Add("Birth", customer.Birth, DbType.DateTime);
            _parameters.Add("Gender", customer.Gender, DbType.Int16);
            _parameters.Add("CashBalance", customer.CashBalance, DbType.Decimal);
            _parameters.Add("Active", customer.Active, DbType.Boolean);
            _parameters.Add("CreationDate", customer.CreationDate, DbType.DateTime);
            _parameters.Add("ChangeDate", customer.ChangeDate, DbType.DateTime);

            return _dataContext.Connection.ExecuteScalar<long>(CustomerQueries.Insert, _parameters);
        }

        public void Update(CustomerDTO customer)
        {
            _parameters.Add("Id", customer.Id, DbType.Int64);
            _parameters.Add("Name", customer.Name, DbType.String);
            _parameters.Add("Birth", customer.Birth, DbType.DateTime);
            _parameters.Add("Gender", customer.Gender, DbType.Int16);
            _parameters.Add("CashBalance", customer.CashBalance, DbType.Decimal);
            _parameters.Add("Active", customer.Active, DbType.Boolean);
            _parameters.Add("CreationDate", customer.CreationDate, DbType.DateTime);
            _parameters.Add("ChangeDate", customer.ChangeDate, DbType.DateTime);

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

        public CustomerDTO Get(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            return _dataContext.Connection.Query<CustomerDTO>(CustomerQueries.Get, _parameters).FirstOrDefault();
        }

        public List<CustomerDTO> List()
        {
            return _dataContext.Connection.Query<CustomerDTO>(CustomerQueries.List, _parameters).ToList();
        }

        public bool CheckId(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            return _dataContext.Connection.Query<bool>(CustomerQueries.CheckId, _parameters).FirstOrDefault();
        }
    }
}