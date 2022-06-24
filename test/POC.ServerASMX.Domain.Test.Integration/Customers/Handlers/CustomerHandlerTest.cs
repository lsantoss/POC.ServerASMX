using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Domain.Customers.Commands.Result;
using POC.ServerASMX.Domain.Customers.Enums;
using POC.ServerASMX.Domain.Customers.Handlers;
using POC.ServerASMX.Domain.Customers.Interfaces.Handlers;
using POC.ServerASMX.Test.Base.Base;
using POC.ServerASMX.Test.Base.Constants;
using POC.ServerASMX.Test.Base.Extensions;
using System;
using System.Threading.Tasks;

namespace POC.ServerASMX.Domain.Test.Integration.Customers.Handlers
{
    internal class CustomerHandlerTest : DatabaseUnitTest
    {
        private readonly ICustomerHandler _handler;

        public CustomerHandlerTest() => _handler = new CustomerHandler();

        [Test]
        public async Task HandleAsync_Add_Success()
        {
            var command = MocksUnitTest.CustomerAddCommand;

            var commandResult = await _handler.HandleAsync(command);

            var result = (CustomerCommandResult)commandResult.Data;

            TestContext.WriteLine(commandResult.Format());

            Assert.True(commandResult.Success);
            Assert.AreEqual("Customer successfully inserted!", commandResult.Message);
            Assert.AreEqual(0, commandResult.Errors.Count);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(command.Name, result.Name);
            Assert.AreEqual(command.Birth, result.Birth);
            Assert.AreEqual(command.Gender, result.Gender);
            Assert.AreEqual(command.CashBalance, result.CashBalance);
            Assert.IsTrue(result.Active);
            Assert.AreEqual(DateTime.Now.Date, result.CreationDate.Date);
            Assert.IsNull(result.ChangeDate);
        }

        [Test]
        public async Task HandleAsync_Add_Invalid_Command_Null()
        {
            var command = (CustomerAddCommand)null;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(null, -1, -1)]
        [TestCase("", -1, -1)]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters, -1, -1)]
        public async Task HandleAsync_Add_Invalid_Command(string name, EGender gender, decimal cashBalance)
        {
            var command = new CustomerAddCommand
            {
                Name = name,
                Birth = DateTime.MinValue,
                Gender = gender,
                CashBalance = cashBalance
            };

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public async Task HandleAsync_Add_Invalid_Name(string name)
        {
            var command = MocksUnitTest.CustomerAddCommand;
            command.Name = name;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task HandleAsync_Add_Invalid_Birth_DateTimeMin()
        {
            var command = MocksUnitTest.CustomerAddCommand;
            command.Birth = DateTime.MinValue;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task HandleAsync_Add_Invalid_Birth_FutureDate()
        {
            var command = MocksUnitTest.CustomerAddCommand;
            command.Birth = DateTime.Now.AddDays(1);

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1)]
        public async Task HandleAsync_Add_Invalid_Gender(EGender gender)
        {
            var command = MocksUnitTest.CustomerAddCommand;
            command.Gender = gender;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1)]
        public async Task HandleAsync_Add_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksUnitTest.CustomerAddCommand;
            command.CashBalance = cashBalance;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task HandleAsync_Update_Success()
        {
            await _handler.HandleAsync(MocksUnitTest.CustomerAddCommand);

            var command = MocksUnitTest.CustomerUpdateCommand;

            var commandResult = await _handler.HandleAsync(command);

            var result = (CustomerCommandResult)commandResult.Data;

            TestContext.WriteLine(commandResult.Format());

            Assert.True(commandResult.Success);
            Assert.AreEqual("Customer successfully updated!", commandResult.Message);
            Assert.AreEqual(0, commandResult.Errors.Count);
            Assert.AreEqual(command.Id, result.Id);
            Assert.AreEqual(command.Name, result.Name);
            Assert.AreEqual(command.Birth, result.Birth);
            Assert.AreEqual(command.Gender, result.Gender);
            Assert.AreEqual(command.CashBalance, result.CashBalance);
            Assert.IsTrue(result.Active);
            Assert.AreEqual(DateTime.Now.Date, result.CreationDate.Date);
            Assert.AreEqual(DateTime.Now.Date, result.ChangeDate.Value.Date);
        }

        [Test]
        public async Task HandleAsync_Update_Invalid_Command_Null()
        {
            var command = (CustomerUpdateCommand)null;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1, null, -1, -1)]
        [TestCase(-1, "", -1, -1)]
        [TestCase(0, StringsWithPredefinedSizes.StringWith101Caracters, -1, -1)]
        public async Task HandleAsync_Update_Invalid_Command(long id, string name, EGender gender, decimal cashBalance)
        {
            var command = new CustomerUpdateCommand
            {
                Id = id,
                Name = name,
                Birth = DateTime.MinValue,
                Gender = gender,
                CashBalance = cashBalance
            };

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task HandleAsync_Update_Invalid_Not_Resgistred_Id()
        {
            var command = MocksUnitTest.CustomerUpdateCommand;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Inconsistencies in the data", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task HandleAsync_Update_Invalid_Id(long id)
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.Id = id;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public async Task HandleAsync_Update_Invalid_Name(string name)
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.Name = name;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task HandleAsync_Update_Invalid_Birth_DateTimeMin()
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.Birth = DateTime.MinValue;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task HandleAsync_Update_Invalid_Birth_FutureDate()
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.Birth = DateTime.Now.AddDays(1);

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1)]
        public async Task HandleAsync_Update_Invalid_Gender(EGender gender)
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.Gender = gender;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1)]
        public async Task HandleAsync_Update_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.CashBalance = cashBalance;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task HandleAsync_Activity_State_Success()
        {
            await _handler.HandleAsync(MocksUnitTest.CustomerAddCommand);

            var command = MocksUnitTest.CustomerActivityStateCommand;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.True(commandResult.Success);
            Assert.AreEqual("Customer successfully updated!", commandResult.Message);
            Assert.AreEqual(0, commandResult.Errors.Count);
            Assert.IsNull(commandResult.Data);
        }

        [Test]
        public async Task HandleAsync_Activity_State_Invalid_Command_Null()
        {
            var command = (CustomerActivityStateCommand)null;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task HandleAsync_Activity_State_Invalid_Not_Resgistred_Id()
        {
            var command = MocksUnitTest.CustomerActivityStateCommand;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Inconsistencies in the data", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task HandleAsync_Activity_State_Invalid_Id(long id)
        {
            var command = MocksUnitTest.CustomerActivityStateCommand;
            command.Id = id;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task HandleAsync_Delete_Success()
        {
            await _handler.HandleAsync(MocksUnitTest.CustomerAddCommand);

            var command = MocksUnitTest.CustomerDeleteCommand;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.True(commandResult.Success);
            Assert.AreEqual("Customer successfully deleted!", commandResult.Message);
            Assert.AreEqual(0, commandResult.Errors.Count);
            Assert.IsNull(commandResult.Data);
        }

        [Test]
        public async Task HandleAsync_Delete_Invalid_Command_Null()
        {
            var command = (CustomerDeleteCommand)null;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task HandleAsync_Delete_Invalid_Not_Resgistred_Id()
        {
            var command = MocksUnitTest.CustomerDeleteCommand;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Inconsistencies in the data", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task HandleAsync_Delete_Invalid_Id(long id)
        {
            var command = MocksUnitTest.CustomerDeleteCommand;
            command.Id = id;

            var commandResult = await _handler.HandleAsync(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }
    }
}
