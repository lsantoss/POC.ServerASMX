using System.Collections.Generic;
using System.Linq;

namespace ServerASMX.Domain.Core.Notifications
{
    public class Notifier
    {
        public bool Valid { get; private set; }
        public bool Invalid { get; private set; }

        public List<Notification> Notifications => _notifications.ToList();
        private readonly IList<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
            Invalid = false;
            Valid = true;
        }

        public void AddNotification(string property, string message)
        {
            Inactivate();
            _notifications.Add(new Notification(property, message));
        }

        private void Inactivate()
        {
            Valid = false;
            Invalid = true;
        }
    }
}