using NUnit.Framework;
using ServerASMX.Domain.Core.Notifications;
using ServerASMX.Test.Base.Base;
using ServerASMX.Test.Base.Extensions;

namespace ServerASMX.Test.Unit.Domain.Core.Notifications
{
    internal class NotifierTest : BaseUnitTest
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
        [TestCase("NotificationProperty", "NotificationMessage")]
        public void AddNotification_Success(string property, string message)
        {
            var notifier = new Notifier();
            notifier.AddNotification(property, message);

            TestContext.WriteLine(notifier.Format());

            Assert.IsFalse(notifier.Valid);
            Assert.IsTrue(notifier.Invalid);
            Assert.AreNotEqual(0, notifier.Notifications.Count);
        }
    }
}
