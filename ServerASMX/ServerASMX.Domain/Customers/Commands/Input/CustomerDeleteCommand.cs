using ServerASMX.Domain.Core.Commands.Interfaces;
using ServerASMX.Domain.Core.Notifications;

namespace ServerASMX.Domain.Customers.Commands.Input
{
    public class CustomerDeleteCommand : Notifier, IStandardCommand
    {
        public long Id { get; set; }

        public bool IsValid()
        {
            if (Id <= 0)
                AddNotification("Id", "Id must be greater than zero");

            return Valid;
        }
    }
}