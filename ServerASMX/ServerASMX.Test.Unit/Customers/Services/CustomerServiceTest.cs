using NUnit.Framework;
using ServerASMX.Test.Base.Base;

namespace ServerASMX.Test.Unit.Customers.Services
{
    internal class CustomerServiceTest : DatabaseTest
    {
        private readonly CustomerService _customerService;

        public CustomerServiceTest() => _customerService = new CustomerService();

        [Test]
        public void Add_Success()
        {
            
        }
    }
}
