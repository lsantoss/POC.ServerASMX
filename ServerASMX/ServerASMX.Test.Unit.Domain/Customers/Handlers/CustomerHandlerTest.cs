using NUnit.Framework;
using ServerASMX.Domain.Customers.Commands.Input;
using ServerASMX.Domain.Customers.Commands.Output;
using ServerASMX.Domain.Customers.Enums;
using ServerASMX.Domain.Customers.Handlers;
using ServerASMX.Domain.Customers.Interfaces.Handlers;
using ServerASMX.Domain.Customers.Interfaces.Repositories;
using ServerASMX.Domain.Customers.Repositories;
using ServerASMX.Test.Base.Base;
using ServerASMX.Test.Base.Constants;
using ServerASMX.Test.Base.Extensions;
using System;

namespace ServerASMX.Test.Unit.Domain.Customers.Handlers
{
    internal class CustomerHandlerTest : DatabaseTest
    {
        private readonly ICustomerHandler _handler;
        private readonly ICustomerRepository _repository;

        public CustomerHandlerTest()
        {
            _handler = new CustomerHandler();
            _repository = new CustomerRepository();
        }

        [Test]
        public void Handle_Add_Success()
        {
            var command = MocksTest.CustomerAddCommand;

            var commandResult = _handler.Handle(command);

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
        public void Handle_Add_Invalid_Command_Null()
        {
            var command = (CustomerAddCommand)null;

            var commandResult = _handler.Handle(command);

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
        public void Handle_Add_Invalid_Command(string name, EGender gender, decimal cashBalance)
        {
            var command = new CustomerAddCommand
            {
                Name = name,
                Birth = DateTime.MinValue,
                Gender = gender,
                CashBalance = cashBalance
            };

            var commandResult = _handler.Handle(command);

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
        public void Handle_Add_Invalid_Name(string name)
        {
            var command = MocksTest.CustomerAddCommand;
            command.Name = name;

            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public void Handle_Add_Invalid_Birth_DateTimeMin()
        {
            var command = MocksTest.CustomerAddCommand;
            command.Birth = DateTime.MinValue;

            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        public void Handle_Add_Invalid_Birth_FutureDate()
        {
            var command = MocksTest.CustomerAddCommand;
            command.Birth = DateTime.Now.AddDays(1);

            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1)]
        public void Handle_Add_Invalid_Gender(EGender gender)
        {
            var command = MocksTest.CustomerAddCommand;
            command.Gender = gender;

            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }

        [Test]
        [TestCase(-1)]
        public void Handle_Add_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksTest.CustomerAddCommand;
            command.CashBalance = cashBalance;

            var commandResult = _handler.Handle(command);

            TestContext.WriteLine(commandResult.Format());

            Assert.False(commandResult.Success);
            Assert.AreEqual("Invalid parameters", commandResult.Message);
            Assert.AreNotEqual(0, commandResult.Errors.Count);
            Assert.Null(commandResult.Data);
        }
    }
}
