using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Domain.Customers.Commands.Result;
using POC.ServerASMX.Domain.Customers.Enums;
using POC.ServerASMX.Test.Base.Constants;
using POC.ServerASMX.Test.Base.Extensions;
using POC.ServerASMX.Test.Tools.Base.Integration;
using System;
using System.Threading.Tasks;

namespace POC.ServerASMX.Application.Test.Unit.Customers.Services
{
    internal class CustomerServiceTest : IntegrationTest
    {
        private readonly CustomerService _customerService;

        public CustomerServiceTest() => _customerService = new CustomerService();

        [Test]
        public async Task Add_Success()
        {
            var command = MocksData.CustomerAddCommand;

            var commandResult = await _customerService.Add(command);

            var result = (CustomerCommandResult)commandResult.Data;

            TestContext.WriteLine(commandResult.ToJson());

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
        public async Task Add_Invalid_Command_Null()
        {
            var command = (CustomerAddCommand)null;

            var commandResult = await _customerService.Add(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(null, -1, -1)]
        [TestCase("", -1, -1)]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters, -1, -1)]
        public async Task Add_Invalid_Command(string name, EGender gender, decimal cashBalance)
        {
            var command = new CustomerAddCommand
            {
                Name = name,
                Birth = DateTime.MinValue,
                Gender = gender,
                CashBalance = cashBalance
            };

            var commandResult = await _customerService.Add(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public async Task Add_Invalid_Name(string name)
        {
            var command = MocksData.CustomerAddCommand;
            command.Name = name;

            var commandResult = await _customerService.Add(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task Add_Invalid_Birth_DateTimeMin()
        {
            var command = MocksData.CustomerAddCommand;
            command.Birth = DateTime.MinValue;

            var commandResult = await _customerService.Add(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task Add_Invalid_Birth_FutureDate()
        {
            var command = MocksData.CustomerAddCommand;
            command.Birth = DateTime.Now.AddDays(1);

            var commandResult = await _customerService.Add(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1)]
        public async Task Add_Invalid_Gender(EGender gender)
        {
            var command = MocksData.CustomerAddCommand;
            command.Gender = gender;

            var commandResult = await _customerService.Add(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1)]
        public async Task Add_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksData.CustomerAddCommand;
            command.CashBalance = cashBalance;

            var commandResult = await _customerService.Add(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task Update_Success()
        {
            await _customerService.Add(MocksData.CustomerAddCommand);

            var command = MocksData.CustomerUpdateCommand;

            var commandResult = await _customerService.Update(command);

            var result = (CustomerCommandResult)commandResult.Data;

            TestContext.WriteLine(commandResult.ToJson());

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
        public async Task Update_Invalid_Command_Null()
        {
            var command = (CustomerUpdateCommand)null;

            var commandResult = await _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1, null, -1, -1)]
        [TestCase(-1, "", -1, -1)]
        [TestCase(0, StringsWithPredefinedSizes.StringWith101Caracters, -1, -1)]
        public async Task Update_Invalid_Command(long id, string name, EGender gender, decimal cashBalance)
        {
            var command = new CustomerUpdateCommand
            {
                Id = id,
                Name = name,
                Birth = DateTime.MinValue,
                Gender = gender,
                CashBalance = cashBalance
            };

            var commandResult = await _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task Update_Invalid_Not_Resgistred_Id()
        {
            var command = MocksData.CustomerUpdateCommand;

            var commandResult = await _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Inconsistencies in the data", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task Update_Invalid_Id(long id)
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Id = id;

            var commandResult = await _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public async Task Update_Invalid_Name(string name)
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Name = name;

            var commandResult = await _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task Update_Invalid_Birth_DateTimeMin()
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Birth = DateTime.MinValue;

            var commandResult = await _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task Update_Invalid_Birth_FutureDate()
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Birth = DateTime.Now.AddDays(1);

            var commandResult = await _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1)]
        public async Task Update_Invalid_Gender(EGender gender)
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Gender = gender;

            var commandResult = await _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1)]
        public async Task Update_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksData.CustomerUpdateCommand;
            command.CashBalance = cashBalance;

            var commandResult = await _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task Activity_State_Success()
        {
            await _customerService.Add(MocksData.CustomerAddCommand);

            var command = MocksData.CustomerActivityStateCommand;

            var commandResult = await _customerService.ChangeActivityState(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.True(commandResult.Success);
            Assert.AreEqual("Customer successfully updated!", commandResult.Message);
            Assert.AreEqual(0, commandResult.Errors.Count);
            Assert.IsNull(commandResult.Data);
        }

        [Test]
        public async Task Activity_State_Invalid_Command_Null()
        {
            var command = (CustomerActivityStateCommand)null;

            var commandResult = await _customerService.ChangeActivityState(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task Activity_State_Invalid_Not_Resgistred_Id()
        {
            var command = MocksData.CustomerActivityStateCommand;

            var commandResult = await _customerService.ChangeActivityState(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Inconsistencies in the data", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task Activity_State_Invalid_Id(long id)
        {
            var command = MocksData.CustomerActivityStateCommand;
            command.Id = id;

            var commandResult = await _customerService.ChangeActivityState(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task Delete_Success()
        {
            await _customerService.Add(MocksData.CustomerAddCommand);

            var command = MocksData.CustomerDeleteCommand;

            var commandResult = await _customerService.Delete(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.True(commandResult.Success);
            Assert.AreEqual("Customer successfully deleted!", commandResult.Message);
            Assert.AreEqual(0, commandResult.Errors.Count);
            Assert.IsNull(commandResult.Data);
        }

        [Test]
        public async Task Delete_Invalid_Command_Null()
        {
            var command = (CustomerDeleteCommand)null;

            var commandResult = await _customerService.Delete(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task Delete_Invalid_Not_Resgistred_Id()
        {
            var command = MocksData.CustomerDeleteCommand;

            var commandResult = await _customerService.Delete(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Inconsistencies in the data", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task Delete_Invalid_Id(long id)
        {
            var command = MocksData.CustomerDeleteCommand;
            command.Id = id;

            var commandResult = await _customerService.Delete(command);

            TestContext.WriteLine(commandResult.ToJson());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public async Task Get_Registred_Id_Success()
        {
            var command = MocksData.CustomerAddCommand;

            await _customerService.Add(command);

            var result = await _customerService.Get(1);

            TestContext.WriteLine(result.ToJson());

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
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(long.MaxValue)]
        public async Task Get_Not_Registred_Id_Success(long id)
        {
            var result = await _customerService.Get(id);

            TestContext.WriteLine(result.ToJson());

            Assert.IsNull(result);
        }

        [Test]
        public async Task List_Registred_Ids_Success()
        {
            var command = MocksData.CustomerAddCommand;

            await _customerService.Add(command);

            var result = await _customerService.List();

            TestContext.WriteLine(result.ToJson());

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(command.Name, result[0].Name);
            Assert.AreEqual(command.Birth, result[0].Birth);
            Assert.AreEqual(command.Gender, result[0].Gender);
            Assert.AreEqual(command.CashBalance, result[0].CashBalance);
            Assert.IsTrue(result[0].Active);
            Assert.AreEqual(DateTime.Now.Date, result[0].CreationDate.Date);
            Assert.IsNull(result[0].ChangeDate);
        }

        [Test]
        public async Task List_Not_Registred_Ids_Success()
        {
            var result = await _customerService.List();

            TestContext.WriteLine(result.ToJson());

            Assert.AreEqual(0, result.Count);
        }
    }
}
