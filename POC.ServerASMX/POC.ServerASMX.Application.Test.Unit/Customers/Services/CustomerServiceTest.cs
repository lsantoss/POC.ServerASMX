using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Domain.Customers.Commands.Output;
using POC.ServerASMX.Domain.Customers.Enums;
using POC.ServerASMX.Test.Base.Base;
using POC.ServerASMX.Test.Base.Constants;
using POC.ServerASMX.Test.Base.Extensions;
using System;

namespace POC.ServerASMX.Application.Test.Unit.Customers.Services
{
    internal class CustomerServiceTest : DatabaseUnitTest
    {
        private readonly CustomerService _customerService;

        public CustomerServiceTest() => _customerService = new CustomerService();

        [Test]
        public void Add_Success()
        {
            var command = MocksUnitTest.CustomerAddCommand;

            var commandResult = _customerService.Add(command);

            var result = (CustomerCommandOutput)commandResult.Data;

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
        public void Add_Invalid_Command_Null()
        {
            var command = (CustomerAddCommand)null;

            var commandResult = _customerService.Add(command);

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
        public void Add_Invalid_Command(string name, EGender gender, decimal cashBalance)
        {
            var command = new CustomerAddCommand
            {
                Name = name,
                Birth = DateTime.MinValue,
                Gender = gender,
                CashBalance = cashBalance
            };

            var commandResult = _customerService.Add(command);

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
        public void Add_Invalid_Name(string name)
        {
            var command = MocksUnitTest.CustomerAddCommand;
            command.Name = name;

            var commandResult = _customerService.Add(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public void Add_Invalid_Birth_DateTimeMin()
        {
            var command = MocksUnitTest.CustomerAddCommand;
            command.Birth = DateTime.MinValue;

            var commandResult = _customerService.Add(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public void Add_Invalid_Birth_FutureDate()
        {
            var command = MocksUnitTest.CustomerAddCommand;
            command.Birth = DateTime.Now.AddDays(1);

            var commandResult = _customerService.Add(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1)]
        public void Add_Invalid_Gender(EGender gender)
        {
            var command = MocksUnitTest.CustomerAddCommand;
            command.Gender = gender;

            var commandResult = _customerService.Add(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1)]
        public void Add_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksUnitTest.CustomerAddCommand;
            command.CashBalance = cashBalance;

            var commandResult = _customerService.Add(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public void Update_Success()
        {
            _customerService.Add(MocksUnitTest.CustomerAddCommand);

            var command = MocksUnitTest.CustomerUpdateCommand;

            var commandResult = _customerService.Update(command);

            var result = (CustomerCommandOutput)commandResult.Data;

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
        public void Update_Invalid_Command_Null()
        {
            var command = (CustomerUpdateCommand)null;

            var commandResult = _customerService.Update(command);

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
        public void Update_Invalid_Command(long id, string name, EGender gender, decimal cashBalance)
        {
            var command = new CustomerUpdateCommand
            {
                Id = id,
                Name = name,
                Birth = DateTime.MinValue,
                Gender = gender,
                CashBalance = cashBalance
            };

            var commandResult = _customerService.Update(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public void Update_Invalid_Not_Resgistred_Id()
        {
            var command = MocksUnitTest.CustomerUpdateCommand;

            var commandResult = _customerService.Update(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Inconsistencies in the data", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Update_Invalid_Id(long id)
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.Id = id;

            var commandResult = _customerService.Update(command);

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
        public void Update_Invalid_Name(string name)
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.Name = name;

            var commandResult = _customerService.Update(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public void Update_Invalid_Birth_DateTimeMin()
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.Birth = DateTime.MinValue;

            var commandResult = _customerService.Update(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public void Update_Invalid_Birth_FutureDate()
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.Birth = DateTime.Now.AddDays(1);

            var commandResult = _customerService.Update(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1)]
        public void Update_Invalid_Gender(EGender gender)
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.Gender = gender;

            var commandResult = _customerService.Update(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1)]
        public void Update_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksUnitTest.CustomerUpdateCommand;
            command.CashBalance = cashBalance;

            var commandResult = _customerService.Update(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public void Activity_State_Success()
        {
            _customerService.Add(MocksUnitTest.CustomerAddCommand);

            var command = MocksUnitTest.CustomerActivityStateCommand;

            var commandResult = _customerService.ChangeActivityState(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.True(commandResult.Success);
            Assert.AreEqual("Customer successfully updated!", commandResult.Message);
            Assert.AreEqual(0, commandResult.Errors.Count);
            Assert.IsNull(commandResult.Data);
        }

        [Test]
        public void Activity_State_Invalid_Command_Null()
        {
            var command = (CustomerActivityStateCommand)null;

            var commandResult = _customerService.ChangeActivityState(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public void Activity_State_Invalid_Not_Resgistred_Id()
        {
            var command = MocksUnitTest.CustomerActivityStateCommand;

            var commandResult = _customerService.ChangeActivityState(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Inconsistencies in the data", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Activity_State_Invalid_Id(long id)
        {
            var command = MocksUnitTest.CustomerActivityStateCommand;
            command.Id = id;

            var commandResult = _customerService.ChangeActivityState(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public void Delete_Success()
        {
            _customerService.Add(MocksUnitTest.CustomerAddCommand);

            var command = MocksUnitTest.CustomerDeleteCommand;

            var commandResult = _customerService.Delete(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.True(commandResult.Success);
            Assert.AreEqual("Customer successfully deleted!", commandResult.Message);
            Assert.AreEqual(0, commandResult.Errors.Count);
            Assert.IsNull(commandResult.Data);
        }

        [Test]
        public void Delete_Invalid_Command_Null()
        {
            var command = (CustomerDeleteCommand)null;

            var commandResult = _customerService.Delete(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public void Delete_Invalid_Not_Resgistred_Id()
        {
            var command = MocksUnitTest.CustomerDeleteCommand;

            var commandResult = _customerService.Delete(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Inconsistencies in the data", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Delete_Invalid_Id(long id)
        {
            var command = MocksUnitTest.CustomerDeleteCommand;
            command.Id = id;

            var commandResult = _customerService.Delete(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public void Get_Registred_Id_Success()
        {
            var command = MocksUnitTest.CustomerAddCommand;

            _customerService.Add(command);

            var result = _customerService.Get(1);

            TestContext.WriteLine(result.Format());

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
        public void Get_Not_Registred_Id_Success(long id)
        {
            var result = _customerService.Get(id);

            TestContext.WriteLine(result.Format());

            Assert.IsNull(result);
        }

        [Test]
        public void List_Registred_Ids_Success()
        {
            var command = MocksUnitTest.CustomerAddCommand;

            _customerService.Add(command);

            var result = _customerService.List();

            TestContext.WriteLine(result.Format());

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
        public void List_Not_Registred_Ids_Success()
        {
            var result = _customerService.List();

            TestContext.WriteLine(result.Format());

            Assert.AreEqual(0, result.Count);
        }
    }
}
