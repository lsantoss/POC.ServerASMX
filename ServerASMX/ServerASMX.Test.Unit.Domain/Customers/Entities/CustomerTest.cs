using NUnit.Framework;
using ServerASMX.Domain.Customers.Entities;
using ServerASMX.Test.Base.Base;
using ServerASMX.Test.Base.Constants;
using ServerASMX.Test.Base.Extensions;
using System;

namespace ServerASMX.Test.Unit.Customers.Entities
{
    internal class CustomerTest : BaseTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var entity = MocksTest.Customer1;

            var valid = entity.Valid;
            var notifications = entity.Notifications.Count;

            TestContext.WriteLine(entity.Format());

            Assert.True(valid);
            Assert.AreEqual(0, notifications);
        }

        [Test]
        public void Constructor_Success_1()
        {
            var command = MocksTest.CustomerAddCommand;

            var entity = new Customer(command.Name, command.Birth, command.Gender, command.CashBalance);

            var valid = entity.Valid;
            var notifications = entity.Notifications.Count;

            TestContext.WriteLine(entity.Format());

            Assert.True(valid);
            Assert.AreEqual(0, notifications);
            Assert.AreEqual(0, entity.Id);
            Assert.AreEqual(command.Name, entity.Name);
            Assert.AreEqual(command.Birth, entity.Birth);
            Assert.AreEqual(command.Gender, entity.Gender);
            Assert.AreEqual(command.CashBalance, entity.CashBalance);
            Assert.AreEqual(true, entity.Active);
            Assert.AreEqual(DateTime.Now.Date, entity.CreationDate.Date);
            Assert.AreEqual(null, entity.ChangeDate);
        }

        [Test]
        public void Constructor_Success_2()
        {
            var command = MocksTest.CustomerUpdateCommand;

            var entity = new Customer(command.Id, command.Name, command.Birth, 
                command.Gender, command.CashBalance, true, DateTime.Now, DateTime.Now.AddDays(1));

            var valid = entity.Valid;
            var notifications = entity.Notifications.Count;

            TestContext.WriteLine(entity.Format());

            Assert.True(valid);
            Assert.AreEqual(0, notifications);
            Assert.AreEqual(command.Id, entity.Id);
            Assert.AreEqual(command.Name, entity.Name);
            Assert.AreEqual(command.Birth, entity.Birth);
            Assert.AreEqual(command.Gender, entity.Gender);
            Assert.AreEqual(command.CashBalance, entity.CashBalance);
            Assert.AreEqual(true, entity.Active);
            Assert.AreEqual(DateTime.Now.Date, entity.CreationDate.Date);
            Assert.AreEqual(DateTime.Now.AddDays(1).Date, entity.ChangeDate.Value.Date);
        }

        [Test]
        public void Constructor_Success_3()
        {
            var command = MocksTest.CustomerUpdateCommand;

            var entity = new Customer(command.Id, command.Name, command.Birth, command.Gender, command.CashBalance, true, DateTime.Now);

            var valid = entity.Valid;
            var notifications = entity.Notifications.Count;

            TestContext.WriteLine(entity.Format());

            Assert.True(valid);
            Assert.AreEqual(0, notifications);
            Assert.AreEqual(command.Id, entity.Id);
            Assert.AreEqual(command.Name, entity.Name);
            Assert.AreEqual(command.Birth, entity.Birth);
            Assert.AreEqual(command.Gender, entity.Gender);
            Assert.AreEqual(command.CashBalance, entity.CashBalance);
            Assert.AreEqual(true, entity.Active);
            Assert.AreEqual(DateTime.Now.Date, entity.CreationDate.Date);
            Assert.AreEqual(null, entity.ChangeDate);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void SetId_Invalid(long id)
        {
            var entity = MocksTest.Customer1;
            entity.SetId(id);

            var valid = entity.Valid;
            var notifications = entity.Notifications.Count;

            TestContext.WriteLine(entity.Format());

            Assert.False(valid);
            Assert.AreNotEqual(0, notifications);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void SetName_Invalid(string name)
        {
            var entity = MocksTest.Customer1;
            entity.SetName(name);

            var valid = entity.Valid;
            var notifications = entity.Notifications.Count;

            TestContext.WriteLine(entity.Format());

            Assert.False(valid);
            Assert.AreNotEqual(0, notifications);
        }

        [Test]
        public void SetBirth_Invalid_DateTimeMin()
        {
            var entity = MocksTest.Customer1;
            entity.SetBirth(DateTime.MinValue);

            var valid = entity.Valid;
            var notifications = entity.Notifications.Count;

            TestContext.WriteLine(entity.Format());

            Assert.False(valid);
            Assert.AreNotEqual(0, notifications);
        }

        [Test]
        public void SetBirth_Invalid_FutureDate()
        {
            var entity = MocksTest.Customer1;
            entity.SetBirth(DateTime.Now.AddDays(1));

            var valid = entity.Valid;
            var notifications = entity.Notifications.Count;

            TestContext.WriteLine(entity.Format());

            Assert.False(valid);
            Assert.AreNotEqual(0, notifications);
        }

        [Test]
        public void SetGender_Invalid()
        {
            var entity = MocksTest.Customer1;
            entity.SetGender(0);

            var valid = entity.Valid;
            var notifications = entity.Notifications.Count;

            TestContext.WriteLine(entity.Format());

            Assert.False(valid);
            Assert.AreNotEqual(0, notifications);
        }

        [Test]
        [TestCase(-1)]
        public void SetCashBalance_Invalid(decimal cashBalance)
        {
            var entity = MocksTest.Customer1;
            entity.SetCashBalance(cashBalance);

            var valid = entity.Valid;
            var notifications = entity.Notifications.Count;

            TestContext.WriteLine(entity.Format());

            Assert.False(valid);
            Assert.AreNotEqual(0, notifications);
        }

        [Test]
        public void MapToCustomerCommandOutput_Success()
        {
            var entity = MocksTest.Customer1;
            var commandOutput = entity.MapToCustomerCommandOutput();

            TestContext.WriteLine(commandOutput.Format());

            Assert.AreEqual(entity.Id, commandOutput.Id);
            Assert.AreEqual(entity.Name, commandOutput.Name);
            Assert.AreEqual(entity.Birth, commandOutput.Birth);
            Assert.AreEqual(entity.Gender, commandOutput.Gender);
            Assert.AreEqual(entity.CashBalance, commandOutput.CashBalance);
            Assert.AreEqual(entity.Active, commandOutput.Active);
            Assert.AreEqual(entity.CreationDate, commandOutput.CreationDate);
            Assert.AreEqual(entity.ChangeDate, commandOutput.ChangeDate);
        }
    }
}