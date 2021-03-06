using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Domain.Customers.Commands.Result;
using POC.ServerASMX.Domain.Customers.Enums;
using POC.ServerASMX.Test.Tools.Base.Integration;
using POC.ServerASMX.Test.Tools.Constants;
using POC.ServerASMX.Test.Tools.Extensions;
using System;

namespace POC.ServerASMX.Application.Test.Integration.Customers.Services
{
    internal class CustomerServiceTest : IntegrationTest
    {
        private readonly CustomerService _customerService;

        public CustomerServiceTest() => _customerService = new CustomerService();

        [Test]
        public void Add_Success()
        {
            var command = MocksData.CustomerAddCommand;

            var commandResult = _customerService.Add(command);

            var result = (CustomerCommandResult)commandResult.Data;

            TestContext.WriteLine(commandResult.ToJson());
            
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
        public void Add_Invalid_Command_Null()
        {
            var command = (CustomerAddCommand)null;

            var commandResult = _customerService.Add(command);

            TestContext.WriteLine(commandResult.ToJson());
            
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

            TestContext.WriteLine(commandResult.ToJson());
            
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
        public void Add_Invalid_Name(string name)
        {
            var command = MocksData.CustomerAddCommand;
            command.Name = name;

            var commandResult = _customerService.Add(command);

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Add_Invalid_Birth_DateTimeMin()
        {
            var command = MocksData.CustomerAddCommand;
            command.Birth = DateTime.MinValue;

            var commandResult = _customerService.Add(command);

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Add_Invalid_Birth_FutureDate()
        {
            var command = MocksData.CustomerAddCommand;
            command.Birth = DateTime.Now.AddDays(1);

            var commandResult = _customerService.Add(command);

            TestContext.WriteLine(commandResult.ToJson());
            
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
        public void Add_Invalid_Gender(EGender gender)
        {
            var command = MocksData.CustomerAddCommand;
            command.Gender = gender;

            var commandResult = _customerService.Add(command);

            TestContext.WriteLine(commandResult.ToJson());
            
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
        public void Add_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksData.CustomerAddCommand;
            command.CashBalance = cashBalance;

            var commandResult = _customerService.Add(command);

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Update_Success()
        {
            _customerService.Add(MocksData.CustomerAddCommand);

            var command = MocksData.CustomerUpdateCommand;

            var commandResult = _customerService.Update(command);

            var result = (CustomerCommandResult)commandResult.Data;

            TestContext.WriteLine(commandResult.ToJson());
            
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
        public void Update_Invalid_Command_Null()
        {
            var command = (CustomerUpdateCommand)null;

            var commandResult = _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());
            
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

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Update_Invalid_Not_Resgistred_Id()
        {
            var command = MocksData.CustomerUpdateCommand;

            var commandResult = _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());
            
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
        public void Update_Invalid_Id(long id)
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Id = id;

            var commandResult = _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());
            
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
        public void Update_Invalid_Name(string name)
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Name = name;

            var commandResult = _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Update_Invalid_Birth_DateTimeMin()
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Birth = DateTime.MinValue;

            var commandResult = _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Update_Invalid_Birth_FutureDate()
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Birth = DateTime.Now.AddDays(1);

            var commandResult = _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());
            
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
        public void Update_Invalid_Gender(EGender gender)
        {
            var command = MocksData.CustomerUpdateCommand;
            command.Gender = gender;

            var commandResult = _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());
            
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
        public void Update_Invalid_CashBalance(decimal cashBalance)
        {
            var command = MocksData.CustomerUpdateCommand;
            command.CashBalance = cashBalance;

            var commandResult = _customerService.Update(command);

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Activity_State_Success()
        {
            _customerService.Add(MocksData.CustomerAddCommand);

            var command = MocksData.CustomerActivityStateCommand;

            var commandResult = _customerService.ChangeActivityState(command);

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.True);
                Assert.That(commandResult.Message, Is.EqualTo("Customer successfully updated!"));
                Assert.That(commandResult.Errors, Is.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Activity_State_Invalid_Command_Null()
        {
            var command = (CustomerActivityStateCommand)null;

            var commandResult = _customerService.ChangeActivityState(command);

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Activity_State_Invalid_Not_Resgistred_Id()
        {
            var command = MocksData.CustomerActivityStateCommand;

            var commandResult = _customerService.ChangeActivityState(command);

            TestContext.WriteLine(commandResult.ToJson());
            
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
        public void Activity_State_Invalid_Id(long id)
        {
            var command = MocksData.CustomerActivityStateCommand;
            command.Id = id;

            var commandResult = _customerService.ChangeActivityState(command);

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Delete_Success()
        {
            _customerService.Add(MocksData.CustomerAddCommand);

            var command = MocksData.CustomerDeleteCommand;

            var commandResult = _customerService.Delete(command);

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.True);
                Assert.That(commandResult.Message, Is.EqualTo("Customer successfully deleted!"));
                Assert.That(commandResult.Errors, Is.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Delete_Invalid_Command_Null()
        {
            var command = (CustomerDeleteCommand)null;

            var commandResult = _customerService.Delete(command);

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Delete_Invalid_Not_Resgistred_Id()
        {
            var command = MocksData.CustomerDeleteCommand;

            var commandResult = _customerService.Delete(command);

            TestContext.WriteLine(commandResult.ToJson());
            
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
        public void Delete_Invalid_Id(long id)
        {
            var command = MocksData.CustomerDeleteCommand;
            command.Id = id;

            var commandResult = _customerService.Delete(command);

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Success, Is.False);
                Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                Assert.That(commandResult.Errors, Is.Not.Empty);
                Assert.That(commandResult.Data, Is.Null);
            });
        }

        [Test]
        public void Get_Registred_Id_Success()
        {
            var command = MocksData.CustomerAddCommand;

            _customerService.Add(command);

            var result = _customerService.Get(1);

            TestContext.WriteLine(result.ToJson());
            
            Assert.Multiple(() =>
            {
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
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(long.MaxValue)]
        public void Get_Not_Registred_Id_Success(long id)
        {
            var result = _customerService.Get(id);

            TestContext.WriteLine(result.ToJson());

            Assert.That(result, Is.Null);
        }

        [Test]
        public void List_Registred_Ids_Success()
        {
            var command = MocksData.CustomerAddCommand;

            _customerService.Add(command);

            var result = _customerService.List();

            TestContext.WriteLine(result.ToJson());

            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0].Id, Is.EqualTo(1));
                Assert.That(result[0].Name, Is.EqualTo(command.Name));
                Assert.That(result[0].Birth, Is.EqualTo(command.Birth));
                Assert.That(result[0].Gender, Is.EqualTo(command.Gender));
                Assert.That(result[0].CashBalance, Is.EqualTo(command.CashBalance));
                Assert.That(result[0].Active, Is.True);
                Assert.That(result[0].CreationDate.Date, Is.EqualTo(DateTime.Now.Date));
                Assert.That(result[0].ChangeDate, Is.Null);
            });
        }

        [Test]
        public void List_Not_Registred_Ids_Success()
        {
            var result = _customerService.List();

            TestContext.WriteLine(result.ToJson());

            Assert.That(result, Is.Empty);
        }
    }
}