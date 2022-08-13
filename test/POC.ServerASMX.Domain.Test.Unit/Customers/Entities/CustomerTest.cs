using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Entities;
using POC.ServerASMX.Infra.Enums;
using POC.ServerASMX.Test.Tools.Base.Unit;
using POC.ServerASMX.Test.Tools.Constants;
using POC.ServerASMX.Test.Tools.Extensions;
using System;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Entities
{
    internal class CustomerTest : UnitTest
    {
        [Test]
        public void IsValid_Valid()
        {
            var customer = MockData.Customer;

            TestContext.WriteLine(customer.ToJson());
            Assert.Multiple(() =>
            {
                Assert.That(customer.Valid, Is.True);
                Assert.That(customer.Invalid, Is.False);
                Assert.That(customer.Notifications, Is.Empty);
            });
        }

        [Test]
        public void Constructor_Success_1()
        {
            var command = MockData.CustomerAddCommand;

            var customer = new Customer(command.Name, command.Birth, command.Gender, command.CashBalance);

            TestContext.WriteLine(customer.ToJson());

            Assert.Multiple(() =>
            {
                Assert.That(customer.Valid, Is.True);
                Assert.That(customer.Invalid, Is.False);
                Assert.That(customer.Notifications, Is.Empty);
                Assert.That(customer.Id, Is.Zero);
                Assert.That(customer.Name, Is.EqualTo(command.Name));
                Assert.That(customer.Birth, Is.EqualTo(command.Birth));
                Assert.That(customer.Gender, Is.EqualTo(command.Gender));
                Assert.That(customer.CashBalance, Is.EqualTo(command.CashBalance));
                Assert.That(customer.Active, Is.True);
                Assert.That(customer.CreationDate.Date, Is.EqualTo(DateTime.Now.Date));
                Assert.That(customer.ChangeDate, Is.Null);
            });
        }

        [Test]
        public void Constructor_Success_2()
        {
            var command = MockData.CustomerUpdateCommand;

            var customer = new Customer(command.Id, command.Name, command.Birth, 
                command.Gender, command.CashBalance, true, DateTime.Now, DateTime.Now.AddDays(1));

            TestContext.WriteLine(customer.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(customer.Valid, Is.True);
                Assert.That(customer.Invalid, Is.False);
                Assert.That(customer.Notifications, Is.Empty);
                Assert.That(customer.Id, Is.EqualTo(command.Id));
                Assert.That(customer.Name, Is.EqualTo(command.Name));
                Assert.That(customer.Birth, Is.EqualTo(command.Birth));
                Assert.That(customer.Gender, Is.EqualTo(command.Gender));
                Assert.That(customer.CashBalance, Is.EqualTo(command.CashBalance));
                Assert.That(customer.Active, Is.True);
                Assert.That(customer.CreationDate.Date, Is.EqualTo(DateTime.Now.Date));
                Assert.That(customer.ChangeDate.Value.Date, Is.EqualTo(DateTime.Now.AddDays(1).Date));
            });
        }

        [Test]
        public void Constructor_Success_3()
        {
            var command = MockData.CustomerUpdateCommand;

            var customer = new Customer(command.Id, command.Name, command.Birth, command.Gender, command.CashBalance, true, DateTime.Now);

            TestContext.WriteLine(customer.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(customer.Valid, Is.True);
                Assert.That(customer.Invalid, Is.False);
                Assert.That(customer.Notifications, Is.Empty);
                Assert.That(customer.Id, Is.EqualTo(command.Id));
                Assert.That(customer.Name, Is.EqualTo(command.Name));
                Assert.That(customer.Birth, Is.EqualTo(command.Birth));
                Assert.That(customer.Gender, Is.EqualTo(command.Gender));
                Assert.That(customer.CashBalance, Is.EqualTo(command.CashBalance));
                Assert.That(customer.Active, Is.True);
                Assert.That(customer.CreationDate.Date, Is.EqualTo(DateTime.Now.Date));
                Assert.That(customer.ChangeDate, Is.Null);
            });
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void SetId_Invalid(long id)
        {
            var customer = MockData.Customer;
            customer.SetId(id);

            TestContext.WriteLine(customer.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(customer.Valid, Is.False);
                Assert.That(customer.Invalid, Is.True);
                Assert.That(customer.Notifications, Is.Not.Empty);
            });
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void SetName_Invalid(string name)
        {
            var customer = MockData.Customer;
            customer.SetName(name);

            TestContext.WriteLine(customer.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(customer.Valid, Is.False);
                Assert.That(customer.Invalid, Is.True);
                Assert.That(customer.Notifications, Is.Not.Empty);
            });
        }

        [Test]
        public void SetBirth_Invalid_DateTimeMin()
        {
            var customer = MockData.Customer;
            customer.SetBirth(DateTime.MinValue);

            TestContext.WriteLine(customer.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(customer.Valid, Is.False);
                Assert.That(customer.Invalid, Is.True);
                Assert.That(customer.Notifications, Is.Not.Empty);
            });
        }

        [Test]
        public void SetBirth_Invalid_FutureDate()
        {
            var customer = MockData.Customer;
            customer.SetBirth(DateTime.Now.AddDays(1));

            TestContext.WriteLine(customer.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(customer.Valid, Is.False);
                Assert.That(customer.Invalid, Is.True);
                Assert.That(customer.Notifications, Is.Not.Empty);
            });
        }

        [Test]
        [TestCase(-1)]
        public void SetGender_Invalid(EGender gender)
        {
            var customer = MockData.Customer;
            customer.SetGender(gender);

            TestContext.WriteLine(customer.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(customer.Valid, Is.False);
                Assert.That(customer.Invalid, Is.True);
                Assert.That(customer.Notifications, Is.Not.Empty);
            });
        }

        [Test]
        [TestCase(-1)]
        public void SetCashBalance_Invalid(decimal cashBalance)
        {
            var customer = MockData.Customer;
            customer.SetCashBalance(cashBalance);

            TestContext.WriteLine(customer.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(customer.Valid, Is.False);
                Assert.That(customer.Invalid, Is.True);
                Assert.That(customer.Notifications, Is.Not.Empty);
            });
        }

        [Test]
        public void MapToCustomerCommandResult_Success()
        {
            var customer = MockData.Customer;
            var commandResult = customer.MapToCustomerCommandResult();

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Id, Is.EqualTo(customer.Id));
                Assert.That(commandResult.Name, Is.EqualTo(customer.Name));
                Assert.That(commandResult.Birth, Is.EqualTo(customer.Birth));
                Assert.That(commandResult.Gender, Is.EqualTo(customer.Gender));
                Assert.That(commandResult.CashBalance, Is.EqualTo(customer.CashBalance));
                Assert.That(commandResult.Active, Is.EqualTo(customer.Active));
                Assert.That(commandResult.CreationDate, Is.EqualTo(customer.CreationDate));
                Assert.That(commandResult.ChangeDate, Is.EqualTo(customer.ChangeDate));
            });
        }

        [Test]
        public void MapToCustomerDTO_Success()
        {
            var customer = MockData.Customer;
            var commandDTO = customer.MapToCustomerDTO();

            TestContext.WriteLine(commandDTO.ToJson());

            Assert.Multiple(() =>
            {
                Assert.That(commandDTO.Id, Is.EqualTo(customer.Id));
                Assert.That(commandDTO.Name, Is.EqualTo(customer.Name));
                Assert.That(commandDTO.Birth, Is.EqualTo(customer.Birth));
                Assert.That(commandDTO.Gender, Is.EqualTo(customer.Gender));
                Assert.That(commandDTO.CashBalance, Is.EqualTo(customer.CashBalance));
                Assert.That(commandDTO.Active, Is.EqualTo(customer.Active));
                Assert.That(commandDTO.CreationDate, Is.EqualTo(customer.CreationDate));
                Assert.That(commandDTO.ChangeDate, Is.EqualTo(customer.ChangeDate));
            });
        }
    }
}