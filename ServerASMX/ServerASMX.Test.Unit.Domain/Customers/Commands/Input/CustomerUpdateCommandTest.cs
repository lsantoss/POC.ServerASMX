using NUnit.Framework;
using ServerASMX.Domain.Customers.Enums;
using ServerASMX.Test.Base.Base;
using ServerASMX.Test.Base.Constants;
using ServerASMX.Test.Base.Extensions;
using System;

namespace ServerASMX.Test.Unit.Domain.Customers.Commands.Input
{
    internal class CustomerUpdateCommandTest : BaseTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var command = MocksTest.CustomerUpdateCommand;

            TestContext.WriteLine(command.Format());

            Assert.True(command.IsValid());
            Assert.AreEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void IsValid_Invalid_Id(long id)
        {
            var command = MocksTest.CustomerUpdateCommand;
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
            var command = MocksTest.CustomerUpdateCommand;
            command.Name = name;

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        public void IsValid_Invalid_Birth_DateTimeMin()
        {
            var command = MocksTest.CustomerUpdateCommand;
            command.Birth = DateTime.MinValue;

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        public void IsValid_Invalid_Birth_FutureDate()
        {
            var command = MocksTest.CustomerUpdateCommand;
            command.Birth = DateTime.Now.AddDays(1);

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(-1)]
        public void IsValid_Invalid_Gender(EGender gender)
        {
            var command = MocksTest.CustomerUpdateCommand;
            command.Gender = gender;

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(-1)]
        public void IsValid_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksTest.CustomerUpdateCommand;
            command.CashBalance = cashBalance;

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }
    }
}