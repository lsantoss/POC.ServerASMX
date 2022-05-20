using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Enums;
using POC.ServerASMX.Test.Base.Base;
using POC.ServerASMX.Test.Base.Constants;
using POC.ServerASMX.Test.Base.Extensions;
using System;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Commands.Input
{
    internal class CustomerUpdateCommandTest : BaseUnitTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var command = MocksUnitTest.CustomerUpdateCommand;

            TestContext.WriteLine(command.Format());

            Assert.True(command.IsValid());
            Assert.AreEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void IsValid_Invalid_Id(long id)
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.Id = id;

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void IsValid_Invalid_Name(string name)
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.Name = name;

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        public void IsValid_Invalid_Birth_DateTimeMin()
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.Birth = DateTime.MinValue;

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        public void IsValid_Invalid_Birth_FutureDate()
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.Birth = DateTime.Now.AddDays(1);

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(-1)]
        public void IsValid_Invalid_Gender(EGender gender)
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.Gender = gender;

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(-1)]
        public void IsValid_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.CashBalance = cashBalance;

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }
    }
}