using NUnit.Framework;
using ServerASMX.Domain.Core.Notifications;
using ServerASMX.Test.Base.Base;
using ServerASMX.Test.Base.Extensions;

namespace ServerASMX.Test.Unit.Domain.Core.Notifications
{
    internal class NotifierTest : BaseTest
    {
        [Test]
        public void Construtor_Success()
        {
            var notifier = new Notifier();

            TestContext.WriteLine(notifier.Format());

            Assert.IsTrue(notifier.Valid);
            Assert.IsFalse(notifier.Invalid);
            Assert.AreEqual(0, notifier.Notifications.Count);
        }

        [Test]
        public void AddNotification_Success()
        {
            var property = "NotificationProperty";
            var message = "NotificationMessage";

            var notifier = new Notifier();
            notifier.AddNotification(property, message);

            TestContext.WriteLine(notifier.Format());

            Assert.IsFalse(notifier.Valid);
            Assert.IsTrue(notifier.Invalid);
            Assert.AreNotEqual(0, notifier.Notifications.Count);
        }
    }
}
