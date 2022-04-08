using NUnit.Framework;
using ServerASMX.Test.Base.Base;
using ServerASMX.Test.Base.Constants;
using ServerASMX.Test.Base.Extensions;
using System;

namespace ServerASMX.Test.Unit.Customers.Commands.Input
{
    internal class CustomerAddCommandTest : BaseTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var command = MocksTest.CustomerAddCommand;

            var valid = command.IsValid();
            var notifications = command.Notifications.Count;

            TestContext.WriteLine(command.Format());

            Assert.True(valid);
            Assert.AreEqual(0, notifications);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void IsValid_Invalid_Name(string name)
        {
            var command = MocksTest.CustomerAddCommand;
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
            var command = MocksTest.CustomerAddCommand;
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
            var command = MocksTest.CustomerAddCommand;
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
            var command = MocksTest.CustomerAddCommand;
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
            var command = MocksTest.CustomerAddCommand;
            command.CashBalance = cashBalance;

            var valid = command.IsValid();
            var notifications = command.Notifications.Count;

            TestContext.WriteLine(command.Format());

            Assert.False(valid);
            Assert.AreNotEqual(0, notifications);
        }

        [Test]
        public void MapToCustomer_Success()
        {
            var command = MocksTest.CustomerAddCommand;
            var mapResult = command.MapToCustomer();

            TestContext.WriteLine(mapResult.Format());

            Assert.AreEqual(0, mapResult.Id);
            Assert.AreEqual(command.Name, mapResult.Name);
            Assert.AreEqual(command.Birth, mapResult.Birth);
            Assert.AreEqual(command.Gender, mapResult.Gender);
            Assert.AreEqual(command.CashBalance, mapResult.CashBalance);
            Assert.IsTrue(mapResult.Active);
            Assert.AreEqual(DateTime.Now.Date, mapResult.CreationDate.Date);
            Assert.IsNull(mapResult.ChangeDate);
        }
    }
}