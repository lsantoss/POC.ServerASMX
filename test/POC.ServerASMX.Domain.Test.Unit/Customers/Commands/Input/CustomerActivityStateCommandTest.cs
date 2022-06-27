using NUnit.Framework;
using POC.ServerASMX.Test.Base.Extensions;
using POC.ServerASMX.Test.Tools.Base.Unit;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Commands.Input
{
    internal class CustomerActivityStateCommandTest : UnitTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var command = MocksData.CustomerDeleteCommand;

            TestContext.WriteLine(command.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(command.IsValid(), Is.True);
                Assert.That(command.Notifications, Is.Empty);
            });
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void IsValid_Invalid_Id(long id)
        {
            var command = MocksData.CustomerDeleteCommand;
            command.Id = id;

            TestContext.WriteLine(command.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(command.IsValid(), Is.False);
                Assert.That(command.Notifications, Is.Not.Empty);
            });
        }
    }
}