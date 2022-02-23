using ServerASMX.Domain.Core.Commands.Interfaces;
using ServerASMX.Domain.Core.Notifications;

namespace ServerASMX.Domain.Customers.Commands.Input
{
    public class CustomerActivityStateCommand : IStandardCommand
    {
        public long Id { get; set; }
        public bool Active { get; set; }

        public Notifier IsValid()
        {
            var notifier = new Notifier();

            if (Id <= 0)
                notifier.AddNotification("Id", "Id must be greater than zero");

            return notifier;
        }
    }
}