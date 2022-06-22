using NUnit.Framework;
using POC.ServerASMX.Infra.Commands.Result;
using POC.ServerASMX.Infra.Notifications;
using POC.ServerASMX.Test.Base.Base;
using POC.ServerASMX.Test.Base.Extensions;
using System.Collections.Generic;

namespace POC.ServerASMX.Infra.Test.Unit.Commands.Result
{
    internal class CommandResultTest : BaseUnitTest
    {
        [Test]
        [TestCase("Message")]
        public void Construtor1_Success_1(string message)
        {
            var commandResult = new CommandResult(message);

            TestContext.WriteLine(commandResult.Format());

            Assert.IsTrue(commandResult.Success);
            Assert.AreEqual(message, commandResult.Message);
            Assert.IsNull(commandResult.Data);
            Assert.AreEqual(0, commandResult.Errors.Count);
        }

        [Test]
        [TestCase("Message", "Data")]
        public void Construtor1_Success_2(string message, string data)
        {
            var commandResult = new CommandResult(message, data);

            TestContext.WriteLine(commandResult.Format());

            Assert.IsTrue(commandResult.Success);
            Assert.AreEqual(message, commandResult.Message);
            Assert.AreEqual(data, commandResult.Data);
            Assert.AreEqual(0, commandResult.Errors.Count);
        }

        [Test]
        [TestCase("Message", "NotificationProperty", "NotificationMessage")]
        public void Construtor2_Success(string message, string notificationProperty, string notificationMessage)
        {
            var errors = new List<Notification> { new Notification(notificationProperty, notificationMessage) };

            var commandResult = new CommandResult(message, errors);

            TestContext.WriteLine(commandResult.Format());

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual(message, commandResult.Message);
            Assert.IsNull(commandResult.Data);
            Assert.AreEqual(errors.Count, commandResult.Errors.Count);
            Assert.AreEqual(errors, commandResult.Errors);
        }

        [Test]
        [TestCase("Message", "NotificationProperty", "NotificationMessage")]
        public void Construtor3_Success(string message, string notificationProperty, string notificationMessage)
        {
            var commandResult = new CommandResult(message, notificationProperty, notificationMessage);

            TestContext.WriteLine(commandResult.Format());

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual(message, commandResult.Message);
            Assert.IsNull(commandResult.Data);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.AreEqual(notificationProperty, commandResult.Errors[0].Property);
            Assert.AreEqual(notificationMessage, commandResult.Errors[0].Message);
        }
    }
}
