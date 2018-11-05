using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace ToDoWebApp.API
{
    public class DataRepository : IDataRepository
    {
        public readonly SqliteConnection connection;
        //Create the readonly configuration items for the database connection string

        public DataRepository()
        {

            this.connection = new SqliteConnection();
            //connection.ConnectionString = "Data Source=C:\\SQLite\\ToDo.db";
            connection.ConnectionString = "Data Source=" + System.Environment.CurrentDirectory.ToString() + "\\ToDo.db";
            Console.WriteLine(System.Environment.CurrentDirectory);
        }

        public SqliteConnection GetDBConnection()
        {
            return this.connection;
        }

    }
}