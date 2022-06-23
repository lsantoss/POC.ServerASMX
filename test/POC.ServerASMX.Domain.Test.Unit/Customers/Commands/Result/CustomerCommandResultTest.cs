using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Commands.Result;
using POC.ServerASMX.Test.Base.Base;
using POC.ServerASMX.Test.Base.Extensions;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Commands.Result
{
    internal class CustomerCommandResultTest : BaseUnitTest
    {
        [Test]
        public void Constructor_Success()
        {
            var customer = MocksUnitTest.Customer;

            var commandResult = new CustomerCommandResult(customer.Id, customer.Name, customer.Birth,
                customer.Gender, customer.CashBalance, customer.Active, customer.CreationDate, customer.ChangeDate);

            TestContext.WriteLine(commandResult.Format());

            Assert.AreEqual(customer.Id, commandResult.Id);
            Assert.AreEqual(customer.Name, commandResult.Name);
            Assert.AreEqual(customer.Birth, commandResult.Birth);
            Assert.AreEqual(customer.Gender, commandResult.Gender);
            Assert.AreEqual(customer.CashBalance, commandResult.CashBalance);
            Assert.AreEqual(customer.Active, commandResult.Active);
            Assert.AreEqual(customer.CreationDate, commandResult.CreationDate);
            Assert.AreEqual(customer.ChangeDate, commandResult.ChangeDate);
        }
    }
}