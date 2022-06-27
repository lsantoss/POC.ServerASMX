using NUnit.Framework;
using POC.ServerASMX.Infra.Commands.Result;
using POC.ServerASMX.Infra.Notifications;
using POC.ServerASMX.Test.Base.Extensions;
using POC.ServerASMX.Test.Tools.Base.Unit;
using System.Collections.Generic;

namespace POC.ServerASMX.Infra.Test.Unit.Commands.Result
{
    internal class CommandResultTest : UnitTest
    {
        [Test]
        [TestCase("Message")]
        public void Construtor1_Success_1(string message)
        {
            var commandResult = new CommandResult(message);

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.True);
                Assert.That(commandResult.Message, Is.EqualTo(message));
                Assert.That(commandResult.Data, Is.Null);
                Assert.That(commandResult.Errors, Is.Empty);
            });
        }

        [Test]
        [TestCase("Message", "Data")]
        public void Construtor1_Success_2(string message, string data)
        {
            var commandResult = new CommandResult(message, data);

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.True);
                Assert.That(commandResult.Message, Is.EqualTo(message));
                Assert.That(commandResult.Data, Is.EqualTo(data));
                Assert.That(commandResult.Errors, Is.Empty);
            });
        }

        [Test]
        [TestCase("Message", "NotificationProperty", "NotificationMessage")]
        public void Construtor2_Success(string message, string notificationProperty, string notificationMessage)
        {
            var errors = new List<Notification> { new Notification(notificationProperty, notificationMessage) };

            var commandResult = new CommandResult(message, errors);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo(message));
                Assert.That(commandResult.Data, Is.Null);
                Assert.That(commandResult.Errors, Has.Count.EqualTo(errors.Count));
                Assert.That(commandResult.Errors, Is.EqualTo(errors));
            });
        }

        [Test]
        [TestCase("Message", "NotificationProperty", "NotificationMessage")]
        public void Construtor3_Success(string message, string notificationProperty, string notificationMessage)
        {
            var commandResult = new CommandResult(message, notificationProperty, notificationMessage);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo(message));
                Assert.That(commandResult.Data, Is.Null);
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Errors[0].Property, Is.EqualTo(notificationProperty));
                Assert.That(commandResult.Errors[0].Message, Is.EqualTo(notificationMessage));
            });
        }
    }
}
