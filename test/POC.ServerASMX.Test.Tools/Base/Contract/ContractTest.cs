using NUnit.Framework;
using POC.ServerASMX.Test.Tools.Base.Common;
using POC.ServerASMX.Test.Tools.MockContractTests;

namespace POC.ServerASMX.Test.Tools.Base.Contract
{
    [TestFixture]
    public class ContractTest : DatabaseTest
    {
        protected MocksIntegrationTest MocksIntegrationTest { get; set; }

        public ContractTest() => MocksIntegrationTest = new MocksIntegrationTest();

        [OneTimeSetUp]
        protected override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
            MocksIntegrationTest = new MocksIntegrationTest();
        }

        [OneTimeTearDown]
        protected override void OneTimeTearDown()
        {
            base.OneTimeTearDown();
            MocksIntegrationTest = null;
        }

        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();
            MocksIntegrationTest = new MocksIntegrationTest();
        }

        [TearDown]
        protected override void TearDown()
        {
            base.TearDown();
            MocksIntegrationTest = null;
        }
    }
}