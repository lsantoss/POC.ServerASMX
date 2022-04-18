using NUnit.Framework;
using ServerASMX.Test.Base.Mocks.UnitTests;

namespace ServerASMX.Test.Base.Base
{
    [TestFixture]
    public class BaseUnitTest
    {
        protected MocksUnitTest MocksUnitTest { get; set; }

        public BaseUnitTest() => MocksUnitTest = new MocksUnitTest();

        [OneTimeSetUp]
        protected virtual void OneTimeSetUp() => MocksUnitTest = new MocksUnitTest();

        [OneTimeTearDown]
        protected virtual void OneTimeTearDown() => MocksUnitTest = null;

        [SetUp]
        protected virtual void SetUp() => MocksUnitTest = new MocksUnitTest();

        [TearDown]
        protected virtual void TearDown() => MocksUnitTest = null;
    }
}