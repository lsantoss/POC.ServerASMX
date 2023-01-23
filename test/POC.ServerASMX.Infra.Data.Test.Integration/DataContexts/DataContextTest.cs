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
            //Act
            var dataContext = new DataContext();

            TestContext.WriteLine(dataContext.ToJson());

            //Assert
            Assert.That(dataContext.Connection.State, Is.EqualTo(ConnectionState.Open));
        }

        [Test]
        public void Dispose_Success()
        {
            //Arrange
            var dataContext = new DataContext();

            //Act
            dataContext.Dispose();

            TestContext.WriteLine($"Connection: {dataContext.Connection.State}");

            //Assert
            Assert.That(dataContext.Connection.State, Is.EqualTo(ConnectionState.Closed));
        }
    }
}