﻿using NUnit.Framework;
using POC.ServerASMX.Infra.Notifications;
using POC.ServerASMX.Test.Base.Extensions;
using POC.ServerASMX.Test.Tools.Base.Unit;

namespace POC.ServerASMX.Infra.Test.Unit.Notifications
{
    internal class NotificationTest : UnitTest
    {
        [Test]
        [TestCase("Property", "Message")]
        public void Construtor_Success(string property, string message)
        {
            var notification = new Notification(property, message);

            TestContext.WriteLine(notification.ToJson());

            Assert.AreEqual(property, notification.Property);
            Assert.AreEqual(message, notification.Message);
        }
    }
}