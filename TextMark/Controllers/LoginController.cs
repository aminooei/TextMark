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
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

    
        public async Task<IActionResult> DetailsAsync(string username, string password)
        {
            if (username == null)
            {
                return NotFound();
            }

        var login =  _context.Users_TB
                .FirstOrDefault(m => m.Username == username && m.Password == password && m.Roles_TB.Role_Text.ToUpper() == "ADMIN");


            if (login != null)
            {
                //var adminClaims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, username),
                //    new Claim(ClaimTypes.Role, "ADMIN")
                //};

                //var adminIdentity = new ClaimsIdentity(adminClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                //var adminPrincipal = new ClaimsPrincipal(new[] { adminIdentity });
                //await HttpContext.SignInAsync(adminPrincipal);

                
                HttpContext.Session.SetString("UserType", "ADMIN");
                //TempData["UserType"] = "ADMIN";
                //string usertype = TempData["UserType"].ToString();
                //  string usertype = HttpContext.Session.GetString("UserType").ToUpper();
                return RedirectToAction("Index", "Admin_UsersTB");
            }

            else
            {
                var userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "USER")
                };

                var userIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(userPrincipal);

                HttpContext.Session.SetString("UserType", "USER");
                return RedirectToAction("Index", "Home");
            }

           
         }

        
    }
}
