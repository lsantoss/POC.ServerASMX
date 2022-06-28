using NUnit.Framework;
using POC.ServerASMX.Test.Tools.Base.Contract;
using POC.ServerASMX.Test.Tools.Constants;
using POC.ServerASMX.Test.Tools.CustomerService;
using POC.ServerASMX.Test.Tools.Extensions;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace POC.ServerASMX.Application.Test.Contract.Customers.Services
{
    internal class CustomerServiceTest : ContractTest
    {
        private readonly CustomerServiceSoapClient _customerServiceSoapClient;

        public CustomerServiceTest() => _customerServiceSoapClient = new CustomerServiceSoapClient("CustomerServiceSoap");

        [Test]
        public async Task Add_Success()
        {
            try
            {
                var command = new CustomerAddCommand()
                {
                    Name = MocksData.CustomerAddCommand.Name,
                    Birth = MocksData.CustomerAddCommand.Birth,
                    Gender = (EGender)MocksData.CustomerAddCommand.Gender,
                    CashBalance = MocksData.CustomerAddCommand.CashBalance
                };

                var response = await _customerServiceSoapClient.AddAsync(command);

                var commandResult = response?.Body?.AddResult;

                var result = (CustomerCommandResult)commandResult.Data;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.True);
                    Assert.That(commandResult.Message, Is.EqualTo("Customer successfully inserted!"));
                    Assert.That(commandResult.Errors, Is.Empty);
                    Assert.That(result.Name, Is.EqualTo(command.Name));
                    Assert.That(result.Birth, Is.EqualTo(command.Birth));
                    Assert.That(result.Gender, Is.EqualTo(command.Gender));
                    Assert.That(result.CashBalance, Is.EqualTo(command.CashBalance));
                    Assert.That(result.Active, Is.True);
                    Assert.That(result.CreationDate.Date, Is.EqualTo(DateTime.Now.Date));
                    Assert.That(result.ChangeDate, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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
                var command = new CustomerAddCommand()
                {
                    Name = name,
                    Birth = MocksData.CustomerAddCommand.Birth,
                    Gender = (EGender)MocksData.CustomerAddCommand.Gender,
                    CashBalance = MocksData.CustomerAddCommand.CashBalance
                };

                var response = await _customerServiceSoapClient.AddAsync(command);

                var commandResult = response?.Body?.AddResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Add_Invalid_Birth_DateTimeMin()
        {
            try
            {
                var command = new CustomerAddCommand()
                {
                    Name = MocksData.CustomerAddCommand.Name,
                    Birth = DateTime.MinValue,
                    Gender = (EGender)MocksData.CustomerAddCommand.Gender,
                    CashBalance = MocksData.CustomerAddCommand.CashBalance
                };

                var response = await _customerServiceSoapClient.AddAsync(command);

                var commandResult = response?.Body?.AddResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Add_Invalid_Birth_FutureDate()
        {
            try
            {
                var command = new CustomerAddCommand()
                {
                    Name = MocksData.CustomerAddCommand.Name,
                    Birth = DateTime.Now.AddDays(1),
                    Gender = (EGender)MocksData.CustomerAddCommand.Gender,
                    CashBalance = MocksData.CustomerAddCommand.CashBalance
                };

                var response = await _customerServiceSoapClient.AddAsync(command);

                var commandResult = response?.Body?.AddResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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
                var command = new CustomerAddCommand()
                {
                    Name = MocksData.CustomerAddCommand.Name,
                    Birth = MocksData.CustomerAddCommand.Birth,
                    Gender = (EGender)MocksData.CustomerAddCommand.Gender,
                    CashBalance = cashBalance
                };

                var response = await _customerServiceSoapClient.AddAsync(command);

                var commandResult = response?.Body?.AddResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Update_Success()
        {
            try
            {
                var commandAdd = new CustomerAddCommand()
                {
                    Name = MocksData.CustomerAddCommand.Name,
                    Birth = MocksData.CustomerAddCommand.Birth,
                    Gender = (EGender)MocksData.CustomerAddCommand.Gender,
                    CashBalance = MocksData.CustomerAddCommand.CashBalance
                };

                var responseAdd = await _customerServiceSoapClient.AddAsync(commandAdd);
                var idAdd = ((CustomerCommandResult)responseAdd?.Body?.AddResult?.Data).Id;

                var command = new CustomerUpdateCommand()
                {
                    Id = idAdd,
                    Name = MocksData.CustomerUpdateCommand.Name,
                    Birth = MocksData.CustomerUpdateCommand.Birth,
                    Gender = (EGender)MocksData.CustomerUpdateCommand.Gender,
                    CashBalance = MocksData.CustomerUpdateCommand.CashBalance
                };

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

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
            catch (EndpointNotFoundException e)
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

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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
                var command = new CustomerUpdateCommand()
                {
                    Id = id,
                    Name = name,
                    Birth = DateTime.MinValue,
                    Gender = EGender.Male,
                    CashBalance = cashBalance
                };

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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
                var command = new CustomerUpdateCommand()
                {
                    Id = id,
                    Name = MocksData.CustomerUpdateCommand.Name,
                    Birth = MocksData.CustomerUpdateCommand.Birth,
                    Gender = (EGender)MocksData.CustomerUpdateCommand.Gender,
                    CashBalance = MocksData.CustomerUpdateCommand.CashBalance
                };

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Inconsistencies in the data"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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
                var command = new CustomerUpdateCommand()
                {
                    Id = id,
                    Name = MocksData.CustomerUpdateCommand.Name,
                    Birth = MocksData.CustomerUpdateCommand.Birth,
                    Gender = (EGender)MocksData.CustomerUpdateCommand.Gender,
                    CashBalance = MocksData.CustomerUpdateCommand.CashBalance
                };

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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
                var command = new CustomerUpdateCommand()
                {
                    Id = MocksData.CustomerUpdateCommand.Id,
                    Name = name,
                    Birth = MocksData.CustomerUpdateCommand.Birth,
                    Gender = (EGender)MocksData.CustomerUpdateCommand.Gender,
                    CashBalance = MocksData.CustomerUpdateCommand.CashBalance
                };

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Update_Invalid_Birth_DateTimeMin()
        {
            try
            {
                var command = new CustomerUpdateCommand()
                {
                    Id = MocksData.CustomerUpdateCommand.Id,
                    Name = MocksData.CustomerUpdateCommand.Name,
                    Birth = DateTime.MinValue,
                    Gender = (EGender)MocksData.CustomerUpdateCommand.Gender,
                    CashBalance = MocksData.CustomerUpdateCommand.CashBalance
                };

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Update_Invalid_Birth_FutureDate()
        {
            try
            {
                var command = new CustomerUpdateCommand()
                {
                    Id = MocksData.CustomerUpdateCommand.Id,
                    Name = MocksData.CustomerUpdateCommand.Name,
                    Birth = DateTime.Now.AddDays(1),
                    Gender = (EGender)MocksData.CustomerUpdateCommand.Gender,
                    CashBalance = MocksData.CustomerUpdateCommand.CashBalance
                };

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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
                var command = new CustomerUpdateCommand()
                {
                    Id = MocksData.CustomerUpdateCommand.Id,
                    Name = MocksData.CustomerUpdateCommand.Name,
                    Birth = MocksData.CustomerUpdateCommand.Birth,
                    Gender = (EGender)MocksData.CustomerUpdateCommand.Gender,
                    CashBalance = cashBalance
                };

                var response = await _customerServiceSoapClient.UpdateAsync(command);

                var commandResult = response?.Body?.UpdateResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Activity_State_Success()
        {
            try
            {
                var commandAdd = new CustomerAddCommand()
                {
                    Name = MocksData.CustomerAddCommand.Name,
                    Birth = MocksData.CustomerAddCommand.Birth,
                    Gender = (EGender)MocksData.CustomerAddCommand.Gender,
                    CashBalance = MocksData.CustomerAddCommand.CashBalance
                };

                var responseAdd = await _customerServiceSoapClient.AddAsync(commandAdd);
                var idAdd = ((CustomerCommandResult)responseAdd?.Body?.AddResult?.Data).Id;

                var command = new CustomerActivityStateCommand()
                {
                    Id = idAdd,
                    Active = MocksData.CustomerActivityStateCommand.Active
                };

                var response = await _customerServiceSoapClient.ChangeActivityStateAsync(command);

                var commandResult = response?.Body?.ChangeActivityStateResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.True);
                    Assert.That(commandResult.Message, Is.EqualTo("Customer successfully updated!"));
                    Assert.That(commandResult.Errors, Is.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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
                var command = new CustomerActivityStateCommand()
                {
                    Id = id,
                    Active = MocksData.CustomerActivityStateCommand.Active
                };

                var response = await _customerServiceSoapClient.ChangeActivityStateAsync(command);

                var commandResult = response?.Body?.ChangeActivityStateResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Inconsistencies in the data"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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
                var command = new CustomerActivityStateCommand()
                {
                    Id = id,
                    Active = MocksData.CustomerActivityStateCommand.Active
                };

                var response = await _customerServiceSoapClient.ChangeActivityStateAsync(command);

                var commandResult = response?.Body?.ChangeActivityStateResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Delete_Success()
        {
            try
            {
                var commandAdd = new CustomerAddCommand()
                {
                    Name = MocksData.CustomerAddCommand.Name,
                    Birth = MocksData.CustomerAddCommand.Birth,
                    Gender = (EGender)MocksData.CustomerAddCommand.Gender,
                    CashBalance = MocksData.CustomerAddCommand.CashBalance
                };

                var responseAdd = await _customerServiceSoapClient.AddAsync(commandAdd);
                var idAdd = ((CustomerCommandResult)responseAdd?.Body?.AddResult?.Data).Id;

                var command = new CustomerDeleteCommand() { Id = idAdd };

                var response = await _customerServiceSoapClient.DeleteAsync(command);

                var commandResult = response?.Body?.DeleteResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.True);
                    Assert.That(commandResult.Message, Is.EqualTo("Customer successfully deleted!"));
                    Assert.That(commandResult.Errors, Is.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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
                var command = new CustomerDeleteCommand() { Id = id };

                var response = await _customerServiceSoapClient.DeleteAsync(command);

                var commandResult = response?.Body?.DeleteResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Inconsistencies in the data"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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
                var command = new CustomerDeleteCommand() { Id = id };

                var response = await _customerServiceSoapClient.DeleteAsync(command);

                var commandResult = response?.Body?.DeleteResult;

                TestContext.WriteLine(commandResult.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(commandResult.Success, Is.False);
                    Assert.That(commandResult.Message, Is.EqualTo("Invalid parameters"));
                    Assert.That(commandResult.Errors, Is.Not.Empty);
                    Assert.That(commandResult.Data, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task Get_Registred_Id_Success()
        {
            try
            {
                var command = new CustomerAddCommand()
                {
                    Name = MocksData.CustomerAddCommand.Name,
                    Birth = MocksData.CustomerAddCommand.Birth,
                    Gender = (EGender)MocksData.CustomerAddCommand.Gender,
                    CashBalance = MocksData.CustomerAddCommand.CashBalance
                };

                var responseAdd = await _customerServiceSoapClient.AddAsync(command);
                var idAdd = ((CustomerCommandResult)responseAdd?.Body?.AddResult?.Data).Id;

                var response = await _customerServiceSoapClient.GetAsync(idAdd);

                var result = response?.Body?.GetResult;

                TestContext.WriteLine(result.ToJson());
                
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.EqualTo(idAdd));
                    Assert.That(result.Name, Is.EqualTo(command.Name));
                    Assert.That(result.Birth, Is.EqualTo(command.Birth));
                    Assert.That(result.Gender, Is.EqualTo(command.Gender));
                    Assert.That(result.CashBalance, Is.EqualTo(command.CashBalance));
                    Assert.That(result.Active, Is.True);
                    Assert.That(result.CreationDate.Date, Is.EqualTo(DateTime.Now.Date));
                    Assert.That(result.ChangeDate, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
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

                TestContext.WriteLine(result.ToJson());

                Assert.That(result, Is.Null);
            }
            catch (EndpointNotFoundException e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task List_Success()
        {
            try
            {
                var command = new CustomerAddCommand()
                {
                    Name = MocksData.CustomerAddCommand.Name,
                    Birth = MocksData.CustomerAddCommand.Birth,
                    Gender = (EGender)MocksData.CustomerAddCommand.Gender,
                    CashBalance = MocksData.CustomerAddCommand.CashBalance
                };

                var responseAdd = await _customerServiceSoapClient.AddAsync(command);
                var idAdd = ((CustomerCommandResult)responseAdd?.Body?.AddResult?.Data).Id;

                var response = await _customerServiceSoapClient.ListAsync();

                var result = response?.Body?.ListResult;

                TestContext.WriteLine(result.ToJson());

                Assert.Multiple(() =>
                {
                    Assert.That(result[0].Id, Is.EqualTo(idAdd));
                    Assert.That(result[0].Name, Is.EqualTo(command.Name));
                    Assert.That(result[0].Birth, Is.EqualTo(command.Birth));
                    Assert.That(result[0].Gender, Is.EqualTo(command.Gender));
                    Assert.That(result[0].CashBalance, Is.EqualTo(command.CashBalance));
                    Assert.That(result[0].Active, Is.True);
                    Assert.That(result[0].CreationDate.Date, Is.EqualTo(DateTime.Now.Date));
                    Assert.That(result[0].ChangeDate, Is.Null);
                });
            }
            catch (EndpointNotFoundException e)
            {
                Assert.Inconclusive(e.Message);
            }
        }

        [Test]
        public async Task List_Not_Registred_Ids_Success()
        {
            try
            {
                var response = await _customerServiceSoapClient.ListAsync();

                var result = response?.Body?.ListResult;

                TestContext.WriteLine(result.ToJson());

                Assert.That(result, Is.Not.Null);
            }
            catch (EndpointNotFoundException e)
            {
                Assert.Inconclusive(e.Message);
            }
        }
    }
}