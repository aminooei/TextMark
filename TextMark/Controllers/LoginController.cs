using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TextMark.Data;
using TextMark.Models;


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

        public async Task<IActionResult> Check_UserName_Password(string Username, string Password)
        {            

            var Users_TB = await _context.Users_TBs
                .FirstOrDefaultAsync(m => m.Username == Username && m.Password == Password);
            //if (Users_TB == null)
            //{
            //    return RedirectToAction("Index", "Home"); 
            //}
            TempData["Loggedin_User"] = Username;
            return View(Users_TB);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("ID,Username,Password,ConfirmPassword")] Users_TB Users_TB)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Users_TB);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                TempData["Loggedin_Username"] = Users_TB.Username;
                return RedirectToAction("Index", "Home");
            }
            return View(Users_TB);
        }

    }
}
