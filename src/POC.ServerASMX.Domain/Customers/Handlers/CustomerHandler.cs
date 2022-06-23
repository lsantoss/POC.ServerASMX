using POC.ServerASMX.Infra.Commands.Result;
using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Domain.Customers.Entities;
using POC.ServerASMX.Domain.Customers.Interfaces.Handlers;
using POC.ServerASMX.Domain.Customers.Interfaces.Repositories;
using POC.ServerASMX.Domain.Customers.Repositories;
using System;
using System.Threading.Tasks;

namespace POC.ServerASMX.Domain.Customers.Handlers
{
    public class CustomerHandler : ICustomerHandler
    {
        private readonly ICustomerRepository _repository;

        public CustomerHandler()
        {
            _repository = new CustomerRepository();
        }

        public async Task<CommandResult> HandleAsync(CustomerAddCommand command)
        {
            if (command == null)
                return new CommandResult("Invalid parameters", "Input parameters", "Input parameters are null");

            if (!command.IsValid())
                return new CommandResult("Invalid parameters", command.Notifications);

            var customer = command.MapToCustomer();

            if (customer.Invalid)
                return new CommandResult("Inconsistencies in the data", customer.Notifications);

            var id = await _repository.InsertAsync(customer);
            customer.SetId(id);

            var resultData = customer.MapToCustomerCommandResult();

            return new CommandResult("Customer successfully inserted!", resultData);
        }

        public async Task<CommandResult> HandleAsync(CustomerUpdateCommand command)
        {
            if (command == null)
                return new CommandResult("Invalid parameters", "Input parameters", "Input parameters are null");

            if (!command.IsValid())
                return new CommandResult("Invalid parameters", command.Notifications);

            var customerQR = await _repository.GetAsync(command.Id);

            if (customerQR == null)
                return new CommandResult("Inconsistencies in the data", "Id", "Invalid id. This id is not registered!");

            var customer = new Customer(command.Id, command.Name, command.Birth, command.Gender, command.CashBalance, customerQR.Active, customerQR.CreationDate, DateTime.Now);

            if (customer.Invalid)
                return new CommandResult("Inconsistencies in the data", customer.Notifications);

            await _repository.UpdateAsync(customer);

            var resultData = customer.MapToCustomerCommandResult();

            return new CommandResult("Customer successfully updated!", resultData);
        }

        public async Task<CommandResult> HandleAsync(CustomerActivityStateCommand command)
        {
            if (command == null)
                return new CommandResult("Invalid parameters", "Input parameters", "Input parameters are null");

            if (!command.IsValid())
                return new CommandResult("Invalid parameters", command.Notifications);

            if (!await _repository.CheckIdAsync(command.Id))
                return new CommandResult("Inconsistencies in the data", "Id", "Invalid id. This id is not registered!");

            await _repository.ChangeActivityStateAsync(command.Id, command.Active);

            return new CommandResult("Customer successfully updated!");
        }

        public async Task<CommandResult> HandleAsync(CustomerDeleteCommand command)
        {
            if (command == null)
                return new CommandResult("Invalid parameters", "Input parameters", "Input parameters are null");

            if (!command.IsValid())
                return new CommandResult("Invalid parameters", command.Notifications);

            if (!await _repository.CheckIdAsync(command.Id))
                return new CommandResult("Inconsistencies in the data", "Id", "Invalid id. This id is not registered!");

            await _repository.DeleteAsync(command.Id);

            return new CommandResult("Customer successfully deleted!");
        }
    }
}