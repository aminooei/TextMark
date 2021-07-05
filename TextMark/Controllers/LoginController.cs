using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TextMark.Data;
using TextMark.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace TextMark.Controllers
{
    public class LoginController : Controller
    {
        private readonly TextMarkContext _context;
        public LoginController(TextMarkContext context)
        {            
            _context = context;

           
        }

        public IActionResult Index()
        {
            ResetUserDetails();
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        private void ResetUserDetails()
        {
            HttpContext.Session.SetString("UserType", "");
            HttpContext.Session.SetString("UserID", "");
            HttpContext.Session.SetString("Username", "");
           // TempData["Username"] = "";
        }
        public void Check_Initialize_Admin()
        {
            var count_Projects = _context.Projects_TB.ToList().Count();
            if (count_Projects == 0)
            {
                Create_Project();
                Create_Role();
                Create_User();
            }
        }
        public  IActionResult DetailsAsync(string username, string password)
        {
            Check_Initialize_Admin();

            var login_admin = _context.Users_TB.Include("Roles_TB")
               .FirstOrDefault(m => m.Username == username && m.Password == password && m.Roles_TB.Role_Text == "ADMIN");

            if (login_admin != null)               
            {   
                HttpContext.Session.SetString("UserType", "ADMIN");
                HttpContext.Session.SetString("UserID", login_admin.User_ID.ToString());
                HttpContext.Session.SetString("Username", username);
                //TempData["Username"] = username;
                return RedirectToAction("Index", "Admin_ProjectsTB");
            }

            else
            {
              
                var login_user = _context.Users_TB.Include("Roles_TB")
              .FirstOrDefault(m => m.Username == username && m.Password == password && m.Roles_TB.Role_Text != "ADMIN");

                if (login_user != null)
                {
                    HttpContext.Session.SetString("UserType", "USER");
                    HttpContext.Session.SetString("UserID", login_user.User_ID.ToString());
                    TempData["Username"] = username;
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Login");


        }
        public  void Create_Role()
        {
            Roles_TB RT = new Roles_TB();
            RT.Role_Text = "Admin";
            RT.Project_ID = 1;
            _context.Add(RT);
            _context.SaveChanges(); 
            
        }
        public void Create_Project()
        {  
            Projects_TB PT = new Projects_TB();
            PT.Project_Name = "Project 1";
            _context.Add(PT); 
            _context.SaveChanges();
        }

        public void Create_User()
        {
            Users_TB UT = new Users_TB();
            UT.Username = "aminoo";
            UT.Password = "12345";
            UT.ConfirmPassword = "12345";
            UT.Role_ID = 1;
            //UT.Project_ID = 1;
            _context.Add(UT);
            _context.SaveChanges();
        }
    }
}
