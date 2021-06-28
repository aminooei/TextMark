using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TextMark.Data;
using TextMark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace TextMark.Controllers
{
    public class HomeController : Controller
    {        
        private readonly TextMarkContext _context;

        public HomeController(TextMarkContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            var UserID = "";

            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }
            if (HttpContext.Session.GetString("UserID") != null)
            {
                UserID = HttpContext.Session.GetString("UserID").ToUpper();
            }
            return View(await _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Where(m => m.User_ID.ToString() == UserID).ToListAsync());
            // return View();
        }
        private bool IsValidUser()
        {

            string usertype = "";

            if (HttpContext.Session.GetString("UserType") != null)
            {
                usertype = HttpContext.Session.GetString("UserType").ToUpper();
            }


            if (usertype == "USER")
            {
                return true;
            }
            else if (usertype == "ADMIN")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<IActionResult> Details(int? AnnotaionID)
        {
            var UserID = "";
            var Annotaion_ID = "";
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }
            if (HttpContext.Session.GetString("UserID") != null)
            {
                UserID = HttpContext.Session.GetString("UserID").ToUpper();
            }
            if (AnnotaionID != null)
            {
                Annotaion_ID = AnnotaionID.ToString().ToUpper();
            }
            var Selected_Annotation = await _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Where(m => m.User_ID.ToString() == UserID && m.Annotation_ID.ToString() == Annotaion_ID).ToListAsync();
            var Selected_Anno_Text = Selected_Annotation[0].Annotations_TB.Annotation_Text;
            TempData["Selected_Anno_Text"] = Selected_Anno_Text;
            if (Selected_Annotation == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
