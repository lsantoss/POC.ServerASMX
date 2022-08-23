using Dapper;
using NUnit.Framework;
using POC.ServerASMX.Test.Tools.Mocks;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace POC.ServerASMX.Test.Tools.Base.Common
{
    [TestFixture]
    public class DatabaseTest : BaseTest
    {
        public DatabaseTest() : base() => CreateDatabase();

        [OneTimeSetUp]
        protected override void OneTimeSetUp() => InitializeData();

        [OneTimeTearDown]
        protected override void OneTimeTearDown() => ClearData();

        [SetUp]
        protected override void SetUp() => InitializeData();

        [TearDown]
        protected override void TearDown() => ClearData();

        private void InitializeData()
        {
            MockData = new MockData();
            DropTablesAndProcedures();
            CreateTablesAndProcedures();
        }

        private void ClearData()
        {
            MockData = null;
            DropTablesAndProcedures();
        }

        private void CreateDatabase()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString;
                var connectionStringDefaultDatabase = connectionString.Replace("ServerASMX.Test", "master");

                var scriptCreateDatabasePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Sql\CreateDatabase.sql";

                using (var streamReader = new StreamReader(scriptCreateDatabasePath))
                {
                    using (var connection = new SqlConnection(connectionStringDefaultDatabase))
                    {
                        connection.Execute(streamReader.ReadToEnd());
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error running scripts to create the test database: {ex.Message}");
            }
        }

        private void CreateTablesAndProcedures()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString;

                var scriptCreateTablesPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Sql\CreateTablesAndProcedures.sql";

                using (var streamReader = new StreamReader(scriptCreateTablesPath))
                {
                    var scripts = streamReader.ReadToEnd().Split(
                        new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

                    using (var connection = new SqlConnection(connectionString))
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

        private void DropTablesAndProcedures()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString;

                var scriptDropTablesPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Sql\DropTablesAndProcedures.sql";

                using (var streamReader = new StreamReader(scriptDropTablesPath))
                {
                    using (var connection = new SqlConnection(connectionString))
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