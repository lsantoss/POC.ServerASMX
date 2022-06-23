using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace POC.ServerASMX.Infra.Data.DataContexts
{
    public class DataContext : IDisposable
    {
        public SqlConnection Connection { get; }

        public DataContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString;
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}