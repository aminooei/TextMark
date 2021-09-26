﻿using System;
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
using Microsoft.AspNetCore.Html;

namespace TextMark.Controllers
{
    public class HomeController : Controller
    {        
        private readonly TextMarkContext _context;
        int LoggedIn_User_ID = 0;
        int? Selected_Project_ID = 0;
        int Selected_Assigned_Anno_ID = 0;
        

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
            HP.ShortcutKeys_Press_Script = Create_ShortcutKeys_Press_Script(HP.allLabels);

            Select_All_Users();
            Select_All_Annotations();
            Select_All_Projects();
            LoggedIn_User_ID = Convert.ToInt32( HttpContext.Session.GetString("UserID"));
            Select_All_Projects_of_LoggedInUser(LoggedIn_User_ID);

            return View(HP);
        }

       
        public ActionResult ViewProject(int Selected_Assigned_Anno_ID, int UserID, int Project_ID)
        {

            CL_Users_Home_Page HP = new CL_Users_Home_Page();
            HP.allAnnotations = All_Assigned_Anno_ToUsers(Project_ID);
            HP.allLabels = Select_Annotation_Labels(Project_ID);
            HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation(Selected_Assigned_Anno_ID, UserID, Project_ID);
            HP.ShortcutKeys_Press_Script = Create_ShortcutKeys_Press_Script(HP.allLabels);

            HttpContext.Session.SetString("Selected_Assigned_Anno_ID", Selected_Assigned_Anno_ID.ToString());
            HttpContext.Session.SetString("Selected_Project_ID", Project_ID.ToString());


            Select_All_Users();
            Select_All_Annotations();
            Select_All_Projects();
            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Select_All_Projects_of_LoggedInUser(LoggedIn_User_ID);

            return  View("Index", HP);
        }

        public ActionResult ViewRecord_AfterSave()
        {    
            Select_All_Users();
            Select_All_Annotations();
            Select_All_Projects();
            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Selected_Assigned_Anno_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Assigned_Anno_ID"));
            Selected_Project_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Project_ID"));
            CL_Users_Home_Page HP = new CL_Users_Home_Page();
            HP.allAnnotations = All_Assigned_Anno_ToUsers(Selected_Project_ID);
            HP.allLabels = Select_Annotation_Labels(Selected_Project_ID);
            HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation(Selected_Assigned_Anno_ID, LoggedIn_User_ID, Selected_Project_ID);
            HP.ShortcutKeys_Press_Script = Create_ShortcutKeys_Press_Script(HP.allLabels);

            Select_All_Projects_of_LoggedInUser(LoggedIn_User_ID);

            //   return View("Index", HP);
            return RedirectToAction("ViewProject", new { Selected_Assigned_Anno_ID = Selected_Assigned_Anno_ID, UserID = LoggedIn_User_ID,  Project_ID = Selected_Project_ID });
        }
        public HtmlString Create_ShortcutKeys_Press_Script(List<Labels_TB> List_Labels)
        {
            string Code_STR = "";
            Code_STR += " <script> ";
            Code_STR += " let apparea = document.getElementById('apparea'); ";
            Code_STR += " apparea.addEventListener(\"keydown\", (e) => { ";
            foreach (var item in List_Labels)
            {
                Code_STR += " if (e.key == \""+ item.Labels_BG_Colours_TB.Label_ShortCut_Key.ToLower() +"\" || e.key == \""+ item.Labels_BG_Colours_TB.Label_ShortCut_Key.ToUpper() +"\")  App.handlers.applyOnclickAnnotation('"+ item.Label_Text  +"'); ";                       
            }

            Code_STR += " else { $(\".example\").attr(\"contenteditable\", false); }";

            
            Code_STR += " }); ";

            Code_STR += " apparea.addEventListener(\"mousedown\", (e) => { ";
            Code_STR += " $(\".example\").attr(\"contenteditable\", true); ";
            Code_STR += " }); ";
            Code_STR += "  </script> ";
            return new HtmlString(Code_STR);
        }
        public Assigned_Annotations_ToUsers_TB Selected_Assigned_Annotation(int id, int UserID, int? Project_ID = 0 )
        {
            string ActiveUserID = HttpContext.Session.GetString("UserID");

            if (ActiveUserID == UserID.ToString())
            {
                var Selected_Annotation = _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB")
               .FirstOrDefault(m => m.Assigned_Anno_ID == id && m.User_ID == UserID && m.Project_ID == Project_ID);
                if (Selected_Annotation == null)
                {
                    return new Assigned_Annotations_ToUsers_TB();
                }
                return Selected_Annotation;
            }

            return new Assigned_Annotations_ToUsers_TB();
        }
        private List<Assigned_Annotations_ToUsers_TB> All_Assigned_Anno_ToUsers(int? Project_ID = 0)
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
            return _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Where(m => m.User_ID.ToString() == UserID && m.Project_ID == Project_ID).ToList();

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
        public List<Labels_TB> Select_Annotation_Labels(int? Project_ID = 0)
        {
            var Labels = _context.Labels_TB.Include("Projects_TB").Include("Labels_BG_Colours_TB").Where(m => m.Project_ID == Project_ID).ToList();
            //var Labels = _context.Labels_TB.Include("Projects_TB").Include("Labels_BG_Colours_TB").Where(m => m.Project_ID.ToString() == HttpContext.Session.GetString("ProjectID")).ToList();
            return Labels;
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
          

            
            if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Assigned_Anno.Selected_Assigned_Annotation);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                       
                }

                return RedirectToAction("ViewRecord_AfterSave", "Home");
             
            }
          
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
        //public List<Assigned_Annotations_ToUsers_TB> Select_All_Projects_of_LoggedInUser(int userID)
        public void Select_All_Projects_of_LoggedInUser(int userID)
        {
            /// var UserRecords = _context.Assigned_Annotations_ToUsers_TB.Where(x => x.User_ID == userID).ToList();

            //ViewBag.UserProjects = UserRecords.GroupBy(t => new { Project_ID = t.Project_ID , Project_Name = t.Projects_TB.Project_Name }).Select(b => b).ToList();


            //ViewBag.UserProjects = _context.Assigned_Annotations_ToUsers_TB.GroupBy(c => new { Project_ID = c.Project_ID, User_ID = c.User_ID, Project_Name = c.Projects_TB.Project_Name }).Where(d => d.Key.User_ID == userID).ToList();
          ViewBag.UserProjects = _context.Assigned_Annotations_ToUsers_TB.Where(x => x.User_ID == userID).Select(c => new { Project_ID = c.Project_ID , Project_Name = c.Projects_TB.Project_Name }).Distinct().ToList();

        }    
    }
}
