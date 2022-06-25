using NUnit.Framework;
using POC.ServerASMX.Test.Base.Mocks.UnitTests;

namespace POC.ServerASMX.Test.Tools.Base.Common
{
    [TestFixture]
    public class BaseTest
    {
        protected MocksData MocksData { get; set; }

        public BaseTest() => MocksData = new MocksData();

        [OneTimeSetUp]
        protected virtual void OneTimeSetUp() => MocksData = new MocksData();

        [OneTimeTearDown]
        protected virtual void OneTimeTearDown() => MocksData = null;

        [SetUp]
        protected virtual void SetUp() => MocksData = new MocksData();

        [TearDown]
        protected virtual void TearDown() => MocksData = null;
    }
}