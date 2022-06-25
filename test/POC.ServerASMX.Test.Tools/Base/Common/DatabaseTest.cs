using Dapper;
using NUnit.Framework;
using POC.ServerASMX.Test.Base.Mocks.UnitTests;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace POC.ServerASMX.Test.Tools.Base.Common
{
    [TestFixture]
    public class DatabaseTest : BaseTest
    {
        private readonly string _connectionString;
        private readonly string _connectionStringReal;
        private readonly string _scriptCreateDatabasePath;
        private readonly string _scriptCreateTablesPath;
        private readonly string _scriptDropTablesPath;

        public DatabaseTest() : base()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString;
            _connectionStringReal = _connectionString.Replace("ServerASMXTest", "ServerASMX");
            _scriptCreateDatabasePath = $@"{baseDirectory}\Sql\CreateDatabase.sql";
            _scriptCreateTablesPath = $@"{baseDirectory}\Sql\CreateTables.sql";
            _scriptDropTablesPath = $@"{baseDirectory}\Sql\DropTables.sql";

            CreateDatabase();
        }

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
            MocksData = new MocksData();
            DropTablesAndProcedures();
            CreateTablesAndProcedures();
        }

        private void ClearData()
        {
            MocksData = null;
            DropTablesAndProcedures();
        }

        private void CreateDatabase()
        {
            try
            {
                using (var streamReader = new StreamReader(_scriptCreateDatabasePath))
                {
                    using (var connection = new SqlConnection(_connectionStringReal))
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
                using (var streamReader = new StreamReader(_scriptCreateTablesPath))
                {
                    var scripts = streamReader.ReadToEnd().Split(
                        new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

                    using (var connection = new SqlConnection(_connectionString))
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
                using (var streamReader = new StreamReader(_scriptDropTablesPath))
                {
                    using (var connection = new SqlConnection(_connectionString))
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