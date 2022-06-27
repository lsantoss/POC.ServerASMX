using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Enums;
using POC.ServerASMX.Test.Base.Constants;
using POC.ServerASMX.Test.Base.Extensions;
using POC.ServerASMX.Test.Tools.Base.Unit;
using System;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Commands.Input
{
    internal class CustomerAddCommandTest : UnitTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var command = MocksData.CustomerAddCommand;

            TestContext.WriteLine(command.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(command.IsValid(), Is.True);
                Assert.That(command.Notifications, Is.Empty);
            });
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
            
            Assert.Multiple(() =>
            {
                Assert.That(command.IsValid(), Is.False);
                Assert.That(command.Notifications, Is.Not.Empty);
            });
        }

        [Test]
        public void IsValid_Invalid_Birth_DateTimeMin()
        {
            var command = MocksData.CustomerAddCommand;
            command.Birth = DateTime.MinValue;

            TestContext.WriteLine(command.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(command.IsValid(), Is.False);
                Assert.That(command.Notifications, Is.Not.Empty);
            });
        }

        [Test]
        public void IsValid_Invalid_Birth_FutureDate()
        {
            var command = MocksData.CustomerAddCommand;
            command.Birth = DateTime.Now.AddDays(1);

            TestContext.WriteLine(command.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(command.IsValid(), Is.False);
                Assert.That(command.Notifications, Is.Not.Empty);
            });
        }

        [Test]
        [TestCase(-1)]
        public void IsValid_Invalid_Gender(EGender gender)
        {
            var command = MocksData.CustomerAddCommand;
            command.Gender = gender;

            TestContext.WriteLine(command.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(command.IsValid(), Is.False);
                Assert.That(command.Notifications, Is.Not.Empty);
            });
        }

        [Test]
        [TestCase(-1)]
        public void IsValid_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksData.CustomerAddCommand;
            command.CashBalance = cashBalance;

            TestContext.WriteLine(command.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(command.IsValid(), Is.False);
                Assert.That(command.Notifications, Is.Not.Empty);
            });
        }

        [Test]
        public void MapToCustomer_Success()
        {
            var command = MocksData.CustomerAddCommand;
            var mapResult = command.MapToCustomer();

            TestContext.WriteLine(mapResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(mapResult.Id, Is.EqualTo(0));
                Assert.That(mapResult.Name, Is.EqualTo(command.Name));
                Assert.That(mapResult.Birth, Is.EqualTo(command.Birth));
                Assert.That(mapResult.Gender, Is.EqualTo(command.Gender));
                Assert.That(mapResult.CashBalance, Is.EqualTo(command.CashBalance));
                Assert.That(mapResult.Active, Is.True);
                Assert.That(mapResult.CreationDate.Date, Is.EqualTo(DateTime.Now.Date));
                Assert.That(mapResult.ChangeDate, Is.Null);
            });
        }
    }
}