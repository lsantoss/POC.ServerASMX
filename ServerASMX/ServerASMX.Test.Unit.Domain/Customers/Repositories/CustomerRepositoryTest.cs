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
            var customer = MocksTest.Customer1;

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
            var customer = MocksTest.Customer1;
            customer.SetName(name);

            Assert.Throws<SqlException>(() => _repository.Insert(customer));
        }

        [Test]
        public void Insert_Invalid_Birth_Exception()
        {
            var customer = MocksTest.Customer1;
            customer.SetBirth(DateTime.MinValue);

            Assert.Throws<SqlTypeException>(() => _repository.Insert(customer));
        }

        [Test]
        [TestCase(-1)]
        public void Insert_Invalid_Gender_Exception(EGender gender)
        {
            var customer = MocksTest.Customer1;
            customer.SetGender(gender);

            Assert.Throws<SqlException>(() => _repository.Insert(customer));
        }

        [Test]
        public void Update_Success()
        {
            var customer = MocksTest.Customer1;

            _repository.Insert(customer);

            var customerEdited = MocksTest.Customer2;
            customerEdited.SetId(customer.Id);

            _repository.Update(customerEdited);

            var result = _repository.Get(customerEdited.Id);

            TestContext.WriteLine(result.Format());

            Assert.AreEqual(customerEdited.Id, result.Id);
            Assert.AreEqual(customerEdited.Name, result.Name);
            Assert.AreEqual(customerEdited.Birth, result.Birth);
            Assert.AreEqual(customerEdited.Gender, result.Gender);
            Assert.AreEqual(customerEdited.CashBalance, result.CashBalance);
            Assert.IsTrue(result.Active);
            Assert.AreEqual(customerEdited.CreationDate.Date, result.CreationDate.Date);
            Assert.IsNull(result.ChangeDate);
        }

        [Test]
        [TestCase(null)]
        public void Update_Invalid_Name_Exception(string name)
        {
            var customer = MocksTest.Customer1;

            _repository.Insert(customer);

            customer.SetName(name);

            Assert.Throws<SqlException>(() => _repository.Update(customer));
        }

        [Test]
        public void Update_Invalid_Birth_Exception()
        {
            var customer = MocksTest.Customer1;

            _repository.Insert(customer);

            customer.SetBirth(DateTime.MinValue);

            Assert.Throws<SqlTypeException>(() => _repository.Update(customer));
        }

        [Test]
        [TestCase(-1)]
        public void Update_Invalid_Gender_Exception(EGender gender)
        {
            var customer = MocksTest.Customer1;

            _repository.Insert(customer);

            customer.SetGender(gender);

            Assert.Throws<SqlException>(() => _repository.Update(customer));
        }

        [Test]
        public void Delete_Success()
        {
            var customer = MocksTest.Customer1;

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
            var customer = MocksTest.Customer1;

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
            var customer = MocksTest.Customer1;

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
        public void Get_Not_Registred_Id_Success()
        {
            var customer = MocksTest.Customer1;

            var result = _repository.Get(customer.Id);

            TestContext.WriteLine(result.Format());

            Assert.IsNull(result);
        }

        [Test]
        public void List_Registred_Ids_Success()
        {
            var customer1 = MocksTest.Customer1;
            var customer2 = MocksTest.Customer2;
            var customer3 = MocksTest.Customer3;

            _repository.Insert(customer1);
            _repository.Insert(customer2);
            _repository.Insert(customer3);

            var result = _repository.List();

            TestContext.WriteLine(result.Format());

            Assert.AreEqual(3, result.Count);

            Assert.AreEqual(customer1.Id, result[0].Id);
            Assert.AreEqual(customer1.Name, result[0].Name);
            Assert.AreEqual(customer1.Birth, result[0].Birth);
            Assert.AreEqual(customer1.Gender, result[0].Gender);
            Assert.AreEqual(customer1.CashBalance, result[0].CashBalance);
            Assert.AreEqual(customer1.Active, result[0].Active);
            Assert.AreEqual(customer1.CreationDate.Date, result[0].CreationDate.Date);
            Assert.AreEqual(customer1.ChangeDate, result[0].ChangeDate);

            Assert.AreEqual(customer2.Id, result[1].Id);
            Assert.AreEqual(customer2.Name, result[1].Name);
            Assert.AreEqual(customer2.Birth, result[1].Birth);
            Assert.AreEqual(customer2.Gender, result[1].Gender);
            Assert.AreEqual(customer2.CashBalance, result[1].CashBalance);
            Assert.AreEqual(customer2.Active, result[1].Active);
            Assert.AreEqual(customer2.CreationDate.Date, result[1].CreationDate.Date);
            Assert.AreEqual(customer2.ChangeDate, result[1].ChangeDate);

            Assert.AreEqual(customer3.Id, result[2].Id);
            Assert.AreEqual(customer3.Name, result[2].Name);
            Assert.AreEqual(customer3.Birth, result[2].Birth);
            Assert.AreEqual(customer3.Gender, result[2].Gender);
            Assert.AreEqual(customer3.CashBalance, result[2].CashBalance);
            Assert.AreEqual(customer3.Active, result[2].Active);
            Assert.AreEqual(customer3.CreationDate.Date, result[2].CreationDate.Date);
            Assert.AreEqual(customer3.ChangeDate.Value.Date, result[2].ChangeDate.Value.Date);
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
            var customer = MocksTest.Customer1;

            _repository.Insert(customer);

            var result = _repository.CheckId(customer.Id);

            TestContext.WriteLine(result.Format());

            Assert.True(result);
        }

        [Test]
        public void CheckId_Not_Registred_Id_Success()
        {
            var customer = MocksTest.Customer1;

            var result = _repository.CheckId(customer.Id);

            TestContext.WriteLine(result.Format());

            Assert.False(result);
        }
    }
}
