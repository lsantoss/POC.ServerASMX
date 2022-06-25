using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Enums;
using POC.ServerASMX.Test.Base.Constants;
using POC.ServerASMX.Test.Base.Extensions;
using POC.ServerASMX.Test.Tools.Base.Common;
using System;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Commands.Input
{
    internal class CustomerAddCommandTest : BaseTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var command = MocksData.CustomerAddCommand;

            TestContext.WriteLine(command.ToJson());

            Assert.True(command.IsValid());
            Assert.AreEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void IsValid_Invalid_Name(string name)
        {
            var command = MocksData.CustomerAddCommand;
            command.Name = name;

            TestContext.WriteLine(command.ToJson());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        public void IsValid_Invalid_Birth_DateTimeMin()
        {
            var command = MocksData.CustomerAddCommand;
            command.Birth = DateTime.MinValue;

            TestContext.WriteLine(command.ToJson());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        public void IsValid_Invalid_Birth_FutureDate()
        {
            var command = MocksData.CustomerAddCommand;
            command.Birth = DateTime.Now.AddDays(1);

            TestContext.WriteLine(command.ToJson());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(-1)]
        public void IsValid_Invalid_Gender(EGender gender)
        {
            var command = MocksData.CustomerAddCommand;
            command.Gender = gender;

            TestContext.WriteLine(command.ToJson());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(-1)]
        public void IsValid_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksData.CustomerAddCommand;
            command.CashBalance = cashBalance;

            TestContext.WriteLine(command.ToJson());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        public void MapToCustomer_Success()
        {
            var command = MocksData.CustomerAddCommand;
            var mapResult = command.MapToCustomer();

            TestContext.WriteLine(mapResult.ToJson());

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