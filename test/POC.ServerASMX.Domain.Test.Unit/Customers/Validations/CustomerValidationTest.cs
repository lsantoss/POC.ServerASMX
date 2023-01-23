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
            //Act
            var notifications = new CustomerValidation().ValidateId(id);

            TestContext.WriteLine(notifications.ToJson());

            //Arrange
            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidateId_Invalid(long id)
        {
            //Act
            var notifications = new CustomerValidation().ValidateId(id);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        [TestCase("Lucas Santos")]
        [TestCase("Lucas S.")]
        public void ValidateName_Valid(string name)
        {
            //Act
            var notifications = new CustomerValidation().ValidateName(name);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void ValidateName_Invalid(string name)
        {
            //Act
            var notifications = new CustomerValidation().ValidateName(name);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        public void ValidateBirth_Valid()
        {
            //Act
            var notifications = new CustomerValidation().ValidateBirth(new DateTime(1995, 07, 14));

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Empty);
        }

        [Test]
        public void ValidateBirth_Invalid_DateTimeMin()
        {
            //Act
            var notifications = new CustomerValidation().ValidateBirth(DateTime.MinValue);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        public void ValidateBirth_Invalid_FutureDate()
        {
            //Act
            var notifications = new CustomerValidation().ValidateBirth(DateTime.Now.AddDays(1));

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void ValidateGender_Valid(EGender gender)
        {
            //Act
            var notifications = new CustomerValidation().ValidateGender(gender);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(-1)]
        public void ValidateGender_Invalid(EGender gender)
        {
            //Act
            var notifications = new CustomerValidation().ValidateGender(gender);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void ValidateCashBalance_Valid(decimal cashBalance)
        {
            //Act
            var notifications = new CustomerValidation().ValidateCashBalance(cashBalance);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(-1)]
        public void ValidateCashBalance_Invalid(decimal cashBalance)
        {
            //Act
            var notifications = new CustomerValidation().ValidateCashBalance(cashBalance);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        public void ValidateCommand_Add_Valid()
        {
            //Arrange
            var command = MockData.CustomerAddCommand;

            //Act
            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(null, -1, -1)]
        [TestCase("", -1, -1)]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters, -1, -1)]
        public void ValidateCommand_Add_Invalid(string name, EGender gender, decimal cashBalance)
        {
            //Arrange
            var command = new CustomerAddCommand
            {
                Name = name,
                Birth = DateTime.MinValue,
                Gender = gender,
                CashBalance = cashBalance
            };

            //Act
            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        public void ValidateCommand_Update_Valid()
        {
            //Arrange
            var command = MockData.CustomerUpdateCommand;

            //Act
            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(-1, null, -1, -1)]
        [TestCase(-1, "", -1, -1)]
        [TestCase(0, StringsWithPredefinedSizes.StringWith101Caracters, -1, -1)]
        public void ValidateCommand_Update_Invalid(long id, string name, EGender gender, decimal cashBalance)
        {
            //Arrange
            var command = new CustomerUpdateCommand
            {
                Id = id,
                Name = name,
                Birth = DateTime.MinValue,
                Gender = gender,
                CashBalance = cashBalance
            };

            //Act
            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        public void ValidateCommand_ActivityState_Valid()
        {
            //Arrange
            var command = MockData.CustomerActivityStateCommand;

            //Act
            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidateCommand_ActivityState_Invalid(long id)
        {
            //Arrange
            var command = new CustomerActivityStateCommand
            {
                Id = id
            };

            //Act
            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Not.Empty);
        }

        [Test]
        public void ValidateCommand_Delete_Valid()
        {
            //Arrange
            var command = MockData.CustomerDeleteCommand;

            //Act
            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Empty);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ValidateCommand_Delete_Invalid(long id)
        {
            //Arrange
            var command = new CustomerDeleteCommand
            {
                Id = id
            };

            //Act
            var notifications = new CustomerValidation().ValidateCommand(command);

            TestContext.WriteLine(notifications.ToJson());

            //Assert
            Assert.That(notifications, Is.Not.Empty);
        }
    }
}