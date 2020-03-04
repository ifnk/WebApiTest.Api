using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace WebApiTest.Api.Dapper
{
    public class ConnectionFactory
    {
        public static DbConnection GetOpenConnection()
        {
            var connection = new SqliteConnection("Data Source=WebApiTest.Db");
            connection.Open();
            return connection;
        }
    }
}