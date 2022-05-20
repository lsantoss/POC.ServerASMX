using POC.ServerASMX.Domain.Core.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace POC.ServerASMX.Domain.Core.Commands.Result
{
    public class CommandResult
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
        public List<Notification> Errors { get; private set; }

        public CommandResult() { }

        public CommandResult(string message, object data = null)
        {
            Success = true;
            Message = message;
            Data = data;
            Errors = new List<Notification>();
        }

        public CommandResult(string message, IEnumerable<Notification> errors)
        {
            Success = false;
            Message = message;
            Data = null;
            Errors = errors.ToList();
        }

        public CommandResult(string message, string propertyNotification, string messageNotification)
        {
            Success = false;
            Message = message;
            Data = null;
            Errors = new List<Notification>() { new Notification(propertyNotification, messageNotification) };
        }
    }
}