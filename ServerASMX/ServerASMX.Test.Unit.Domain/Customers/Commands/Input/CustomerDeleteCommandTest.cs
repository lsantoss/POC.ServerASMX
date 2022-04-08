using NUnit.Framework;
using ServerASMX.Test.Base.Base;
using ServerASMX.Test.Base.Extensions;

namespace ServerASMX.Test.Unit.Domain.Customers.Commands.Input
{
    internal class CustomerDeleteCommandTest : BaseTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var command = MocksTest.CustomerDeleteCommand;

            var valid = command.IsValid();
            var notifications = command.Notifications.Count;

            TestContext.WriteLine(command.Format());

            Assert.True(valid);
            Assert.AreEqual(0, notifications);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void IsValid_Invalid_Id(long id)
        {
            var command = MocksTest.CustomerDeleteCommand;
            command.Id = id;

            var valid = command.IsValid();
            var notifications = command.Notifications.Count;

            TestContext.WriteLine(command.Format());

            Assert.False(valid);
            Assert.AreNotEqual(0, notifications);
        }
    }
}