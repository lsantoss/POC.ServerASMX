using NUnit.Framework;
using ServerASMX.Test.Base.Mocks;

namespace ServerASMX.Test.Base.Base
{
    [TestFixture]
    public class BaseTest
    {
        protected MocksTest MocksTest { get; set; }

        public BaseTest() => MocksTest = new MocksTest();

        [OneTimeSetUp]
        protected virtual void OneTimeSetUp() => MocksTest = new MocksTest();

        [OneTimeTearDown]
        protected virtual void OneTimeTearDown() => MocksTest = null;

        [SetUp]
        protected virtual void SetUp() => MocksTest = new MocksTest();

        [TearDown]
        protected virtual void TearDown() => MocksTest = null;
    }
}