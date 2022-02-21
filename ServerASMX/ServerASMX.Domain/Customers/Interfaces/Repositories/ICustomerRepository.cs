using ServerASMX.Domain.Customers.Entities;
using ServerASMX.Domain.Customers.Queries.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerASMX.Domain.Customers.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<long> Insert(Customer client);
        Task Update(Customer client);
        Task Delete(long id);

        Task<CustomerQueryResult> Get(long id);
        Task<List<CustomerQueryResult>> List();

        Task<bool> CheckId(long id);
    }
}