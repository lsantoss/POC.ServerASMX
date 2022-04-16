using NUnit.Framework;
using ServerASMX.Domain.Core.Notifications;
using ServerASMX.Test.Base.Base;
using ServerASMX.Test.Base.Extensions;

namespace ServerASMX.Test.Unit.Domain.Core.Notifications
{
    internal class NotificationTest : BaseTest
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
