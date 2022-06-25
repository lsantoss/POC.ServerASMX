using NUnit.Framework;
using POC.ServerASMX.Test.Base.Extensions;
using POC.ServerASMX.Test.Tools.Base.Common;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Commands.Input
{
    internal class CustomerDeleteCommandTest : BaseTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var command = MocksData.CustomerDeleteCommand;

            TestContext.WriteLine(command.ToJson());

            Assert.True(command.IsValid());
            Assert.AreEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void IsValid_Invalid_Id(long id)
        {
            var command = MocksData.CustomerDeleteCommand;
            command.Id = id;

            TestContext.WriteLine(command.ToJson());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }
    }
}