using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace ToDoWebApp.API
{
    interface IDataRepository
    {
        SqliteConnection GetDBConnection();
    }
}