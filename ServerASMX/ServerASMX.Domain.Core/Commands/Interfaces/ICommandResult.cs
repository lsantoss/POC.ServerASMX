using Flunt.Notifications;
using System.Collections.Generic;

namespace ServerASMX.Domain.Core.Commands.Interfaces
{
    public interface ICommandResult
    {
        int StatusCode { get; set; }
        bool Success { get; set; }
        string Message { get; set; }
        object Data { get; set; }
        IReadOnlyCollection<Notification> Errors { get; set; }
    }
}