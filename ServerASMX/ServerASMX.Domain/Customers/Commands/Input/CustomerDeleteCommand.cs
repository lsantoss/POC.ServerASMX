using ServerASMX.Domain.Core.Commands.Interfaces;
using ServerASMX.Domain.Core.Notifications;
using ServerASMX.Domain.Customers.Validations;

namespace ServerASMX.Domain.Customers.Commands.Input
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