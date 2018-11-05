using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoWebApp.Models;

//This repository manages any database connections associated with creating, deleting, modifying todo lists
namespace ToDoWebApp.API
{
    public class ListRepository : IListRepository
    {
        SqliteConnection connection;
        ITaskRepository _taskRepository = new TaskRepository();

        public ListRepository()
        {
            IDataRepository dataRepo = new DataRepository();
            connection = dataRepo.GetDBConnection();
        }

        public void CreateList(int uid, string title)
        {
            if (!String.IsNullOrEmpty(title))
                title = StringCleanse(title);
            using (connection)
            {
                connection.Open();
                string query = "INSERT INTO list (uid, title) VALUES ( "+ uid + ",'" + title + "');";

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public List<TodoList> GetListsByUserID(int uid)
        {
            List<TodoList> lists = new List<TodoList>();
            
            string query = "SELECT * FROM list WHERE uid=" + uid + ";";

            int lid = 0;
            string title = "";

            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                uid = (int)(long)reader["uid"];
                lid = (int)(long)reader["lid"];
                title = reader["title"].ToString();

                lists.Add(new TodoList(lid, uid, title));
            }

            connection.Close();
            return lists;
        }

        public void DeleteList(int lid)
        {
            _taskRepository.DeleteListTasks(lid);

            using (connection)
            {
                connection.Open();
                string query = "DELETE FROM list WHERE lid=" + lid + ";";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            
            
        }

        private string StringCleanse(string s)
        {
            s = s.Replace("'", "''");
            return s;
        }

    }
}
