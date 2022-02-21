using ServerASMX.Domain.Customers.Entities;
using ServerASMX.Domain.Customers.Queries.Results;
using System.Collections.Generic;

namespace ServerASMX.Domain.Customers.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        long Insert(Customer client);
        void Update(Customer client);
        void Delete(long id);

        CustomerQueryResult Get(long id);
        List<CustomerQueryResult> List();

        bool CheckId(long id);
    }
}