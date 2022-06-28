using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Enums;
using POC.ServerASMX.Test.Tools.Base.Unit;
using POC.ServerASMX.Test.Tools.Constants;
using POC.ServerASMX.Test.Tools.Extensions;
using System;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Commands.Input
{
    internal class CustomerAddCommandTest : UnitTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var command = MocksData.CustomerAddCommand;
            var valid = command.IsValid();

            TestContext.WriteLine(command.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(valid, Is.True);
                Assert.That(command.Valid, Is.True);
                Assert.That(command.Invalid, Is.False);
                Assert.That(command.Notifications, Is.Empty);
            });
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void IsValid_Invalid_Name(string name)
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Name = name;
            var valid = command.IsValid();

            TestContext.WriteLine(command.ToJson());

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
            var command = MocksData.CustomerUpdateCommand;
            command.Birth = DateTime.MinValue;
            var valid = command.IsValid();

            TestContext.WriteLine(command.ToJson());

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
            var command = MocksData.CustomerUpdateCommand;
            command.Birth = DateTime.Now.AddDays(1);
            var valid = command.IsValid();

            TestContext.WriteLine(command.ToJson());

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
            var command = MocksData.CustomerUpdateCommand;
            command.Gender = gender;
            var valid = command.IsValid();

            TestContext.WriteLine(command.ToJson());

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
            var command = MocksData.CustomerUpdateCommand;
            command.CashBalance = cashBalance;
            var valid = command.IsValid();

            TestContext.WriteLine(command.ToJson());

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