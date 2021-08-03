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
        
       
        public ActionResult Index(int Selected_Assigned_Anno_ID, int UserID)
        {           

            CL_Users_Home_Page HP = new CL_Users_Home_Page();
            HP.allAnnotations = All_Assigned_Anno_ToUsers();
            HP.allLabels = Select_Annotation_Labels();
            HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation(Selected_Assigned_Anno_ID, UserID);


            Select_All_Users();
            Select_All_Annotations();
            Select_All_Projects();

            return View(HP);
        }
        public Assigned_Annotations_ToUsers_TB Selected_Assigned_Annotation(int id, int UserID)
        {
            string ActiveUserID = HttpContext.Session.GetString("UserID");

            if (ActiveUserID == UserID.ToString())
            {
                var Selected_Annotation = _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB")
               .FirstOrDefault(m => m.Assigned_Anno_ID == id && m.User_ID == UserID);
                if (Selected_Annotation == null)
                {
                    return new Assigned_Annotations_ToUsers_TB();
                }
                return Selected_Annotation;
            }

            return new Assigned_Annotations_ToUsers_TB();
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
        public List<Labels_TB> Select_Annotation_Labels()
        {
            var Labels = _context.Labels_TB.Include("Projects_TB").Include("Labels_BG_Colours_TB").Where(m => m.Project_ID.ToString() == HttpContext.Session.GetString("ProjectID")).ToList();
            return Labels;
            //Select(c => new { Label_Text = c.Label_Text , Project_ID = c.Project_ID }).
            //.Select(c => new { Label_Text=c.Label_Text, Label_Background_Color = c.Labels_BG_Colours_TB.Label_Background_Color , Label_ShortCut_Key = c.Labels_BG_Colours_TB.Label_ShortCut_Key , Project_ID= c.Project_ID })
        }
        public async Task<IActionResult> Details(int? AnnotaionID)
        {           
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }
            
           
            var Selected_Annotation = await _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Where(m => m.Assigned_Anno_ID == AnnotaionID).FirstOrDefaultAsync();
         
            if (Selected_Annotation == null)
            {
                return NotFound();
            }

            // return RedirectToAction("Index", "Home");
            return  View(Selected_Annotation);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit2(CL_Users_Home_Page Assigned_Anno)
        //{
        //    Select_All_Users();
        //    Select_All_Annotations();
        //    Select_All_Projects();

        //    //if (!IsValidUser())
        //    //{
        //    //    return RedirectToAction("Index", "Login");
        //    //}

        //    //if (id != Assigned_Anno.Assigned_Anno_ID)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //if (await IsAssignedAnnoDuplicated(Assigned_Anno.Annotation_ID, Assigned_Anno.User_ID, Assigned_Anno.Project_ID))
        //    //{
        //    //    ViewBag.Error = "This Label is already registered for this Project";

        //    //}
        //    //else
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(Assigned_Anno.Selected_Assigned_Annotation);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                //if (!Assigned_Anno_Exists(Assigned_Anno.Assigned_Anno_ID))
        //                //{
        //                //    return NotFound();
        //                //}
        //                //else
        //                //{
        //                //    throw;
        //                //}
        //            }
        //            return RedirectToAction("Index","Home");
        //        }
        //    }
        //    return View(Assigned_Anno);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( CL_Users_Home_Page Assigned_Anno)
        {
            Select_All_Users();
            Select_All_Annotations();
            Select_All_Projects();
           

            //Select_All_Roles();
            //if (!IsValidUser())
            //{
            //    return RedirectToAction("Index", "Login");
            //}

            //if (id != Assigned_Anno.Selected_Assigned_Annotation.Assigned_Anno_ID)
            //{
            //    return NotFound();
            //}

            //if (await IsUserDuplicated(Assigned_Anno_Users.Username, Assigned_Anno_Users.Role_ID))
            //{
            //    ViewBag.Error = "This Username is already registered for this role";

            //}
            //else
            //{
            if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Assigned_Anno.Selected_Assigned_Annotation);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        //if (!AnnoExists(Assigned_Anno_Users.Assigned_Anno_ID))
                        //{
                        //    return NotFound();
                        //}
                        //else
                        //{
                        //    throw;
                        //}
                }
                    return RedirectToAction("Index", "Home");
                }
            //}
            return RedirectToAction("Index", "Home");
        }

        private bool AnnoExists(int id)
        {
            return _context.Assigned_Annotations_ToUsers_TB.Any(e => e.Assigned_Anno_ID == id);
        }

        public List<Users_TB> Select_All_Users()
        {           
            ViewBag.Users = _context.Users_TB.Where(m => m.Roles_TB.Role_Text.ToLower() != "admin").ToList();
            return ViewBag.Users;
        }
        public List<Annotations_TB> Select_All_Annotations()
        {            
            ViewBag.Annotations = _context.Annotations_TB.ToList();
            return ViewBag.Annotations;
        }
        public List<Projects_TB> Select_All_Projects()
        {
            ViewBag.Projects = _context.Projects_TB.ToList();
            return ViewBag.Projects;
        }
    }
}
