using ServerASMX.Domain.Core.Commands.Result;
using ServerASMX.Domain.Customers.Commands.Input;
using ServerASMX.Domain.Customers.Interfaces.Handlers;
using ServerASMX.Domain.Customers.Interfaces.Repositories;
using ServerASMX.Domain.Customers.Repositories;

namespace ServerASMX.Domain.Customers.Handlers
{
    public class CustomerHandler : ICustomerHandler
    {
        private readonly ICustomerRepository _repository;

        public CustomerHandler()
        {
            _repository = new CustomerRepository();
        }

        public CommandResult Handle(CustomerAddCommand command)
        {
            if (command == null)
                return new CommandResult("Invalid parameters", "Input parameters", "Input parameters are null");

            var commndValidations = command.IsValid();

            if (commndValidations.Invalid)
                return new CommandResult("Invalid parameters", commndValidations.Notifications);

            var customer = command.MapToCustomer();

            if (customer.Invalid)
                return new CommandResult("Inconsistencies in the data", customer.Notifications);

            var id = _repository.Insert(customer);
            customer.SetId(id);

            var outputData = customer.MapToCustomerCommandOutput();

            return new CommandResult("Company recorded successfully!", outputData);
        }

        public CommandResult Handle(CustomerUpdateCommand command)
        {
            if (command == null)
                return new CommandResult("Invalid parameters", "Input parameters", "Input parameters are null");

            if (!command.IsValid())
                return new CommandResult("Invalid parameters", command.Notifications);

            var customer = command.MapToCustomer();

            if (customer.Invalid)
                return new CommandResult("Inconsistencies in the data", customer.Notifications);

            if (!_repository.CheckId(customer.Id))
                return new CommandResult("Inconsistencies in the data", "Id", "Invalid id. This id is not registered!");

            _repository.Update(customer);

            var outputData = customer.MapToCustomerCommandOutput();

            return new CommandResult("Customer successfully updated!", outputData);
        }

        public CommandResult Handle(CustomerDeleteCommand command)
        {
            if (!_repository.CheckId(command.Id))
                return new CommandResult("Inconsistencies in the data", "Id", "Invalid id. This id is not registered!");

            _repository.Delete(command.Id);

            return new CommandResult("Customer successfully deleted!", $"Customer with Id {command.Id} has been deleted");
        }
    }
}