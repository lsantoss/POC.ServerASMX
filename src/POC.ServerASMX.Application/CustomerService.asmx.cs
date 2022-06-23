using POC.ServerASMX.Infra.Commands.Result;
using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Domain.Customers.Handlers;
using POC.ServerASMX.Domain.Customers.Interfaces.Handlers;
using POC.ServerASMX.Domain.Customers.Interfaces.Repositories;
using POC.ServerASMX.Domain.Customers.Queries.Results;
using POC.ServerASMX.Domain.Customers.Repositories;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using System.Xml.Serialization;
using POC.ServerASMX.Domain.Customers.Commands.Result;
using System.Threading.Tasks;

namespace POC.ServerASMX.Application
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
        [XmlInclude(typeof(CustomerCommandResult))]
        public async Task<CommandResult> Add(CustomerAddCommand command) => await _handler.HandleAsync(command);

        [WebMethod]
        [XmlInclude(typeof(CustomerCommandResult))]
        public async Task<CommandResult> Update(CustomerUpdateCommand command) => await _handler.HandleAsync(command);

        [WebMethod]
        [XmlInclude(typeof(CustomerCommandResult))]
        public async Task<CommandResult> ChangeActivityState(CustomerActivityStateCommand command) => await _handler.HandleAsync(command);

        [WebMethod]
        public async Task<CommandResult> Delete(CustomerDeleteCommand command) => await _handler.HandleAsync(command);

        [WebMethod]
        public async Task<CustomerQueryResult> Get(long id) => await _repository.GetAsync(id);

        [WebMethod]
        public async Task<List<CustomerQueryResult>> List() => await _repository.ListAsync();
    }
}