using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Enums;
using POC.ServerASMX.Domain.Customers.Interfaces.Repositories;
using POC.ServerASMX.Domain.Customers.Repositories;
using POC.ServerASMX.Test.Tools.Base.Integration;
using POC.ServerASMX.Test.Tools.Constants;
using POC.ServerASMX.Test.Tools.Extensions;
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace POC.ServerASMX.Domain.Test.Integration.Customers.Repositories
{
    internal class CustomerRepositoryTest : IntegrationTest
    {
        private readonly ICustomerRepository _repository;

        public CustomerRepositoryTest() => _repository = new CustomerRepository();

        [Test]
        public void Insert_Success()
        {
            var customer = MockData.Customer;

            _repository.Insert(customer);

            var result = _repository.Get(customer.Id);

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
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void Insert_Invalid_Name_Exception(string name)
        {
            var customer = MockData.Customer;
            customer.SetName(name);

            Assert.Throws<SqlException>(() => _repository.Insert(customer));
        }

        [Test]
        public void Insert_Invalid_Birth_Exception()
        {
            var customer = MockData.Customer;
            customer.SetBirth(DateTime.MinValue);

            Assert.Throws<SqlTypeException>(() => _repository.Insert(customer));
        }

        [Test]
        [TestCase(-1)]
        public void Insert_Invalid_Gender_Exception(EGender gender)
        {
            var customer = MockData.Customer;
            customer.SetGender(gender);

            Assert.Throws<SqlException>(() => _repository.Insert(customer));
        }

        [Test]
        public void Update_Success()
        {
            _repository.Insert(MockData.Customer);

            var customer = MockData.CustomerEdited;

            _repository.Update(customer);

            var result = _repository.Get(customer.Id);

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
        [TestCase(StringsWithPredefinedSizes.StringWith101Caracters)]
        public void Update_Invalid_Name_Exception(string name)
        {
            var customer = MockData.Customer;

            _repository.Insert(customer);

            customer.SetName(name);

            Assert.Throws<SqlException>(() => _repository.Update(customer));
        }

        [Test]
        public void Update_Invalid_Birth_Exception()
        {
            var customer = MockData.Customer;

            _repository.Insert(customer);

            customer.SetBirth(DateTime.MinValue);

            Assert.Throws<SqlTypeException>(() => _repository.Update(customer));
        }

        [Test]
        [TestCase(-1)]
        public void Update_Invalid_Gender_Exception(EGender gender)
        {
            var customer = MockData.Customer;

            _repository.Insert(customer);

            customer.SetGender(gender);

            Assert.Throws<SqlException>(() => _repository.Update(customer));
        }

        [Test]
        public void Delete_Success()
        {
            var customer = MockData.Customer;

            _repository.Insert(customer);

            _repository.Delete(customer.Id);

            var result = _repository.Get(customer.Id);

            TestContext.WriteLine(result.ToJson());

            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void ChangeActivityState_Success(bool active)
        {
            var customer = MockData.Customer;

            _repository.Insert(customer);

            _repository.ChangeActivityState(customer.Id, active);

            var result = _repository.Get(customer.Id);

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
        public void Get_Registred_Id_Success()
        {
            var customer = MockData.Customer;

            _repository.Insert(customer);

            var result = _repository.Get(customer.Id);

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
        public void Get_Not_Registred_Id_Success(long id)
        {
            var result = _repository.Get(id);

            TestContext.WriteLine(result.ToJson());

            Assert.That(result, Is.Null);
        }

        [Test]
        public void List_Registred_Ids_Success()
        {
            var customer = MockData.Customer;

            _repository.Insert(customer);

            var result = _repository.List();

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
        public void List_Not_Registred_Ids_Success()
        {
            var result = _repository.List();

            TestContext.WriteLine(result.ToJson());

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void CheckId_Registred_Id_Success()
        {
            var customer = MockData.Customer;

            _repository.Insert(customer);

            var result = _repository.CheckId(customer.Id);

            TestContext.WriteLine(result.ToJson());

            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(long.MaxValue)]
        public void CheckId_Not_Registred_Id_Success(long id)
        {
            var result = _repository.CheckId(id);

            TestContext.WriteLine(result.ToJson());

            Assert.That(result, Is.False);
        }
    }
}
