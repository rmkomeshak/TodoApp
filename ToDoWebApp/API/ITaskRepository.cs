using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoWebApp.Models;

namespace ToDoWebApp.API
{
    interface ITaskRepository
    {
        List<UserTask> GetTaskByUserID(int uid);
        List<UserTask> GetTaskByListID(int lid);
        void CreateTask(int lid, int uid, string title, string body);
        UserTask GetTaskByTaskID(int tid);
        void InvertTaskStatus(int tid, int status);
        void DeleteTask(int tid);
        void DeleteAllCompleted(int lid);
        void UpdateTask(int tid, string title, string body);
        void DeleteListTasks(int lid);
    }
}
