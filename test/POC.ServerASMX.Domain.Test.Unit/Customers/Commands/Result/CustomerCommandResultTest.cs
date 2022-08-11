using NUnit.Framework;
using POC.ServerASMX.Domain.Customers.Commands.Result;
using POC.ServerASMX.Test.Tools.Base.Unit;
using POC.ServerASMX.Test.Tools.Extensions;

namespace POC.ServerASMX.Domain.Test.Unit.Customers.Commands.Result
{
    internal class CustomerCommandResultTest : UnitTest
    {
        [Test]
        public void Constructor_Success()
        {
            var customer = MockData.Customer;

            var commandResult = new CustomerCommandResult(customer.Id, customer.Name, customer.Birth,
                customer.Gender, customer.CashBalance, customer.Active, customer.CreationDate, customer.ChangeDate);

            TestContext.WriteLine(commandResult.ToJson());
            
            Assert.Multiple(() =>
            {
                Assert.That(commandResult.Id, Is.EqualTo(customer.Id));
                Assert.That(commandResult.Name, Is.EqualTo(customer.Name));
                Assert.That(commandResult.Birth, Is.EqualTo(customer.Birth));
                Assert.That(commandResult.Gender, Is.EqualTo(customer.Gender));
                Assert.That(commandResult.CashBalance, Is.EqualTo(customer.CashBalance));
                Assert.That(commandResult.Active, Is.EqualTo(customer.Active));
                Assert.That(commandResult.CreationDate, Is.EqualTo(customer.CreationDate));
                Assert.That(commandResult.ChangeDate, Is.EqualTo(customer.ChangeDate));
            });
        }
    }
}