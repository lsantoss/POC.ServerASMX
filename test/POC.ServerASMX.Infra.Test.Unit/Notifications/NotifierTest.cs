using NUnit.Framework;
using POC.ServerASMX.Infra.Notifications;
using POC.ServerASMX.Test.Base.Base;
using POC.ServerASMX.Test.Base.Extensions;

namespace POC.ServerASMX.Infra.Test.Unit.Notifications
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
