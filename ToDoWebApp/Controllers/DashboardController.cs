using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoWebApp.API;
using ToDoWebApp.Models;

namespace ToDoWebApp.Controllers
{
    public class DashboardController : Controller
    {
        int temp_uid = 1;
        IListRepository _listRepository = new ListRepository();
        ITaskRepository _taskRepository = new TaskRepository();

        public IActionResult Index(int lid=0, int tid=0, string sortBy="all")
        {

            List<TodoList> _list = _listRepository.GetListsByUserID(temp_uid);
            TodoList current = _list[0];

            //sets the current list
            foreach(TodoList tdl in _list)
            {
                if (tdl.ListID == lid)
                    current = tdl;
            }

            //sets the current task
            UserTask t;

            if(tid == 0)
                t = new UserTask();
            else
                t = _taskRepository.GetTaskByTaskID(tid);

            if (sortBy.Equals("complete"))
                current.Tasks.RemoveAll(task => task.Status == 1);
            if (sortBy.Equals("active"))
                current.Tasks.RemoveAll(task => task.Status == 0);

            //model that contains all content needed on the dashboard
            DashboardContent dbc = new DashboardContent(_list, current, t, sortBy);

            return View(dbc);
        }

        public IActionResult CreateList(string title)
        {
            _listRepository.CreateList(temp_uid, title);
            return Redirect("~/Dashboard/");
        }

        public IActionResult CreateTask(int listid, string title, string body)
        {
            _taskRepository.CreateTask(listid, temp_uid, title, body);
            return Redirect("~/Dashboard?lid=" + listid);
        }

        public IActionResult InvertStatus(int listid, int taskid, int status)
        {
            _taskRepository.InvertTaskStatus(taskid, status);
            return Redirect("~/Dashboard?lid=" + listid + "&tid=" + taskid);

        }

        public IActionResult DeleteTask(int taskid, int listid)
        {
            _taskRepository.DeleteTask(taskid);
            return Redirect("~/Dashboard?lid=" + listid);
        }

        public IActionResult DeleteAll(int listid, string sortBy)
        {
            _taskRepository.DeleteAllCompleted(listid);
            return Redirect("~/Dashboard?lid=" + listid + "&sortBy=" + sortBy);
        }

        public IActionResult UpdateTask(int tid, string title, string body, int lid, string sortBy)
        {
            _taskRepository.UpdateTask(tid, title, body);
            return Redirect("~/Dashboard?lid=" + lid + "&tid=" + tid + "&sortBy=" + sortBy);
        }

        public IActionResult DeleteList(int listid)
        {
            _listRepository.DeleteList(listid);
            return Redirect("~/Dashboard");
        }
    }
}