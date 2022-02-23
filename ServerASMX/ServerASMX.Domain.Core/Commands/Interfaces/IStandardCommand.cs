using ServerASMX.Domain.Core.Notifications;

namespace ServerASMX.Domain.Core.Commands.Interfaces
{
    public interface IStandardCommand
    {
        Notifier IsValid();
    }
}