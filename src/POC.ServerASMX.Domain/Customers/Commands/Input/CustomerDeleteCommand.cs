using POC.ServerASMX.Domain.Core.Commands.Interfaces;
using POC.ServerASMX.Domain.Core.Notifications;
using POC.ServerASMX.Domain.Customers.Validations;

namespace POC.ServerASMX.Domain.Customers.Commands.Input
{
    public class CustomerDeleteCommand : Notifier, IStandardCommand
    {
        public long Id { get; set; }

        public bool IsValid()
        {
            AddNotification(new CustomerValidation().ValidateCommand(this));
            return Valid;
        }
    }
}