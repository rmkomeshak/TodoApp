using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoWebApp.API
{
    interface IUserRepository
    {
        bool isLoginValid(string username, string password);
    }
}
