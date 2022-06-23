using POC.ServerASMX.Domain.Customers.Entities;
using POC.ServerASMX.Domain.Customers.Queries.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POC.ServerASMX.Domain.Customers.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<long> InsertAsync(Customer client);
        Task UpdateAsync(Customer client);
        Task DeleteAsync(long id);
        Task ChangeActivityStateAsync(long id, bool active);

        Task<CustomerQueryResult> GetAsync(long id);
        Task<List<CustomerQueryResult>> ListAsync();

        Task<bool> CheckIdAsync(long id);
    }
}