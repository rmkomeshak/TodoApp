using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoWebApp.Models {

    public class UserTask
    {
        public UserTask()
        {
            UserID = 0;
            TaskID = 0;
            Status = 0;
            Title = "";
            Body = "";
            StartDate = DateTime.MinValue;
        }

        public UserTask(int uid, int tid, int lid, int status, string title, string body, DateTime startdate)
        {
            UserID = uid;
            TaskID = tid;
            ListID = lid;
            Status = status;
            Title = title;
            Body = body;
            StartDate = startdate;
        }

        public int UserID { get; set; }
        public int TaskID { get; set; }
        public int ListID { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime StartDate { get; set; }
    }
}
