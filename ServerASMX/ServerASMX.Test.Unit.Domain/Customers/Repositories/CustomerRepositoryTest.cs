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
    }
}
