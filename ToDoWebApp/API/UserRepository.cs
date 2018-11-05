using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoWebApp.Models;

namespace ToDoWebApp.API
{
    public class UserRepository : IUserRepository
    {
        SqliteConnection connection;

        public UserRepository()
        {
            IDataRepository dataRepo = new DataRepository();
            connection = dataRepo.GetDBConnection();
        }
        
        //NOTE: for the sake of ease and quickness - PASSWORDS ARE STORED IN PLAINTEXT.
        public bool isLoginValid(string username, string password)
        {
            string query = "SELECT uid FROM user WHERE username='" + username + "' AND password='" + password + "';";
            connection.Open();

            SqliteCommand command = new SqliteCommand(query, connection);

            SqliteDataReader reader = command.ExecuteReader();

            if (reader.Read())
                return true;

            return false;
        } 

    }
}
