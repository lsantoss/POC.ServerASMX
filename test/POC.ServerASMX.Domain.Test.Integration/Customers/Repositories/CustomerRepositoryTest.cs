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

            Assert.AreEqual(customer.Id, result.Id);
            Assert.AreEqual(customer.Name, result.Name);
            Assert.AreEqual(customer.Birth, result.Birth);
            Assert.AreEqual(customer.Gender, result.Gender);
            Assert.AreEqual(customer.CashBalance, result.CashBalance);
            Assert.IsTrue(result.Active);
            Assert.AreEqual(customer.CreationDate.Date, result.CreationDate.Date);
            Assert.IsNull(result.ChangeDate);
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

            Assert.AreEqual(customer.Id, result.Id);
            Assert.AreEqual(customer.Name, result.Name);
            Assert.AreEqual(customer.Birth, result.Birth);
            Assert.AreEqual(customer.Gender, result.Gender);
            Assert.AreEqual(customer.CashBalance, result.CashBalance);
            Assert.IsTrue(result.Active);
            Assert.AreEqual(customer.CreationDate.Date, result.CreationDate.Date);
            Assert.IsNull(result.ChangeDate);
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

            Assert.IsNull(result);
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

            Assert.AreEqual(customer.Id, result.Id);
            Assert.AreEqual(customer.Name, result.Name);
            Assert.AreEqual(customer.Birth, result.Birth);
            Assert.AreEqual(customer.Gender, result.Gender);
            Assert.AreEqual(customer.CashBalance, result.CashBalance);
            Assert.AreEqual(active, result.Active);
            Assert.AreEqual(customer.CreationDate.Date, result.CreationDate.Date);
            Assert.AreEqual(DateTime.Now.Date, result.ChangeDate.Value.Date);
        }

        [Test]
        public async Task GetAsync_Registred_Id_Success()
        {
            var customer = MocksData.Customer;

            await _repository.InsertAsync(customer);

            var result = await _repository.GetAsync(customer.Id);

            TestContext.WriteLine(result.ToJson());

            Assert.AreEqual(customer.Id, result.Id);
            Assert.AreEqual(customer.Name, result.Name);
            Assert.AreEqual(customer.Birth, result.Birth);
            Assert.AreEqual(customer.Gender, result.Gender);
            Assert.AreEqual(customer.CashBalance, result.CashBalance);
            Assert.IsTrue(result.Active);
            Assert.AreEqual(customer.CreationDate.Date, result.CreationDate.Date);
            Assert.IsNull(result.ChangeDate);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(long.MaxValue)]
        public async Task GetAsync_Not_Registred_Id_Success(long id)
        {
            var result = await _repository.GetAsync(id);

            TestContext.WriteLine(result.ToJson());

            Assert.IsNull(result);
        }

        [Test]
        public async Task ListAsync_Registred_Ids_Success()
        {
            var customer = MocksData.Customer;

            await _repository.InsertAsync(customer);

            var result = await _repository.ListAsync();

            TestContext.WriteLine(result.ToJson());

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(customer.Id, result[0].Id);
            Assert.AreEqual(customer.Name, result[0].Name);
            Assert.AreEqual(customer.Birth, result[0].Birth);
            Assert.AreEqual(customer.Gender, result[0].Gender);
            Assert.AreEqual(customer.CashBalance, result[0].CashBalance);
            Assert.AreEqual(customer.Active, result[0].Active);
            Assert.AreEqual(customer.CreationDate.Date, result[0].CreationDate.Date);
            Assert.AreEqual(customer.ChangeDate, result[0].ChangeDate);
        }

        [Test]
        public async Task ListAsync_Not_Registred_Ids_Success()
        {
            var result = await _repository.ListAsync();

            TestContext.WriteLine(result.ToJson());

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public async Task CheckIdAsync_Registred_Id_Success()
        {
            var customer = MocksData.Customer;

            await _repository.InsertAsync(customer);

            var result = await _repository.CheckIdAsync(customer.Id);

            TestContext.WriteLine(result.ToJson());

            Assert.True(result);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(long.MaxValue)]
        public async Task CheckIdAsync_Not_Registred_Id_Success(long id)
        {
            var result = await _repository.CheckIdAsync(id);

            TestContext.WriteLine(result.ToJson());

            Assert.False(result);
        }
    }
}
