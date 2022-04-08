using NUnit.Framework;
using ServerASMX.Domain.Customers.Commands.Output;
using ServerASMX.Test.Base.Base;
using ServerASMX.Test.Base.Extensions;

namespace ServerASMX.Test.Unit.Domain.Customers.Commands.Output
{
    internal class CustomerCommandOutputTest : BaseTest
    {
        [Test]
        public void Constructor_Success_1()
        {
            var customer = MocksTest.Customer1;

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

        [Test]
        public void Constructor_Success_2()
        {
            var customer = MocksTest.Customer2;

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