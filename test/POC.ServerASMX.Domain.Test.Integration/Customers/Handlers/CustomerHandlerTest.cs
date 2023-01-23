using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Domain.Customers.Commands.Result;
using POC.ServerASMX.Domain.Customers.Handlers;
using POC.ServerASMX.Domain.Customers.Interfaces.Handlers;
using POC.ServerASMX.Infra.Enums;
using POC.ServerASMX.Test.Tools.Base.Integration;
using POC.ServerASMX.Test.Tools.Constants;
using POC.ServerASMX.Test.Tools.Extensions;
using System;

namespace POC.ServerASMX.Domain.Test.Integration.Customers.Handlers
{
    internal class CustomerHandlerTest : IntegrationTest
    {
        private readonly ICustomerHandler _handler;

        public CustomerHandlerTest() => _handler = new CustomerHandler();

        [Test]
        public void Handle_Add_Success()
        {
            //Arrange
            var command = MockData.CustomerAddCommand;

            //Act
            var commandResult = _handler.Handle(command);

            var result = (CustomerCommandResult)commandResult.Data;
            
            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.True);
                Assert.That(commandResult.Message, Is.EqualTo("Customer successfully inserted!"));
                Assert.That(commandResult.Errors, Is.Empty);
                Assert.That(result.Id, Is.EqualTo(1));
                Assert.That(result.Name, Is.EqualTo(command.Name));
                Assert.That(result.Birth, Is.EqualTo(command.Birth));
                Assert.That(result.Gender, Is.EqualTo(command.Gender));
                Assert.That(result.CashBalance, Is.EqualTo(command.CashBalance));
                Assert.That(result.Active, Is.True);
                Assert.That(result.CreationDate.Date, Is.EqualTo(DateTime.Now.Date));
                Assert.That(result.ChangeDate, Is.Null);
            });
        }

        [Test]
        public void Handle_Add_Invalid_Command_Null()
        {
            //Arrange
            var command = (CustomerAddCommand)null;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        [TestCase(null, -1, -1)]
        [TestCase("", -1, -1)]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters, -1, -1)]
        public void Handle_Add_Invalid_Command(string name, EGender gender, decimal cashBalance)
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
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void Handle_Add_Invalid_Name(string name)
        {
            //Arrange
            var command = MockData.CustomerAddCommand;
            command.Name = name;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Handle_Add_Invalid_Birth_DateTimeMin()
        {
            //Arrange
            var command = MockData.CustomerAddCommand;
            command.Birth = DateTime.MinValue;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Handle_Add_Invalid_Birth_FutureDate()
        {
            //Arrange
            var command = MockData.CustomerAddCommand;
            command.Birth = DateTime.Now.AddDays(1);

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        [TestCase(-1)]
        public void Handle_Add_Invalid_Gender(EGender gender)
        {
            //Arrange
            var command = MockData.CustomerAddCommand;
            command.Gender = gender;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        [TestCase(-1)]
        public void Handle_Add_Invalid_CashBalance(decimal cashBalance)
        {
            //Arrange
            var command = MockData.CustomerAddCommand;
            command.CashBalance = cashBalance;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Handle_Update_Success()
        {
            //Arrange
            _handler.Handle(MockData.CustomerAddCommand);

            var command = MockData.CustomerUpdateCommand;

            //Act
            var commandResult = _handler.Handle(command);

            var result = (CustomerCommandResult)commandResult.Data;

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.True);
                Assert.That(commandResult.Message, Is.EqualTo("Customer successfully updated!"));
                Assert.That(commandResult.Errors, Is.Empty);
                Assert.That(result.Id, Is.EqualTo(command.Id));
                Assert.That(result.Name, Is.EqualTo(command.Name));
                Assert.That(result.Birth, Is.EqualTo(command.Birth));
                Assert.That(result.Gender, Is.EqualTo(command.Gender));
                Assert.That(result.CashBalance, Is.EqualTo(command.CashBalance));
                Assert.That(result.Active, Is.True);
                Assert.That(result.CreationDate.Date, Is.EqualTo(DateTime.Now.Date));
                Assert.That(result.ChangeDate.Value.Date, Is.EqualTo(DateTime.Now.Date));
            });
        }

        [Test]
        public void Handle_Update_Invalid_Command_Null()
        {
            //Arrange
            var command = (CustomerUpdateCommand)null;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        [TestCase(-1, null, -1, -1)]
        [TestCase(-1, "", -1, -1)]
        [TestCase(0, StringsWithPredefinedSizes.StringWith101Caracters, -1, -1)]
        public void Handle_Update_Invalid_Command(long id, string name, EGender gender, decimal cashBalance)
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
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Handle_Update_Invalid_Not_Resgistred_Id()
        {
            //Arrange
            var command = MockData.CustomerUpdateCommand;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Inconsistencies in the data"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Handle_Update_Invalid_Id(long id)
        {
            //Arrange
            var command = MockData.CustomerUpdateCommand;
            command.Id = id;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void Handle_Update_Invalid_Name(string name)
        {
            //Arrange
            var command = MockData.CustomerUpdateCommand;
            command.Name = name;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Handle_Update_Invalid_Birth_DateTimeMin()
        {
            //Arrange
            var command = MockData.CustomerUpdateCommand;
            command.Birth = DateTime.MinValue;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Handle_Update_Invalid_Birth_FutureDate()
        {
            //Arrange
            var command = MockData.CustomerUpdateCommand;
            command.Birth = DateTime.Now.AddDays(1);

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        [TestCase(-1)]
        public void Handle_Update_Invalid_Gender(EGender gender)
        {
            //Arrange
            var command = MockData.CustomerUpdateCommand;
            command.Gender = gender;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        [TestCase(-1)]
        public void Handle_Update_Invalid_CashBalance(decimal cashBalance)
        {
            //Arrange
            var command = MockData.CustomerUpdateCommand;
            command.CashBalance = cashBalance;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Handle_Activity_State_Success()
        {
            //Arrange
            _handler.Handle(MockData.CustomerAddCommand);

            var command = MockData.CustomerActivityStateCommand;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.True);
                Assert.That(commandResult.Message, Is.EqualTo("Customer successfully updated!"));
                Assert.That(commandResult.Errors, Is.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Handle_Activity_State_Invalid_Command_Null()
        {
            //Arrange
            var command = (CustomerActivityStateCommand)null;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Handle_Activity_State_Invalid_Not_Resgistred_Id()
        {
            //Arrange
            var command = MockData.CustomerActivityStateCommand;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Inconsistencies in the data"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Handle_Activity_State_Invalid_Id(long id)
        {
            //Arrange
            var command = MockData.CustomerActivityStateCommand;
            command.Id = id;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Handle_Delete_Success()
        {
            //Arrange
            _handler.Handle(MockData.CustomerAddCommand);

            var command = MockData.CustomerDeleteCommand;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.True);
                Assert.That(commandResult.Message, Is.EqualTo("Customer successfully deleted!"));
                Assert.That(commandResult.Errors, Is.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Handle_Delete_Invalid_Command_Null()
        {
            //Arrange
            var command = (CustomerDeleteCommand)null;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Handle_Delete_Invalid_Not_Resgistred_Id()
        {
            //Arrange
            var command = MockData.CustomerDeleteCommand;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Inconsistencies in the data"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Handle_Delete_Invalid_Id(long id)
        {
            //Arrange
            var command = MockData.CustomerDeleteCommand;
            command.Id = id;

            //Act
            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.ToJson());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }
    }
}
