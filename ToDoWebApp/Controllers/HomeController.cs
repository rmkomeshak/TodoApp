using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoWebApp.API;
using ToDoWebApp.Models;

namespace ToDoWebApp.Controllers
{
    public class HomeController : Controller
    {
        TaskRepository _taskRepository = new TaskRepository();
        UserRepository _userRepository = new UserRepository();

        public IActionResult Index()
        {
            return Redirect("~/Dashboard/");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult LoginAction(string username, string password)
        {
            if (_userRepository.isLoginValid(username, password))
            {  
                return Redirect("~/Dashboard/");
            }

            return Redirect("~/");
        }
    }
}
