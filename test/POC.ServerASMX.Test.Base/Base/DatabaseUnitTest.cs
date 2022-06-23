﻿using Dapper;
using NUnit.Framework;
using POC.ServerASMX.Test.Base.Mocks.UnitTests;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace POC.ServerASMX.Test.Base.Base
{
    [TestFixture]
    public class DatabaseUnitTest : BaseUnitTest
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString;
        private static readonly string _connectionStringReal = _connectionString.Replace("ServerASMXTest", "ServerASMX");

        private static readonly string _scriptCreateDatabasePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Sql\CreateDatabase.sql";
        private static readonly string _scriptCreateTablesPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Sql\CreateTables.sql";
        private static readonly string _scriptDropTablesPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Sql\DropTables.sql";

        public DatabaseUnitTest() : base() { }

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
            MocksUnitTest = new MocksUnitTest();
            PrepareDatabase();
        }

        private void ClearData()
        {
            MocksUnitTest = null;
            DestroyDatabase();
        }

        private void PrepareDatabase()
        {
            try
            {
                using (var streamReader = new StreamReader(_scriptCreateDatabasePath))
                {
                    using (var connection = new SqlConnection(_connectionStringReal))
                    {
                        connection.Execute(streamReader.ReadToEnd());
                    }
                }

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

        private void DestroyDatabase()
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