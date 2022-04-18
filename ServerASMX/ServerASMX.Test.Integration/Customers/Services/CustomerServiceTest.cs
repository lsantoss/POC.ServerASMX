using NUnit.Framework;
using ServerASMX.Test.Base.Constants;
using ServerASMX.Test.Base.Extensions;
using ServerASMX.Test.Integration.CustomerService;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ServerASMX.Test.Integration.Customers.Services
{
    internal class CustomerServiceTest
    {
        private readonly CustomerServiceSoapClient _customerServiceSoapClient;

        public CustomerServiceTest()
        {
            var endpointConfigurationName = ConfigurationManager.AppSettings["EndpointConfigurationName"];
            _customerServiceSoapClient = new CustomerServiceSoapClient(endpointConfigurationName);
        }

        private CustomerAddCommand CustomerAddCommand => new CustomerAddCommand()
        {
            Name = "Lucas Santos",
            Birth = new DateTime(1995, 7, 14),
            Gender = EGender.Male,
            CashBalance = 1500.75m
        };

        private CustomerUpdateCommand CustomerUpdateCommand => new CustomerUpdateCommand()
        {
            Id = 0,
            Name = "Lucas S.",
            Birth = new DateTime(1996, 3, 10),
            Gender = EGender.Male,
            CashBalance = 2200.33m
        };

        private CustomerActivityStateCommand CustomerActivityStateCommand => new CustomerActivityStateCommand()
        {
            Id = 0,
            Active = false
        };

        private CustomerDeleteCommand CustomerDeleteCommand => new CustomerDeleteCommand()
        {
            Id = 0
        };

        [Test]
        public async Task Add_Success()
        {
            try
            {
                var command = CustomerAddCommand;

                var response = await _customerServiceSoapClient.AddAsync(command);

                var commandResult = response?.Body?.AddResult;

                var result = (CustomerCommandOutput)commandResult.Data;

                TestContext.WriteLine(commandResult.Format());

                Assert.True(commandResult.Success);
                Assert.AreEqual("Customer successfully inserted!", commandResult.Message);
                Assert.AreEqual(0, commandResult.Errors.Count);
                Assert.AreEqual(command.Name, result.Name);
                Assert.AreEqual(command.Birth, result.Birth);
                Assert.AreEqual(command.Gender, result.Gender);
                Assert.AreEqual(command.CashBalance, result.CashBalance);
                Assert.IsTrue(result.Active);
                Assert.AreEqual(DateTime.Now.Date, result.CreationDate.Date);
                Assert.IsNull(result.ChangeDate);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }

        }

        [Test]
        public async Task Add_Invalid_Command_Null()
        {
            try
            {
                var command = (CustomerAddCommand)null;

                var response = await _customerServiceSoapClient.AddAsync(command);

                var commandResult = response?.Body?.AddResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        [TestCase(null, -1)]
        [TestCase("", -1)]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters, -1)]
        public async Task Add_Invalid_Command(string name, decimal cashBalance)
        {
            try
            {
                var command = new CustomerAddCommand
                {
                    Name = name,
                    Birth = DateTime.MinValue,
                    Gender = EGender.Male,
                    CashBalance = cashBalance
                };

                var response = await _customerServiceSoapClient.AddAsync(command);

                var commandResult = response?.Body?.AddResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public async Task Add_Invalid_Name(string name)
        {
            try
            {
                var command = CustomerAddCommand;
                command.Name = name;

                var response = await _customerServiceSoapClient.AddAsync(command);

                var commandResult = response?.Body?.AddResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Add_Invalid_Birth_DateTimeMin()
        {
            try
            {
                var command = CustomerAddCommand;
                command.Birth = DateTime.MinValue;

                var response = await _customerServiceSoapClient.AddAsync(command);

                var commandResult = response?.Body?.AddResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Add_Invalid_Birth_FutureDate()
        {
            try
            {
                var command = CustomerAddCommand;
                command.Birth = DateTime.Now.AddDays(1);

                var response = await _customerServiceSoapClient.AddAsync(command);

                var commandResult = response?.Body?.AddResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        [TestCase(-1)]
        public async Task Add_Invalid_CashBalance(decimal cashBalance)
        {
            try
            {
                var command = CustomerAddCommand;
                command.CashBalance = cashBalance;

                var response = await _customerServiceSoapClient.AddAsync(command);

                var commandResult = response?.Body?.AddResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Update_Success()
        {
            try
            {
                var responseAdd = await _customerServiceSoapClient.AddAsync(CustomerAddCommand);
                var idAdd = ((CustomerCommandOutput)responseAdd?.Body?.AddResult?.Data).Id;

                var command = CustomerUpdateCommand;
                command.Id = idAdd;

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

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
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Update_Invalid_Command_Null()
        {
            try
            {
                var command = (CustomerUpdateCommand)null;

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        [TestCase(-1, null, -1)]
        [TestCase(-1, "", -1)]
        [TestCase(0, StringsWithPredefinedSizes.StringWith101Caracters, -1)]
        public async Task Update_Invalid_Command(long id, string name, decimal cashBalance)
        {
            try
            {
                var command = CustomerUpdateCommand;
                command.Id = id;
                command.Name = name;
                command.Birth = DateTime.MinValue;
                command.Gender = EGender.Male;
                command.CashBalance = cashBalance;

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        [TestCase(long.MaxValue)]
        public async Task Update_Invalid_Not_Resgistred_Id(long id)
        {
            try
            {
                var command = CustomerUpdateCommand;
                command.Id = id;

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Inconsistencies in the data", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task Update_Invalid_Id(long id)
        {
            try
            {
                var command = CustomerUpdateCommand;
                command.Id = id;

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public async Task Update_Invalid_Name(string name)
        {
            try
            {
                var command = CustomerUpdateCommand;
                command.Name = name;

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Update_Invalid_Birth_DateTimeMin()
        {
            try
            {
                var command = CustomerUpdateCommand;
                command.Birth = DateTime.MinValue;

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Update_Invalid_Birth_FutureDate()
        {
            try
            {
                var command = CustomerUpdateCommand;
                command.Birth = DateTime.Now.AddDays(1);

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        [TestCase(-1)]
        public async Task Update_Invalid_CashBalance(decimal cashBalance)
        {
            try
            {
                var command = CustomerUpdateCommand;
                command.CashBalance = cashBalance;

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Activity_State_Success()
        {
            try
            {
                var responseAdd = await _customerServiceSoapClient.AddAsync(CustomerAddCommand);
                var idAdd = ((CustomerCommandOutput)responseAdd?.Body?.AddResult?.Data).Id;

                var command = CustomerActivityStateCommand;
                command.Id = idAdd;

                var response = await _customerServiceSoapClient.ChangeActivityStateAsync(command);

                var commandResult = response?.Body?.ChangeActivityStateResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.True(commandResult.Success);
                Assert.AreEqual("Customer successfully updated!", commandResult.Message);
                Assert.AreEqual(0, commandResult.Errors.Count);
                Assert.IsNull(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Activity_State_Invalid_Command_Null()
        {
            try
            {
                var command = (CustomerActivityStateCommand)null;

                var response = await _customerServiceSoapClient.ChangeActivityStateAsync(command);

                var commandResult = response?.Body?.ChangeActivityStateResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        [TestCase(long.MaxValue)]
        public async Task Activity_State_Invalid_Not_Resgistred_Id(long id)
        {
            try
            {
                var command = CustomerActivityStateCommand;
                command.Id = id;

                var response = await _customerServiceSoapClient.ChangeActivityStateAsync(command);

                var commandResult = response?.Body?.ChangeActivityStateResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Inconsistencies in the data", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task Activity_State_Invalid_Id(long id)
        {
            try
            {
                var command = CustomerActivityStateCommand;
                command.Id = id;

                var response = await _customerServiceSoapClient.ChangeActivityStateAsync(command);

                var commandResult = response?.Body?.ChangeActivityStateResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Delete_Success()
        {
            try
            {
                var responseAdd = await _customerServiceSoapClient.AddAsync(CustomerAddCommand);
                var idAdd = ((CustomerCommandOutput)responseAdd?.Body?.AddResult?.Data).Id;

                var command = CustomerDeleteCommand;
                command.Id = idAdd;

                var response = await _customerServiceSoapClient.DeleteAsync(command);

                var commandResult = response?.Body?.DeleteResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.True(commandResult.Success);
                Assert.AreEqual("Customer successfully deleted!", commandResult.Message);
                Assert.AreEqual(0, commandResult.Errors.Count);
                Assert.IsNull(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Delete_Invalid_Command_Null()
        {
            try
            {
                var command = (CustomerDeleteCommand)null;

                var response = await _customerServiceSoapClient.DeleteAsync(command);

                var commandResult = response?.Body?.DeleteResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        [TestCase(long.MaxValue)]
        public async Task Delete_Invalid_Not_Resgistred_Id(long id)
        {
            try
            {
                var command = CustomerDeleteCommand;
                command.Id = id;

                var response = await _customerServiceSoapClient.DeleteAsync(command);

                var commandResult = response?.Body?.DeleteResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Inconsistencies in the data", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task Delete_Invalid_Id(long id)
        {
            try
            {
                var command = CustomerDeleteCommand;
                command.Id = id;

                var response = await _customerServiceSoapClient.DeleteAsync(command);

                var commandResult = response?.Body?.DeleteResult;

                TestContext.WriteLine(commandResult.Format());

                Assert.False(commandResult.Success);
                Assert.AreEqual("Invalid parameters", commandResult.Message);
                Assert.AreNotEqual(0, commandResult.Errors.Count);
                Assert.Null(commandResult.Data);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Get_Registred_Id_Success()
        {
            try
            {
                var command = CustomerAddCommand;

                var responseAdd = await _customerServiceSoapClient.AddAsync(CustomerAddCommand);
                var idAdd = ((CustomerCommandOutput)responseAdd?.Body?.AddResult?.Data).Id;

                var response = await _customerServiceSoapClient.GetAsync(idAdd);

                var result = response?.Body?.GetResult;

                TestContext.WriteLine(result.Format());

                Assert.AreEqual(idAdd, result.Id);
                Assert.AreEqual(command.Name, result.Name);
                Assert.AreEqual(command.Birth, result.Birth);
                Assert.AreEqual(command.Gender, result.Gender);
                Assert.AreEqual(command.CashBalance, result.CashBalance);
                Assert.IsTrue(result.Active);
                Assert.AreEqual(DateTime.Now.Date, result.CreationDate.Date);
                Assert.IsNull(result.ChangeDate);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(long.MaxValue)]
        public async Task Get_Not_Registred_Id_Success(long id)
        {
            try
            {
                var response = await _customerServiceSoapClient.GetAsync(id);

                var result = response?.Body?.GetResult;

                TestContext.WriteLine(result.Format());

                Assert.IsNull(result);
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task List_Success()
        {
            try
            {
                var response = await _customerServiceSoapClient.ListAsync();

                var result = response?.Body?.ListResult;

                TestContext.WriteLine(result.Format());

                Assert.Pass();
            }
            catch (Exception e)
            {
                Assert.Inconclusive(e.Message);
            }
        }
    }
}