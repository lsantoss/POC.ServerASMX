using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Commands.Output;
using POC.ServerASMX.Test.Base.Base;
using POC.ServerASMX.Test.Base.Extensions;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Commands.Output
{
    internal class CustomerCommandOutputTest : BaseUnitTest
    {
        [Test]
        public void Constructor_Success()
        {
            var customer = MocksUnitTest.Customer;

            var commandOutput = new CustomerCommandOutput(customer.Id, customer.Name, customer.Birth,
                customer.Gender, customer.CashBalance, customer.Active, customer.CreationDate, customer.ChangeDate);

            TestContext.WriteLine(commandOutput.Format());

            Assert.AreEqual(customer.Id, commandOutput.Id);
            Assert.AreEqual(customer.Name, commandOutput.Name);
            Assert.AreEqual(customer.Birth, commandOutput.Birth);
            Assert.AreEqual(customer.Gender, commandOutput.Gender);
            Assert.AreEqual(customer.CashBalance, commandOutput.CashBalance);
            Assert.AreEqual(customer.Active, commandOutput.Active);
            Assert.AreEqual(customer.CreationDate, commandOutput.CreationDate);
            Assert.AreEqual(customer.ChangeDate, commandOutput.ChangeDate);
        }
    }
}