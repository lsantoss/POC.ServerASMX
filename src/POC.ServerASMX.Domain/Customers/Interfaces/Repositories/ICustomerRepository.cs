using POC.ServerASMX.Domain.Customers.Entities;
using POC.ServerASMX.Domain.Customers.Queries.Results;
using System.Collections.Generic;

namespace POC.ServerASMX.Domain.Customers.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        long Insert(Customer client);
        void Update(Customer client);
        void Delete(long id);
        void ChangeActivityState(long id, bool active);

        CustomerQueryResult Get(long id);
        List<CustomerQueryResult> List();

        bool CheckId(long id);
    }
}