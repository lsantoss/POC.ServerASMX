using ServerASMX.Domain.Customers.Handlers;
using ServerASMX.Domain.Customers.Interfaces.Handlers;
using ServerASMX.Domain.Customers.Interfaces.Repositories;
using ServerASMX.Domain.Customers.Repositories;
using System.ComponentModel;
using System.Web.Services;

namespace ServerASMX
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class CustomerService : WebService
    {
        private readonly ICustomerHandler _handler;
        private readonly ICustomerRepository _repository;

        public CustomerService()
        {
            _handler = new CustomerHandler();
            _repository = new CustomerRepository();
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Olá, Mundo";
        }
    }
}
