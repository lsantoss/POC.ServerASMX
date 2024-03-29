﻿using NUnit.Framework;
using POC.ServerASMX.Infra.Notifications;
using POC.ServerASMX.Test.Tools.Base.Unit;
using POC.ServerASMX.Test.Tools.Extensions;

namespace POC.ServerASMX.Infra.Test.Unit.Notifications
{
    internal class NotifierTest : UnitTest
    {
        [Test]
        public void Construtor_Success()
        {
            //Act
            var notifier = new Notifier();

            TestContext.WriteLine(notifier.ToJson());

            //Assert
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
            //Arrange
            var notifier = new Notifier();

            //Act
            notifier.AddNotification(property, message);

            TestContext.WriteLine(notifier.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(notifier.Valid, Is.False);
                Assert.That(notifier.Invalid, Is.True);
                Assert.That(notifier.Notifications, Is.Not.Empty);
            });
        }
    }
}