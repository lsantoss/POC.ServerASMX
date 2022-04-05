using NUnit.Framework;
using ServerASMX.Test.Base.Base;
using ServerASMX.Test.Base.Constants;
using ServerASMX.Test.Base.Extensions;
using System;

namespace ServerASMX.Test.Unit.Customers.Commands.Input
{
    internal class CustomerUpdateCommandTest : BaseTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var command = MocksTest.CustomerUpdateCommand;

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
            var command = MocksTest.CustomerUpdateCommand;
            command.Id = id;

            var valid = command.IsValid();
            var notifications = command.Notifications.Count;

            TestContext.WriteLine(command.Format());

            Assert.False(valid);
            Assert.AreNotEqual(0, notifications);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void IsValid_Invalid_Name(string name)
        {
            var command = MocksTest.CustomerUpdateCommand;
            command.Name = name;

            var valid = command.IsValid();
            var notifications = command.Notifications.Count;

            TestContext.WriteLine(command.Format());

            Assert.False(valid);
            Assert.AreNotEqual(0, notifications);
        }

        [Test]
        public void IsValid_Invalid_Birth_DateTimeMin()
        {
            var command = MocksTest.CustomerUpdateCommand;
            command.Birth = DateTime.MinValue;

            var valid = command.IsValid();
            var notifications = command.Notifications.Count;

            TestContext.WriteLine(command.Format());

            Assert.False(valid);
            Assert.AreNotEqual(0, notifications);
        }

        [Test]
        public void IsValid_Invalid_Birth_FutureDate()
        {
            var command = MocksTest.CustomerUpdateCommand;
            command.Birth = DateTime.Now.AddDays(1);

            var valid = command.IsValid();
            var notifications = command.Notifications.Count;

            TestContext.WriteLine(command.Format());

            Assert.False(valid);
            Assert.AreNotEqual(0, notifications);
        }

        [Test]
        public void IsValid_Invalid_Gender()
        {
            var command = MocksTest.CustomerUpdateCommand;
            command.Gender = 0;

            var valid = command.IsValid();
            var notifications = command.Notifications.Count;

            TestContext.WriteLine(command.Format());

            Assert.False(valid);
            Assert.AreNotEqual(0, notifications);
        }

        [Test]
        [TestCase(-1)]
        public void IsValid_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksTest.CustomerUpdateCommand;
            command.CashBalance = cashBalance;

            var valid = command.IsValid();
            var notifications = command.Notifications.Count;

            TestContext.WriteLine(command.Format());

            Assert.False(valid);
            Assert.AreNotEqual(0, notifications);
        }
    }
}