using NUnit.Framework;
using POC.ServerASMX.Test.Base.Mocks.IntegrationTests;

namespace POC.ServerASMX.Test.Base.Base
{
    [TestFixture]
    public class BaseIntegrationTest
    {
        protected MocksIntegrationTest MocksIntegrationTest { get; set; }

        public BaseIntegrationTest() => MocksIntegrationTest = new MocksIntegrationTest();

        [OneTimeSetUp]
        protected virtual void OneTimeSetUp() => MocksIntegrationTest = new MocksIntegrationTest();

        [OneTimeTearDown]
        protected virtual void OneTimeTearDown() => MocksIntegrationTest = null;

        [SetUp]
        protected virtual void SetUp() => MocksIntegrationTest = new MocksIntegrationTest();

        [TearDown]
        protected virtual void TearDown() => MocksIntegrationTest = null;
    }
}