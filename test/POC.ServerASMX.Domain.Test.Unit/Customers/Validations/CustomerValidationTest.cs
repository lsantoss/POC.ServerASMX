using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Domain.Customers.Validations;
using POC.ServerASMX.Infra.Enums;
using POC.ServerASMX.Test.Tools.Base.Unit;
using POC.ServerASMX.Test.Tools.Constants;
using POC.ServerASMX.Test.Tools.Extensions;
using System;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Validations
{
    internal class CustomerValidationTest : UnitTest
    {
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void ValidateId_Valid(long id)
        {
            var notifications = new CustomerValidation().ValidateId(id);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidateId_Invalid(long id)
        {
            var notifications = new CustomerValidation().ValidateId(id);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        [TestCase("Lucas Santos")]
        [TestCase("Lucas S.")]
        public void ValidateName_Valid(string name)
        {
            var notifications = new CustomerValidation().ValidateName(name);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void ValidateName_Invalid(string name)
        {
            var notifications = new CustomerValidation().ValidateName(name);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        public void ValidateBirth_Valid()
        {
            var notifications = new CustomerValidation().ValidateBirth(new DateTime(1995, 07, 14));

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Empty);
        }

        [Test]
        public void ValidateBirth_Invalid_DateTimeMin()
        {
            var notifications = new CustomerValidation().ValidateBirth(DateTime.MinValue);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        public void ValidateBirth_Invalid_FutureDate()
        {
            var notifications = new CustomerValidation().ValidateBirth(DateTime.Now.AddDays(1));

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void ValidateGender_Valid(EGender gender)
        {
            var notifications = new CustomerValidation().ValidateGender(gender);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(-1)]
        public void ValidateGender_Invalid(EGender gender)
        {
            var notifications = new CustomerValidation().ValidateGender(gender);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void ValidateCashBalance_Valid(decimal cashBalance)
        {
            var notifications = new CustomerValidation().ValidateCashBalance(cashBalance);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(-1)]
        public void ValidateCashBalance_Invalid(decimal cashBalance)
        {
            var notifications = new CustomerValidation().ValidateCashBalance(cashBalance);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        public void ValidateCommand_Add_Valid()
        {
            var command = MockData.CustomerAddCommand;

            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(null, -1, -1)]
        [TestCase("", -1, -1)]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters, -1, -1)]
        public void ValidateCommand_Add_Invalid(string name, EGender gender, decimal cashBalance)
        {
            var command = new CustomerAddCommand
            {
                Name = name,
                Birth = DateTime.MinValue,
                Gender = gender,
                CashBalance = cashBalance
            };

            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        public void ValidateCommand_Update_Valid()
        {
            var command = MockData.CustomerUpdateCommand;

            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(-1, null, -1, -1)]
        [TestCase(-1, "", -1, -1)]
        [TestCase(0, StringsWithPredefinedSizes.StringWith101Caracters, -1, -1)]
        public void ValidateCommand_Update_Invalid(long id, string name, EGender gender, decimal cashBalance)
        {
            var command = new CustomerUpdateCommand
            {
                Id = id,
                Name = name,
                Birth = DateTime.MinValue,
                Gender = gender,
                CashBalance = cashBalance
            };

            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        public void ValidateCommand_ActivityState_Valid()
        {
            var command = MockData.CustomerActivityStateCommand;

            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidateCommand_ActivityState_Invalid(long id)
        {
            var command = new CustomerActivityStateCommand
            {
                Id = id
            };

            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        public void ValidateCommand_Delete_Valid()
        {
            var command = MockData.CustomerDeleteCommand;

            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidateCommand_Delete_Invalid(long id)
        {
            var command = new CustomerDeleteCommand
            {
                Id = id
            };

            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            Assert.That(notifications, Is.Not.Empty);
        }
    }
}