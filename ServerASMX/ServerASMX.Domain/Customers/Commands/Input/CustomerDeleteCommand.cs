using Flunt.Notifications;
using ServerASMX.Domain.Core.Commands.Interfaces;

namespace ServerASMX.Domain.Customers.Commands.Input
{
    public class CustomerDeleteCommand : Notifiable, IStandardCommand
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