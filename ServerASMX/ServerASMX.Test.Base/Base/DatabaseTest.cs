using Dapper;
using NUnit.Framework;
using ServerASMX.Test.Base.Constants;
using ServerASMX.Test.Base.Mocks;
using System;
using System.Data.SqlClient;
using System.IO;

namespace ServerASMX.Test.Base.Base
{
    [TestFixture]
    public class DatabaseTest : BaseTest
    {
        public DatabaseTest() => MocksTest = new MocksTest();

        [OneTimeSetUp]
        protected override void OneTimeSetUp()
        {
            MocksTest = new MocksTest();
            PrepareDatabase();
        }

        [OneTimeTearDown]
        protected override void OneTimeTearDown()
        {
            MocksTest = null;
            DestroyDatabase();
        }

        [SetUp]
        protected override void SetUp()
        {
            MocksTest = new MocksTest();
            PrepareDatabase();
        }

        [TearDown]
        protected override void TearDown()
        {
            MocksTest = null;
            DestroyDatabase();
        }

        protected void PrepareDatabase()
        {
            try
            {
                var scriptCreateDatabasePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Sql\CreateDatabase .sql";
                var scriptCreateTablesPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Sql\CreateTables.sql";

                using (var streamReader = new StreamReader(scriptCreateDatabasePath))
                {
                    using (var connection = new SqlConnection(ConnectionStrings.DataBase))
                    {
                        connection.Execute(streamReader.ReadToEnd());
                    }
                }

                using (var streamReader = new StreamReader(scriptCreateTablesPath))
                {
                    var scripts = streamReader.ReadToEnd().Split(
                        new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

                    using (var connection = new SqlConnection(ConnectionStrings.DataBaseTest))
                    {
                        foreach (var script in scripts)
                            connection.Execute(script);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error running scripts to prepare the test database: {ex.Message}");
            }
        }

        protected void DestroyDatabase()
        {
            try
            {
                var scriptDropTablesPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Sql\DropTables.sql";

                using (var streamReader = new StreamReader(scriptDropTablesPath))
                {
                    using (var connection = new SqlConnection(ConnectionStrings.DataBaseTest))
                    {
                        connection.Execute(streamReader.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error running scripts to destroy test database: {ex.Message}");
            }
        }
    }
}