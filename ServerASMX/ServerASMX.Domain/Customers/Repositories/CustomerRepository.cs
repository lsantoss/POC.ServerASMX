using Dapper;
using ServerASMX.Domain.Customers.Entities;
using ServerASMX.Domain.Customers.Interfaces.Repositories;
using ServerASMX.Domain.Customers.Queries.Results;
using ServerASMX.Domain.Customers.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ServerASMX.Domain.Customers.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;
        private readonly DynamicParameters _parameters;

        public CustomerRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString.ToString();
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

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<long>(CustomerQueries.Insert, _parameters);
            }
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

            using (var connection = new SqlConnection(_connectionString))
            {
                _ = connection.Execute(CustomerQueries.Update, _parameters);
            }
        }

        public void Delete(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_connectionString))
            {
                _ = connection.Execute(CustomerQueries.Delete, _parameters);
            }
        }

        public void ChangeActivityState(long id, bool active)
        {
            _parameters.Add("Id", id, DbType.Int64);
            _parameters.Add("Active", active, DbType.Boolean);
            _parameters.Add("ChangeDate", DateTime.Now, DbType.DateTime);

            using (var connection = new SqlConnection(_connectionString))
            {
                _ = connection.Execute(CustomerQueries.ChangeActivityState, _parameters);
            }
        }

        public CustomerQueryResult Get(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<CustomerQueryResult>(CustomerQueries.Get, _parameters).FirstOrDefault();
            }
        }

        public List<CustomerQueryResult> List()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<CustomerQueryResult>(CustomerQueries.List, _parameters).ToList();
            }
        }

        public bool CheckId(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<bool>(CustomerQueries.CheckId, _parameters).FirstOrDefault();
            }
        }
    }
}