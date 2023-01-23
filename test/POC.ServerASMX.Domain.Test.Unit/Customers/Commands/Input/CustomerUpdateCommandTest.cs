using NUnit.Framework;
using POC.ServerASMX.Infra.Enums;
using POC.ServerASMX.Test.Tools.Base.Unit;
using POC.ServerASMX.Test.Tools.Constants;
using POC.ServerASMX.Test.Tools.Extensions;
using System;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Commands.Input
{
    internal class CustomerUpdateCommandTest : UnitTest
    {
        [Test]
        public void IsValid_Valid()
        {
            //Arrange
            var command = MockData.CustomerUpdateCommand;

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
            var command = MockData.CustomerUpdateCommand;
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

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void IsValid_Invalid_Name(string name)
        {
            //Arrange
            var command = MockData.CustomerUpdateCommand;
            command.Name = name;

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

        [Test]
        public void IsValid_Invalid_Birth_DateTimeMin()
        {
            //Arrange
            var command = MockData.CustomerUpdateCommand;
            command.Birth = DateTime.MinValue;

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

        [Test]
        public void IsValid_Invalid_Birth_FutureDate()
        {
            //Arrange
            var command = MockData.CustomerUpdateCommand;
            command.Birth = DateTime.Now.AddDays(1);

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

        [Test]
        [TestCase(-1)]
        public void IsValid_Invalid_Gender(EGender gender)
        {
            //Arrange
            var command = MockData.CustomerUpdateCommand;
            command.Gender = gender;

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

        [Test]
        [TestCase(-1)]
        public void IsValid_Invalid_CashBalance(decimal cashBalance)
        {
            //Arrange
            var command = MockData.CustomerUpdateCommand;
            command.CashBalance = cashBalance;

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