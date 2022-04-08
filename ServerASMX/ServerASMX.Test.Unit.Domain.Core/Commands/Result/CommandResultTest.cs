using NUnit.Framework;
using ServerASMX.Domain.Core.Commands.Result;
using ServerASMX.Domain.Core.Notifications;
using ServerASMX.Test.Base.Base;
using ServerASMX.Test.Base.Extensions;
using System.Collections.Generic;

namespace ServerASMX.Test.Unit.Domain.Core.Commands.Result
{
    internal class CommandResultTest : BaseTest
    {
        [Test]
        public void Construtor1_Success_1()
        {
            var message = "Message";

            var commandResult = new CommandResult(message);

            TestContext.WriteLine(commandResult.Format());

            Assert.IsTrue(commandResult.Success);
            Assert.AreEqual(message, commandResult.Message);
            Assert.IsNull(commandResult.Data);
            Assert.AreEqual(0, commandResult.Errors.Count);
        }

        [Test]
        public void Construtor1_Success_2()
        {
            var message = "Message";
            var data = "Data";

            var commandResult = new CommandResult(message, data);

            TestContext.WriteLine(commandResult.Format());

            Assert.IsTrue(commandResult.Success);
            Assert.AreEqual(message, commandResult.Message);
            Assert.AreEqual(data, commandResult.Data);
            Assert.AreEqual(0, commandResult.Errors.Count);
        }

        [Test]
        public void Construtor2_Success()
        {
            var message = "Message";
            var errors = new List<Notification>
            {
                new Notification("NotificationProperty", "NotificationMessage")
            };

            var commandResult = new CommandResult(message, errors);

            TestContext.WriteLine(commandResult.Format());

            Assert.IsFalse(commandResult.Success);
            Assert.AreEqual(message, commandResult.Message);
            Assert.IsNull(commandResult.Data);
            Assert.AreEqual(errors.Count, commandResult.Errors.Count);
            Assert.AreEqual(errors, commandResult.Errors);
        }

        [Test]
        public void Construtor3_Success()
        {
            var message = "Message";
            var notificationProperty = "NotificationProperty";
            var notificationMessage = "NotificationMessage";

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
