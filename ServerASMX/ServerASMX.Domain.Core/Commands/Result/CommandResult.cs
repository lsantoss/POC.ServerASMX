using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace ServerASMX.Domain.Core.Commands.Result
{
    public class CommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public IReadOnlyCollection<Notification> Errors { get; set; }

        public CommandResult() { }

        public CommandResult(string message, object data)
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