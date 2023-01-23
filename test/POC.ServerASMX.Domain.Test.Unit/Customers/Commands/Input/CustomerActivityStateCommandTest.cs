using NUnit.Framework;
using POC.ServerASMX.Test.Tools.Base.Unit;
using POC.ServerASMX.Test.Tools.Extensions;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Commands.Input
{
    internal class CustomerActivityStateCommandTest : UnitTest
    {
        [Test]
        public void IsValid_Valid()
        {
            //Arrange
            var command = MockData.CustomerActivityStateCommand;

            //Act
            var valid = command.IsValid();

            TestContext.WriteLine(command.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(valid, Is.True);
                Assert.That(command.Valid, Is.True);
                Assert.That(command.Invalid, Is.False);
                Assert.That(command.Notifications, Is.Empty);
            });
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void IsValid_Invalid_Id(long id)
        {
            //Arrange
            var command = MockData.CustomerActivityStateCommand;
            command.Id = id;

            //Act
            var valid = command.IsValid();

            TestContext.WriteLine(command.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(valid, Is.False);
                Assert.That(command.Valid, Is.False);
                Assert.That(command.Invalid, Is.True);
                Assert.That(command.Notifications, Is.Not.Empty);
            });
        }
    }
}