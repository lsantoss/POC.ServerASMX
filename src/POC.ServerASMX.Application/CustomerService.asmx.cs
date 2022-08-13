using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Domain.Customers.Commands.Result;
using POC.ServerASMX.Domain.Customers.Handlers;
using POC.ServerASMX.Domain.Customers.Interfaces.Handlers;
using POC.ServerASMX.Infra.Commands.Result;
using POC.ServerASMX.Infra.Data.Customers.DTOs;
using POC.ServerASMX.Infra.Data.Customers.Interfaces.Repositories;
using POC.ServerASMX.Infra.Data.Customers.Repositories;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using System.Xml.Serialization;

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
        public CommandResult Add(CustomerAddCommand command) => _handler.Handle(command);

        [WebMethod]
        [XmlInclude(typeof(CustomerCommandResult))]
        public CommandResult Update(CustomerUpdateCommand command) => _handler.Handle(command);

        [WebMethod]
        [XmlInclude(typeof(CustomerCommandResult))]
        public CommandResult ChangeActivityState(CustomerActivityStateCommand command) => _handler.Handle(command);

        [WebMethod]
        public CommandResult Delete(CustomerDeleteCommand command) => _handler.Handle(command);

        [WebMethod]
        public CustomerDTO Get(long id) => _repository.Get(id);

        [WebMethod]
        public List<CustomerDTO> List() => _repository.List();
    }
}