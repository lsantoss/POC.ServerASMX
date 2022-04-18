using NUnit.Framework;
using ServerASMX.Domain.Customers.Enums;
using ServerASMX.Domain.Customers.Interfaces.Repositories;
using ServerASMX.Domain.Customers.Repositories;
using ServerASMX.Test.Base.Base;
using ServerASMX.Test.Base.Extensions;
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace ServerASMX.Test.Unit.Domain.Customers.Repositories
{
    internal class CustomerRepositoryTest : DatabaseTest
    {
        private readonly ICustomerRepository _repository;

        public CustomerRepositoryTest() => _repository = new CustomerRepository();

        [Test]
        public void Insert_Success()
        {
            var customer = MocksTest.Customer;

            _repository.Insert(customer);

            var result = _repository.Get(customer.Id);

            TestContext.WriteLine(result.Format());

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
        public void Insert_Invalid_Name_Exception(string name)
        {
            var customer = MocksTest.Customer;
            customer.SetName(name);

            Assert.Throws<SqlException>(() => _repository.Insert(customer));
        }

        [Test]
        public void Insert_Invalid_Birth_Exception()
        {
            var customer = MocksTest.Customer;
            customer.SetBirth(DateTime.MinValue);

            Assert.Throws<SqlTypeException>(() => _repository.Insert(customer));
        }

        [Test]
        [TestCase(-1)]
        public void Insert_Invalid_Gender_Exception(EGender gender)
        {
            var customer = MocksTest.Customer;
            customer.SetGender(gender);

            Assert.Throws<SqlException>(() => _repository.Insert(customer));
        }

        [Test]
        public void Update_Success()
        {
            _repository.Insert(MocksTest.Customer);

            var customer = MocksTest.CustomerEdited;

            _repository.Update(customer);

            var result = _repository.Get(customer.Id);

            TestContext.WriteLine(result.Format());

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
        public void Update_Invalid_Name_Exception(string name)
        {
            var customer = MocksTest.Customer;

            _repository.Insert(customer);

            customer.SetName(name);

            Assert.Throws<SqlException>(() => _repository.Update(customer));
        }

        [Test]
        public void Update_Invalid_Birth_Exception()
        {
            var customer = MocksTest.Customer;

            _repository.Insert(customer);

            customer.SetBirth(DateTime.MinValue);

            Assert.Throws<SqlTypeException>(() => _repository.Update(customer));
        }

        [Test]
        [TestCase(-1)]
        public void Update_Invalid_Gender_Exception(EGender gender)
        {
            var customer = MocksTest.Customer;

            _repository.Insert(customer);

            customer.SetGender(gender);

            Assert.Throws<SqlException>(() => _repository.Update(customer));
        }

        [Test]
        public void Delete_Success()
        {
            var customer = MocksTest.Customer;

            _repository.Insert(customer);

            _repository.Delete(customer.Id);

            var result = _repository.Get(customer.Id);

            TestContext.WriteLine(result.Format());

            Assert.IsNull(result);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void ChangeActivityState_Success(bool active)
        {
            var customer = MocksTest.Customer;

            _repository.Insert(customer);

            _repository.ChangeActivityState(customer.Id, active);

            var result = _repository.Get(customer.Id);

            TestContext.WriteLine(result.Format());

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
        public void Get_Registred_Id_Success()
        {
            var customer = MocksTest.Customer;

            _repository.Insert(customer);

            var result = _repository.Get(customer.Id);

            TestContext.WriteLine(result.Format());

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
        public void Get_Not_Registred_Id_Success(long id)
        {
            var result = _repository.Get(id);

            TestContext.WriteLine(result.Format());

            Assert.IsNull(result);
        }

        [Test]
        public void List_Registred_Ids_Success()
        {
            var customer = MocksTest.Customer;

            _repository.Insert(customer);

            var result = _repository.List();

            TestContext.WriteLine(result.Format());

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
        public void List_Not_Registred_Ids_Success()
        {
            var result = _repository.List();

            TestContext.WriteLine(result.Format());

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void CheckId_Registred_Id_Success()
        {
            var customer = MocksTest.Customer;

            _repository.Insert(customer);

            var result = _repository.CheckId(customer.Id);

            TestContext.WriteLine(result.Format());

            Assert.True(result);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(long.MaxValue)]
        public void CheckId_Not_Registred_Id_Success(long id)
        {
            var result = _repository.CheckId(id);

            TestContext.WriteLine(result.Format());

            Assert.False(result);
        }
    }
}
