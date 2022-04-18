using NUnit.Framework;
using ServerASMX.Domain.Customers.Enums;
using ServerASMX.Test.Base.Base;
using ServerASMX.Test.Base.Constants;
using ServerASMX.Test.Base.Extensions;
using System;

namespace ServerASMX.Test.Unit.Domain.Customers.Commands.Input
{
    internal class CustomerAddCommandTest : BaseUnitTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var command = MocksUnitTest.CustomerAddCommand;

            TestContext.WriteLine(command.Format());

            Assert.True(command.IsValid());
            Assert.AreEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void IsValid_Invalid_Name(string name)
        {
            var command = MocksUnitTest.CustomerAddCommand;
            command.Name = name;

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        public void IsValid_Invalid_Birth_DateTimeMin()
        {
            var command = MocksUnitTest.CustomerAddCommand;
            command.Birth = DateTime.MinValue;

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        public void IsValid_Invalid_Birth_FutureDate()
        {
            var command = MocksUnitTest.CustomerAddCommand;
            command.Birth = DateTime.Now.AddDays(1);

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(-1)]
        public void IsValid_Invalid_Gender(EGender gender)
        {
            var command = MocksUnitTest.CustomerAddCommand;
            command.Gender = gender;

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(-1)]
        public void IsValid_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksUnitTest.CustomerAddCommand;
            command.CashBalance = cashBalance;

            TestContext.WriteLine(command.Format());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        public void MapToCustomer_Success()
        {
            var command = MocksUnitTest.CustomerAddCommand;
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