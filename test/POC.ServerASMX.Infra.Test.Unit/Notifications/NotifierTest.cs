using NUnit.Framework;
using POC.ServerASMX.Infra.Notifications;
using POC.ServerASMX.Test.Base.Extensions;
using POC.ServerASMX.Test.Tools.Base.Unit;

namespace POC.ServerASMX.Infra.Test.Unit.Notifications
{
    internal class NotifierTest : UnitTest
    {
        [Test]
        public void Construtor_Success()
        {
            var notifier = new Notifier();

            TestContext.WriteLine(notifier.ToJson());
            Assert.Multiple(() =>
            {
                Assert.That(notifier.Valid, Is.True);
                Assert.That(notifier.Invalid, Is.False);
                Assert.That(notifier.Notifications, Is.Empty);
            });
        }

        [Test]
        [TestCase("NotificationProperty", "NotificationMessage")]
        public void AddNotification_Success(string property, string message)
        {
            var notifier = new Notifier();
            notifier.AddNotification(property, message);

            TestContext.WriteLine(notifier.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(notifier.Valid, Is.False);
                Assert.That(notifier.Invalid, Is.True);
                Assert.That(notifier.Notifications, Is.Not.Empty);
            });
        }
    }
}
