using NUnit.Framework;
using POC.ServerASMX.Test.Tools.Mocks;

namespace POC.ServerASMX.Test.Tools.Base.Common
{
    [TestFixture]
    public class BaseTest
    {
        protected MockData MockData { get; set; }

        public BaseTest() => MockData = new MockData();

        [OneTimeSetUp]
        protected virtual void OneTimeSetUp() => MockData = new MockData();

        [OneTimeTearDown]
        protected virtual void OneTimeTearDown() => MockData = null;

        [SetUp]
        protected virtual void SetUp() => MockData = new MockData();

        [TearDown]
        protected virtual void TearDown() => MockData = null;
    }
}