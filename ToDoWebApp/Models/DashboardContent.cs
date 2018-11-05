using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoWebApp.Models
{
    public class DashboardContent
    {
        public DashboardContent(List<TodoList> lists, TodoList current, UserTask task, string sortBy)
        {
            TodoLists = lists;
            CurrentList = current;
            CurrentTask = task;
            SortBy = sortBy;
        }

        public List<TodoList> TodoLists { get; set; }
        public TodoList CurrentList { get; set; }
        public UserTask CurrentTask { get; set; }
        public string SortBy { get; set; }
    }

    
}
