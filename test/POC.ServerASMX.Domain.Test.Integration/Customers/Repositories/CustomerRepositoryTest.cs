using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Enums;
using POC.ServerASMX.Domain.Customers.Interfaces.Repositories;
using POC.ServerASMX.Domain.Customers.Repositories;
using POC.ServerASMX.Test.Base.Extensions;
using POC.ServerASMX.Test.Base.Mocks.UnitTests;
using POC.ServerASMX.Test.Tools.Base.Integration;
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading.Tasks;

namespace POC.ServerASMX.Domain.Test.Integration.Customers.Repositories
{
    internal class CustomerRepositoryTest : IntegrationTest
    {
        private readonly ICustomerRepository _repository;

        public CustomerRepositoryTest() => _repository = new CustomerRepository();

        [Test]
        public async Task InsertAsync_Success()
        {
            var customer = MocksData.Customer;

            await _repository.InsertAsync(customer);

            var result = await _repository.GetAsync(customer.Id);

            TestContext.WriteLine(result.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(customer.Id));
                Assert.That(result.Name, Is.EqualTo(customer.Name));
                Assert.That(result.Birth, Is.EqualTo(customer.Birth));
                Assert.That(result.Gender, Is.EqualTo(customer.Gender));
                Assert.That(result.CashBalance, Is.EqualTo(customer.CashBalance));
                Assert.That(result.Active, Is.True);
                Assert.That(result.CreationDate.Date, Is.EqualTo(customer.CreationDate.Date));
                Assert.That(result.ChangeDate, Is.Null);
            });
        }

        [Test]
        [TestCase(null)]
        public void InsertAsync_Invalid_Name_Exception(string name)
        {
            var customer = MocksData.Customer;
            customer.SetName(name);

            Assert.ThrowsAsync<SqlException>(() => _repository.InsertAsync(customer));
        }

        [Test]
        public void InsertAsync_Invalid_Birth_Exception()
        {
            var customer = MocksData.Customer;
            customer.SetBirth(DateTime.MinValue);

            Assert.ThrowsAsync<SqlTypeException>(() => _repository.InsertAsync(customer));
        }

        [Test]
        [TestCase(-1)]
        public void InsertAsync_Invalid_Gender_Exception(EGender gender)
        {
            var customer = MocksData.Customer;
            customer.SetGender(gender);

            Assert.ThrowsAsync<SqlException>(() => _repository.InsertAsync(customer));
        }

        [Test]
        public async Task UpdateAsync_Success()
        {
            await _repository.InsertAsync(MocksData.Customer);

            var customer = MocksData.CustomerEdited;

            await _repository.UpdateAsync(customer);

            var result = await _repository.GetAsync(customer.Id);

            TestContext.WriteLine(result.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(customer.Id));
                Assert.That(result.Name, Is.EqualTo(customer.Name));
                Assert.That(result.Birth, Is.EqualTo(customer.Birth));
                Assert.That(result.Gender, Is.EqualTo(customer.Gender));
                Assert.That(result.CashBalance, Is.EqualTo(customer.CashBalance));
                Assert.That(result.Active, Is.True);
                Assert.That(result.CreationDate.Date, Is.EqualTo(customer.CreationDate.Date));
                Assert.That(result.ChangeDate, Is.Null);
            });
        }

        [Test]
        [TestCase(null)]
        public async Task UpdateAsync_Invalid_Name_Exception(string name)
        {
            var customer = MocksData.Customer;

            await _repository.InsertAsync(customer);

            customer.SetName(name);

            Assert.ThrowsAsync<SqlException>(() => _repository.UpdateAsync(customer));
        }

        [Test]
        public async Task UpdateAsync_Invalid_Birth_Exception()
        {
            var customer = MocksData.Customer;

            await _repository.InsertAsync(customer);

            customer.SetBirth(DateTime.MinValue);

            Assert.ThrowsAsync<SqlTypeException>(() => _repository.UpdateAsync(customer));
        }

        [Test]
        [TestCase(-1)]
        public async Task UpdateAsync_Invalid_Gender_Exception(EGender gender)
        {
            var customer = MocksData.Customer;

            await _repository.InsertAsync(customer);

            customer.SetGender(gender);

            Assert.ThrowsAsync<SqlException>(() => _repository.UpdateAsync(customer));
        }

        [Test]
        public async Task DeleteAsync_Success()
        {
            var customer = MocksData.Customer;

            await _repository.InsertAsync(customer);

            await _repository.DeleteAsync(customer.Id);

            var result = await _repository.GetAsync(customer.Id);

            TestContext.WriteLine(result.ToJson());

            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public async Task ChangeActivityStateAsync_Success(bool active)
        {
            var customer = MocksData.Customer;

            await _repository.InsertAsync(customer);

            await _repository.ChangeActivityStateAsync(customer.Id, active);

            var result = await _repository.GetAsync(customer.Id);

            TestContext.WriteLine(result.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(customer.Id));
                Assert.That(result.Name, Is.EqualTo(customer.Name));
                Assert.That(result.Birth, Is.EqualTo(customer.Birth));
                Assert.That(result.Gender, Is.EqualTo(customer.Gender));
                Assert.That(result.CashBalance, Is.EqualTo(customer.CashBalance));
                Assert.That(result.Active, Is.EqualTo(active));
                Assert.That(result.CreationDate.Date, Is.EqualTo(customer.CreationDate.Date));
                Assert.That(result.ChangeDate.Value.Date, Is.EqualTo(DateTime.Now.Date));
            });
        }

        [Test]
        public async Task GetAsync_Registred_Id_Success()
        {
            var customer = MocksData.Customer;

            await _repository.InsertAsync(customer);

            var result = await _repository.GetAsync(customer.Id);

            TestContext.WriteLine(result.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(customer.Id));
                Assert.That(result.Name, Is.EqualTo(customer.Name));
                Assert.That(result.Birth, Is.EqualTo(customer.Birth));
                Assert.That(result.Gender, Is.EqualTo(customer.Gender));
                Assert.That(result.CashBalance, Is.EqualTo(customer.CashBalance));
                Assert.That(result.Active, Is.True);
                Assert.That(result.CreationDate.Date, Is.EqualTo(customer.CreationDate.Date));
                Assert.That(result.ChangeDate, Is.Null);
            });
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(long.MaxValue)]
        public async Task GetAsync_Not_Registred_Id_Success(long id)
        {
            var result = await _repository.GetAsync(id);

            TestContext.WriteLine(result.ToJson());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task ListAsync_Registred_Ids_Success()
        {
            var customer = MocksData.Customer;

            await _repository.InsertAsync(customer);

            var result = await _repository.ListAsync();

            TestContext.WriteLine(result.ToJson());

            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0].Id, Is.EqualTo(customer.Id));
                Assert.That(result[0].Name, Is.EqualTo(customer.Name));
                Assert.That(result[0].Birth, Is.EqualTo(customer.Birth));
                Assert.That(result[0].Gender, Is.EqualTo(customer.Gender));
                Assert.That(result[0].CashBalance, Is.EqualTo(customer.CashBalance));
                Assert.That(result[0].Active, Is.EqualTo(customer.Active));
                Assert.That(result[0].CreationDate.Date, Is.EqualTo(customer.CreationDate.Date));
                Assert.That(result[0].ChangeDate, Is.EqualTo(customer.ChangeDate));
            });
        }

        [Test]
        public async Task ListAsync_Not_Registred_Ids_Success()
        {
            var result = await _repository.ListAsync();

            TestContext.WriteLine(result.ToJson());

            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task CheckIdAsync_Registred_Id_Success()
        {
            var customer = MocksData.Customer;

            await _repository.InsertAsync(customer);

            var result = await _repository.CheckIdAsync(customer.Id);

            TestContext.WriteLine(result.ToJson());

            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(long.MaxValue)]
        public async Task CheckIdAsync_Not_Registred_Id_Success(long id)
        {
            var result = await _repository.CheckIdAsync(id);

            TestContext.WriteLine(result.ToJson());

            Assert.That(result, Is.False);
        }
    }
}
