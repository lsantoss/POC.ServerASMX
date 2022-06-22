using NUnit.Framework;
using POC.ServerASMX.Infra.Notifications;
using POC.ServerASMX.Test.Base.Base;
using POC.ServerASMX.Test.Base.Extensions;

namespace POC.ServerASMX.Infra.Test.Unit.Notifications
{
    internal class NotificationTest : BaseUnitTest
    {
        [Test]
        [TestCase("Property", "Message")]
        public void Construtor_Success(string property, string message)
        {
            var notification = new Notification(property, message);

            TestContext.WriteLine(notification.Format());

            Assert.AreEqual(property, notification.Property);
            Assert.AreEqual(message, notification.Message);
        }
    }
}