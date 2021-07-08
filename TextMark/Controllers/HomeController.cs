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
using Newtonsoft.Json;

namespace TextMark.Controllers
{
    public class HomeController : Controller
    {        
        private readonly TextMarkContext _context;
        

        public HomeController(TextMarkContext context)
        {
            _context = context;            
        }
        
       
        public ActionResult Index(int? Selected_Assigned_Anno_ID)
        {
            CL_Users_Home_Page HP = new CL_Users_Home_Page();
            HP.allAnnotations = All_Assigned_Anno_ToUsers();
            HP.allLabels = Select_Annotation_Labels(1);
            HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation(Selected_Assigned_Anno_ID);

            return View(HP);
        }
        public Assigned_Annotations_ToUsers_TB Selected_Assigned_Annotation(int? id)
        {
           var Selected_Annotation = _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB")
                .FirstOrDefault(m => m.Assigned_Anno_ID == id);
            if (Selected_Annotation == null)
            {
                return new Assigned_Annotations_ToUsers_TB();
            }
            return Selected_Annotation;
           
        }
        private List<Assigned_Annotations_ToUsers_TB> All_Assigned_Anno_ToUsers()
        {
            var UserID = "";

            if (!IsValidUser())
            {
                //return RedirectToAction("Index", "Login");
            }
            if (HttpContext.Session.GetString("UserID") != null)
            {
                UserID = HttpContext.Session.GetString("UserID").ToUpper();
            }
            return _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Where(m => m.User_ID.ToString() == UserID).ToList();

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
        public List<Labels_TB> Select_Annotation_Labels(int Project_ID)
        {
            return _context.Labels_TB.ToList();
            // return _context.Labels_TB.Include("Projects_TB").Where(m => m.Project_ID == Project_ID).ToList();
        }
        public async Task<IActionResult> Details(int? AnnotaionID)
        {
            //var Users_tb = await _context.Users_TB.Include("Roles_TB").Include("Roles_TB.Projects_TB")
            //  .FirstOrDefaultAsync(m => m.User_ID == id);
            //if (Users_tb == null)
            //{
            //    return NotFound();
            //}

            //return View(Users_tb);




           // var Assigned_Annotaion_ID = "";
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }
            
            //if (AnnotaionID != null)
            //{
            //    Assigned_Annotaion_ID = AnnotaionID.ToString().ToUpper();                
            //}
            var Selected_Annotation = await _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Where(m => m.Assigned_Anno_ID == AnnotaionID).FirstOrDefaultAsync();
          //  var Selected_Anno_Text = Selected_Annotation[0].Annotations_TB.Annotation_Text;
           // TempData["Selected_Anno_Text"] = Selected_Anno_Text;
            if (Selected_Annotation == null)
            {
                return NotFound();
            }

            // return RedirectToAction("Index", "Home");
            return  View(Selected_Annotation);
        }
        

       
    }
}
