using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoWebApp.Models;

//This file is used solely for interacting with the database for task related issues (deleting, marking complete, creating, etc.)
namespace ToDoWebApp.API
{
    public class TaskRepository : ITaskRepository
    {
        SqliteConnection connection;

        public TaskRepository()
        {
            IDataRepository dataRepo = new DataRepository();
            connection = dataRepo.GetDBConnection();
        }

        public List<UserTask> GetTaskByUserID(int uid)
        {
            List<UserTask> tasks = new List<UserTask>();

            int _uid = 0, _tid = 0, _lid = 0, _status = -1;
            string _title = "", _body = "";
            DateTime _startdate = DateTime.MinValue;

            string query = "SELECT * FROM task WHERE uid=" + uid + ";";

            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                _tid = (int)(long)reader["tid"];
                _uid = (int)(long)reader["uid"];
                _lid = (int)(long)reader["lid"];
                _status = (int)(long)reader["status"];
                _title = reader["title"].ToString();
                _body = reader["body"].ToString();
                _startdate = Convert.ToDateTime(reader["startdate"]);

                tasks.Add(new UserTask(_uid, _tid, _lid, _status, _title, _body, _startdate));
            }

            connection.Close();
            return tasks;
        }

        public List<UserTask> GetTaskByListID(int lid)
        {
            List<UserTask> tasks = new List<UserTask>();

            int _uid = 0, _tid = 0, _lid = 0, _status = -1;
            string _title = "", _body = "";
            DateTime _startdate = DateTime.MinValue;

            string query = "SELECT * FROM task WHERE lid=" + lid + ";";

            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                _tid = (int)(long)reader["tid"];
                _uid = (int)(long)reader["uid"];
                _lid = (int)(long)reader["lid"];
                _status = (int)(long)reader["status"];
                _title = reader["title"].ToString();
                _body = reader["body"].ToString();
                _startdate = Convert.ToDateTime(reader["startdate"]);

                tasks.Add(new UserTask(_uid, _tid, _lid, _status, _title, _body, _startdate));
            }

            connection.Close();
            return tasks;

        }

        public UserTask GetTaskByTaskID(int tid)
        {
            UserTask task = new UserTask();

            int _uid = 0, _tid = 0, _lid = 0, _status = -1;
            string _title = "", _body = "";
            DateTime _startdate = DateTime.MinValue;

            string query = "SELECT * FROM task WHERE tid=" + tid + ";";

            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                _tid = (int)(long)reader["tid"];
                _uid = (int)(long)reader["uid"];
                _lid = (int)(long)reader["lid"];
                _status = (int)(long)reader["status"];
                _title = reader["title"].ToString();
                _body = reader["body"].ToString();
                _startdate = Convert.ToDateTime(reader["startdate"]);

               task = new UserTask(_uid, _tid, _lid, _status, _title, _body, _startdate);
            }

            connection.Close();
            return task;

        }

        public void CreateTask(int lid, int uid, string title, string body)
        {
            
            if (!String.IsNullOrEmpty(body))
                body = StringCleanse(body);
            if (!String.IsNullOrEmpty(title))
                title = StringCleanse(title);

            using (connection)
            {
                connection.Open();
                string query = "INSERT INTO task (uid, lid, status, title, body, startdate) VALUES (" +
                uid + ", " + lid + ", 1, '" + title + "', '" + body + "', '" + DateTime.Now + "');";

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void InvertTaskStatus(int tid, int status)
        {
            int new_status = -1;

            if (status == 1)
                new_status = 0;
            else
                new_status = 1;

            using (connection)
            {
                connection.Open();
                string query = "UPDATE task SET status=" + new_status + " WHERE tid=" + tid + ";";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void DeleteTask(int tid)
        {
            using (connection)
            {
                connection.Open();
                string query = "DELETE FROM task WHERE tid=" + tid + ";";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void DeleteAllCompleted(int lid)
        {
            using (connection)
            {
                connection.Open();
                string query = "DELETE FROM task WHERE lid=" + lid + " AND status=0;";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void UpdateTask(int tid, string title, string body)
        {
            if (!String.IsNullOrEmpty(body))
                body = StringCleanse(body);
            if (!String.IsNullOrEmpty(title))
                title = StringCleanse(title);

            using (connection)
            {
                connection.Open();
                string query = "UPDATE task SET title='" + title + "', body='" + body + "' WHERE tid=" + tid + ";";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void DeleteListTasks(int lid)
        {
            using (connection)
            {
                connection.Open();
                string query = "DELETE FROM task WHERE lid=" + lid + ";";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        //Turn any ' to '' so it's escaped in the database.
        private string StringCleanse(string s)
        {
            s = s.Replace("'", "''");
            return s;
        }
    }
}
