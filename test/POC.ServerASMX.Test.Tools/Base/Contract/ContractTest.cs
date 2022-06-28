using NUnit.Framework;
using POC.ServerASMX.Test.Tools.Base.Common;

namespace POC.ServerASMX.Test.Tools.Base.Contract
{
    [TestFixture]
    public class ContractTest : DatabaseTest
    {
        public ContractTest() : base() { }

        [OneTimeSetUp]
        protected override void OneTimeSetUp() => base.OneTimeSetUp();

        [OneTimeTearDown]
        protected override void OneTimeTearDown() => base.OneTimeTearDown();

        [SetUp]
        protected override void SetUp() => base.SetUp();

        [TearDown]
        protected override void TearDown() => base.TearDown();
    }
}