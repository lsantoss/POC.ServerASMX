using System;
using System.Data.SqlClient;

namespace POC.ServerASMX.Infra.Data.DataContexts.Interfaces
{
    public interface IDataContext : IDisposable
    {
        SqlConnection Connection { get; }
    }
}