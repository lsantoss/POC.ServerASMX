﻿using NUnit.Framework;
using POC.ServerASMX.Infra.Notifications;
using POC.ServerASMX.Test.Tools.Base.Unit;
using POC.ServerASMX.Test.Tools.Extensions;

namespace POC.ServerASMX.Infra.Test.Unit.Notifications
{
    internal class NotificationTest : UnitTest
    {
        [Test]
        [TestCase("Property", "Message")]
        public void Construtor_Success(string property, string message)
        {
            //Act
            var notification = new Notification(property, message);

            TestContext.WriteLine(notification.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(notification.Property, Is.EqualTo(property));
                Assert.That(notification.Message, Is.EqualTo(message));
            });
        }
    }
}