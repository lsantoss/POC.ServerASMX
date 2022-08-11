using NUnit.Framework;
using POC.ServerASMX.Infra.Data.DataContexts;
using POC.ServerASMX.Test.Tools.Base.Integration;
using POC.ServerASMX.Test.Tools.Extensions;
using System.Data;

namespace POC.ServerASMX.Infra.Data.Test.Integration.DataContexts
{
    internal class DataContextTest : IntegrationTest
    {
        [Test]
        public void Contructor_Success()
        {
            var dataContext = new DataContext();

            TestContext.WriteLine(dataContext.ToJson());

            Assert.That(dataContext.Connection.State, Is.EqualTo(ConnectionState.Open));
        }

        [Test]
        public void Dispose_Success()
        {
            var dataContext = new DataContext();

            dataContext.Dispose();

            TestContext.WriteLine($"Connection: {dataContext.Connection.State}");

            Assert.That(dataContext.Connection.State, Is.EqualTo(ConnectionState.Closed));
        }
    }
}