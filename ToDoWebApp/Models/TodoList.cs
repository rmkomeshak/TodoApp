using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoWebApp.API;

namespace ToDoWebApp.Models
{
    public class TodoList
    {
        ITaskRepository tr = new TaskRepository();

        public TodoList(int lid, int uid, string title)
        {
            ListID = lid;
            UserID = uid;
            ListTitle = title;
            Tasks = tr.GetTaskByListID(lid);
        }

        public int ListID { get; set; }
        public int UserID { get; set; }
        public string ListTitle { get; set; }
        public List<UserTask> Tasks { get; set; }
    }
}
