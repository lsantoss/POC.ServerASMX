using NUnit.Framework;
using POC.ServerASMX.Test.Base.Base;
using POC.ServerASMX.Test.Base.Extensions;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Commands.Input
{
    internal class CustomerActivityStateCommandTest : BaseUnitTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var command = MocksUnitTest.CustomerDeleteCommand;

            TestContext.WriteLine(command.Format());

            Assert.True(command.IsValid());
            Assert.AreEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void IsValid_Invalid_Id(long id)
        {
            var command = MocksUnitTest.CustomerDeleteCommand;
            command.Id = id;

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }
    }
}