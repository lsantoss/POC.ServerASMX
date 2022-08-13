using POC.ServerASMX.Infra.Data.Customers.DTOs;
using System.Collections.Generic;

namespace POC.ServerASMX.Infra.Data.Customers.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        long Insert(CustomerDTO customer);
        void Update(CustomerDTO customer);
        void Delete(long id);
        void ChangeActivityState(long id, bool active);

        CustomerDTO Get(long id);
        List<CustomerDTO> List();

        bool CheckId(long id);
    }
}