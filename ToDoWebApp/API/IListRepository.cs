using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoWebApp.Models;

namespace ToDoWebApp.API
{
    interface IListRepository
    {
        void CreateList(int uid, string title);
        List<TodoList> GetListsByUserID(int uid);
        void DeleteList(int lid);
    }
}
