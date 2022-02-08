using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextMark.Controllers
{
    public class Admin_HelpController : Controller
    {
        public IActionResult Manage_Projects_Help()
        {
            return View();
        }
        public IActionResult Manage_Roles_Help()
        {
            return View();
        }
        public IActionResult Manage_Annotations_Labels_Help()
        {
            return View();
        }
        public IActionResult Manage_Classifications_Labels_Help()
        {
            return View();
        }
        public IActionResult Manage_Files_Help()
        {
            return View();
        }
        public IActionResult Manage_Users_Help()
        {
            return View();
        }
        public IActionResult Manage_Assigned_Text_Annotations_Help()
        {
            return View();
        }
        
        public IActionResult Manage_Assigned_Text_Classifications_Help()
        {
            return View();
        }
    }
}
