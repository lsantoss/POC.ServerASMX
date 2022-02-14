using Flunt.Notifications;
using ServerASMX.Domain.Core.Commands.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ServerASMX.Domain.Core.Commands.Result
{
    public class CommandResult : ICommandResult
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public IReadOnlyCollection<Notification> Errors { get; set; }

        public CommandResult(string message, object data, int statusCode = 200)
        {
            StatusCode = statusCode;
            Success = true;
            Message = message;
            Data = data;
            Errors = new List<Notification>();
        }

        public CommandResult(string message, IEnumerable<Notification> errors)
        {
            StatusCode = 422;
            Success = false;
            Message = message;
            Data = null;
            Errors = errors.ToList();
        }

        public CommandResult(string message, string propertyNotification, string messageNotification)
        {
            StatusCode = 422;
            Success = false;
            Message = message;
            Data = null;
            Errors = new List<Notification>() { new Notification(propertyNotification, messageNotification) };
        }
    }
}