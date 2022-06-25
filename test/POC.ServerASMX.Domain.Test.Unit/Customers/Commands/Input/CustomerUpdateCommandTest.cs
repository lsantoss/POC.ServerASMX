using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Enums;
using POC.ServerASMX.Test.Base.Constants;
using POC.ServerASMX.Test.Base.Extensions;
using POC.ServerASMX.Test.Tools.Base.Common;
using System;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Commands.Input
{
    internal class CustomerUpdateCommandTest : BaseTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var command = MocksData.CustomerUpdateCommand;

            TestContext.WriteLine(command.ToJson());

            Assert.True(command.IsValid());
            Assert.AreEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void IsValid_Invalid_Id(long id)
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Id = id;

            TestContext.WriteLine(command.ToJson());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void IsValid_Invalid_Name(string name)
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Name = name;

            TestContext.WriteLine(command.ToJson());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        public void IsValid_Invalid_Birth_DateTimeMin()
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Birth = DateTime.MinValue;

            TestContext.WriteLine(command.ToJson());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        public void IsValid_Invalid_Birth_FutureDate()
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Birth = DateTime.Now.AddDays(1);

            TestContext.WriteLine(command.ToJson());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(-1)]
        public void IsValid_Invalid_Gender(EGender gender)
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Gender = gender;

            TestContext.WriteLine(command.ToJson());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }

        [Test]
        [TestCase(-1)]
        public void IsValid_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksData.CustomerUpdateCommand;
            command.CashBalance = cashBalance;

            TestContext.WriteLine(command.ToJson());

            Assert.False(command.IsValid());
            Assert.AreNotEqual(0, command.Notifications.Count);
        }
    }
}