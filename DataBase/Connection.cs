using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace StockPilot.DataBase
{
    public class Connection
    {
        readonly SQLiteConnection connection = new();
        public Connection()
        {
            connection.ConnectionString = @"Data Source = |DataDirectory|\DataBase\DataBase.db";
        }
        public SQLiteConnection Connect()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }
        public void Disconnect()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
