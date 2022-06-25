using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Entities;
using POC.ServerASMX.Domain.Customers.Enums;
using POC.ServerASMX.Test.Base.Constants;
using POC.ServerASMX.Test.Base.Extensions;
using POC.ServerASMX.Test.Tools.Base.Common;
using System;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Entities
{
    internal class CustomerTest : BaseTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var customer = MocksData.Customer;

            TestContext.WriteLine(customer.ToJson());

            Assert.True(customer.Valid);
            Assert.AreEqual(0, customer.Notifications.Count);
        }

        [Test]
        public void Constructor_Success_1()
        {
            var command = MocksData.CustomerAddCommand;

            var customer = new Customer(command.Name, command.Birth, command.Gender, command.CashBalance);

            TestContext.WriteLine(customer.ToJson());

            Assert.IsTrue(customer.Valid);
            Assert.AreEqual(0, customer.Notifications.Count);
            Assert.AreEqual(0, customer.Id);
            Assert.AreEqual(command.Name, customer.Name);
            Assert.AreEqual(command.Birth, customer.Birth);
            Assert.AreEqual(command.Gender, customer.Gender);
            Assert.AreEqual(command.CashBalance, customer.CashBalance);
            Assert.IsTrue(customer.Active);
            Assert.AreEqual(DateTime.Now.Date, customer.CreationDate.Date);
            Assert.IsNull(customer.ChangeDate);
        }

        [Test]
        public void Constructor_Success_2()
        {
            var command = MocksData.CustomerUpdateCommand;

            var customer = new Customer(command.Id, command.Name, command.Birth, 
                command.Gender, command.CashBalance, true, DateTime.Now, DateTime.Now.AddDays(1));

            TestContext.WriteLine(customer.ToJson());

            Assert.True(customer.Valid);
            Assert.AreEqual(0, customer.Notifications.Count);
            Assert.AreEqual(command.Id, customer.Id);
            Assert.AreEqual(command.Name, customer.Name);
            Assert.AreEqual(command.Birth, customer.Birth);
            Assert.AreEqual(command.Gender, customer.Gender);
            Assert.AreEqual(command.CashBalance, customer.CashBalance);
            Assert.IsTrue(customer.Active);
            Assert.AreEqual(DateTime.Now.Date, customer.CreationDate.Date);
            Assert.AreEqual(DateTime.Now.AddDays(1).Date, customer.ChangeDate.Value.Date);
        }

        [Test]
        public void Constructor_Success_3()
        {
            var command = MocksData.CustomerUpdateCommand;

            var customer = new Customer(command.Id, command.Name, command.Birth, command.Gender, command.CashBalance, true, DateTime.Now);

            TestContext.WriteLine(customer.ToJson());

            Assert.True(customer.Valid);
            Assert.AreEqual(0, customer.Notifications.Count);
            Assert.AreEqual(command.Id, customer.Id);
            Assert.AreEqual(command.Name, customer.Name);
            Assert.AreEqual(command.Birth, customer.Birth);
            Assert.AreEqual(command.Gender, customer.Gender);
            Assert.AreEqual(command.CashBalance, customer.CashBalance);
            Assert.IsTrue(customer.Active);
            Assert.AreEqual(DateTime.Now.Date, customer.CreationDate.Date);
            Assert.IsNull(customer.ChangeDate);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void SetId_Invalid(long id)
        {
            var customer = MocksData.Customer;
            customer.SetId(id);

            TestContext.WriteLine(customer.ToJson());

            Assert.False(customer.Valid);
            Assert.AreNotEqual(0, customer.Notifications.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void SetName_Invalid(string name)
        {
            var customer = MocksData.Customer;
            customer.SetName(name);

            TestContext.WriteLine(customer.ToJson());

            Assert.False(customer.Valid);
            Assert.AreNotEqual(0, customer.Notifications.Count);
        }

        [Test]
        public void SetBirth_Invalid_DateTimeMin()
        {
            var customer = MocksData.Customer;
            customer.SetBirth(DateTime.MinValue);

            TestContext.WriteLine(customer.ToJson());

            Assert.False(customer.Valid);
            Assert.AreNotEqual(0, customer.Notifications.Count);
        }

        [Test]
        public void SetBirth_Invalid_FutureDate()
        {
            var customer = MocksData.Customer;
            customer.SetBirth(DateTime.Now.AddDays(1));

            TestContext.WriteLine(customer.ToJson());

            Assert.False(customer.Valid);
            Assert.AreNotEqual(0, customer.Notifications.Count);
        }

        [Test]
        [TestCase(-1)]
        public void SetGender_Invalid(EGender gender)
        {
            var customer = MocksData.Customer;
            customer.SetGender(gender);

            TestContext.WriteLine(customer.ToJson());

            Assert.False(customer.Valid);
            Assert.AreNotEqual(0, customer.Notifications.Count);
        }

        [Test]
        [TestCase(-1)]
        public void SetCashBalance_Invalid(decimal cashBalance)
        {
            var customer = MocksData.Customer;
            customer.SetCashBalance(cashBalance);

            TestContext.WriteLine(customer.ToJson());

            Assert.False(customer.Valid);
            Assert.AreNotEqual(0, customer.Notifications.Count);
        }

        [Test]
        public void MapToCustomerCommandResult_Success()
        {
            var customer = MocksData.Customer;
            var commandResult = customer.MapToCustomerCommandResult();

            TestContext.WriteLine(commandResult.ToJson());

            Assert.AreEqual(customer.Id, commandResult.Id);
            Assert.AreEqual(customer.Name, commandResult.Name);
            Assert.AreEqual(customer.Birth, commandResult.Birth);
            Assert.AreEqual(customer.Gender, commandResult.Gender);
            Assert.AreEqual(customer.CashBalance, commandResult.CashBalance);
            Assert.AreEqual(customer.Active, commandResult.Active);
            Assert.AreEqual(customer.CreationDate, commandResult.CreationDate);
            Assert.AreEqual(customer.ChangeDate, commandResult.ChangeDate);
        }
    }
}