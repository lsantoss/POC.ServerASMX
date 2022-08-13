using NUnit.Framework;
using POC.ServerASMX.Infra.Data.Customers.DTOs;
using POC.ServerASMX.Test.Tools.Base.Unit;
using POC.ServerASMX.Test.Tools.Extensions;

namespace POC.ServerASMX.Infra.Data.Test.Integration.Customers.DTOs
{
    internal class CustomerDTOTest : UnitTest
    {
        [Test]
        public void Constructor_Success()
        {
            var customer = MockData.Customer;

            var commandDTO = new CustomerDTO(customer.Id, customer.Name, customer.Birth,
                customer.Gender, customer.CashBalance, customer.Active, customer.CreationDate, customer.ChangeDate);

            TestContext.WriteLine(commandDTO.ToJson());

            Assert.Multiple(() =>
            {
                Assert.That(commandDTO.Id, Is.EqualTo(customer.Id));
                Assert.That(commandDTO.Name, Is.EqualTo(customer.Name));
                Assert.That(commandDTO.Birth, Is.EqualTo(customer.Birth));
                Assert.That(commandDTO.Gender, Is.EqualTo(customer.Gender));
                Assert.That(commandDTO.CashBalance, Is.EqualTo(customer.CashBalance));
                Assert.That(commandDTO.Active, Is.EqualTo(customer.Active));
                Assert.That(commandDTO.CreationDate, Is.EqualTo(customer.CreationDate));
                Assert.That(commandDTO.ChangeDate, Is.EqualTo(customer.ChangeDate));
            });
        }
    }
}