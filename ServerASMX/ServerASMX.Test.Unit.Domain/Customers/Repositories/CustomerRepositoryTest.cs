using NUnit.Framework;
using ServerASMX.Domain.Customers.Repositories;
using ServerASMX.Test.Base.Base;
using ServerASMX.Test.Base.Extensions;

namespace ServerASMX.Test.Unit.Domain.Customers.Repositories
{
    internal class CustomerRepositoryTest : DatabaseTest
    {
        [Test]
        public void Insert()
        {
            var customer = MocksTest.Customer1;

            var repository = new CustomerRepository();
            repository.Insert(customer);

            var result = repository.Get(customer.Id);

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
    }
}
