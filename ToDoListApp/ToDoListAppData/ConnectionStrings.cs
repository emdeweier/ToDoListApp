using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListAppData
{
    public sealed class ConnectionStrings
    {
        public SqlConnection Connections { get; }
        public ConnectionStrings(string connectionString)
        {
            Connections = new SqlConnection(connectionString);
            Connections.Open();
        }
        public void Dispose()
        {
            Connections.Close();
        }
    }
}
