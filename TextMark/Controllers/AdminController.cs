using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TextMark.Data;
using TextMark.Models;


namespace TextMark.Controllers
{
    public class AdminController : Controller
    {
        private readonly TextMarkContext _context;
        public AdminController(TextMarkContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_User([Bind("User_ID,Username,Password,ConfirmPassword,Role_ID")] Users_TB users_tb)
        {            
            if (ModelState.IsValid)
            {
                _context.Add(users_tb);
                await _context.SaveChangesAsync();              
              //  TempData["Loggedin_Username"] = Users_TB.Username;
                return RedirectToAction("Index", "Home");
            }            
            return RedirectToAction("Index","Admin");
        }
    }
}
