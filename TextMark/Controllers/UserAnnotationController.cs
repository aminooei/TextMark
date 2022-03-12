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
using Microsoft.AspNetCore.Html;
using PagedList;

namespace TextMark.Controllers
{
    public class UserAnnotationController : Controller
    {        
        private readonly TextMarkContext _context;
        int LoggedIn_User_ID = 0;
        int? Selected_Project_ID = 0;
        int Selected_Assigned_Anno_ID = 0;
        CL_UsersAnnotations_Home_Page HP = new CL_UsersAnnotations_Home_Page();

        public UserAnnotationController(TextMarkContext context)
        {
            _context = context;
           
        }
        
       
        public IActionResult Index(int Selected_Assigned_Anno_ID, int UserID)
        {
            var Active_ProjectID = HttpContext.Session.GetString("Active_ProjectID");

 //           CL_UsersAnnotations_Home_Page HP = new CL_UsersAnnotations_Home_Page();
            HP.allAnnotations = All_Assigned_Anno_ToUsers(HP.PageNum);
            HP.allLabels = Select_Annotation_Labels();
            HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation(Selected_Assigned_Anno_ID, UserID);
            HP.ShortcutKeys_Press_Script = Create_ShortcutKeys_Press_Script(HP.allLabels);

            //await Select_All_Users();
            //await Select_All_Annotations();
            Select_All_Projects();
            LoggedIn_User_ID = Convert.ToInt32( HttpContext.Session.GetString("UserID"));
            Select_All_Projects_of_LoggedInUser(LoggedIn_User_ID);

            return View(HP);
        }

        public IActionResult ViewRecord_Next(int PageNum)
        {
            //await Select_All_Users();
            //await Select_All_Classifications();
            Select_All_Projects();
            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Selected_Assigned_Anno_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Assigned_Anno_ID"));
            Selected_Project_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Project_ID"));
//          CL_UsersAnnotations_Home_Page HP = new CL_UsersAnnotations_Home_Page();
            HP.allAnnotations = All_Assigned_Anno_ToUsers(HP.PageNum, Selected_Project_ID);
            HP.allLabels = Select_Annotation_Labels(Selected_Project_ID);
            // HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation(Selected_Assigned_Anno_ID, LoggedIn_User_ID, Selected_Project_ID);
            HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation_AfterSave(Selected_Assigned_Anno_ID, LoggedIn_User_ID, Selected_Project_ID);
           // HP.all_ClassifiedText_Tags = await Select_All_ClassifiedText_Tags(Selected_Assigned_Classification_ID);

            HP.ShortcutKeys_Press_Script = Create_ShortcutKeys_Press_Script(HP.allLabels);

            Select_All_Projects_of_LoggedInUser(LoggedIn_User_ID);



            return RedirectToAction("ViewProject", new { Selected_Assigned_Anno_ID = (Selected_Assigned_Anno_ID + 1), UserID = LoggedIn_User_ID, Project_ID = Selected_Project_ID, PageNum = PageNum });
        }

        public IActionResult Delete_TextsAnnotations(int Assigned_TextAnnotation_ID, string Annotation_ID_InText)
        {

            try
            {
                //    ClassifiedTexts_Tags tb = new ClassifiedTexts_Tags();
                //    tb.Assigned_TextClassification_ID = Assigned_Anno.Selected_Assigned_Classification.Assigned_TextClassification_ID;
                //    tb.ClassificationLabel_ID = Convert.ToInt32(Assigned_Anno.Selected_Assigned_Classification.TextClassification_HtmlTags);


                // Edit Here Amin
                var Selected_tag = _context.AnnotatedTexts_Tags.Where(m => (m.Assigned_TextAnnotation_ID == Assigned_TextAnnotation_ID && m.Annotation_ID_InText == Annotation_ID_InText)).FirstOrDefault();

                var tag = _context.AnnotatedTexts_Tags.Find(Selected_tag.ID);
                _context.AnnotatedTexts_Tags.Remove(tag);
                _context.SaveChangesAsync();

                //var Count_Annotated_Tags = await _context.AnnotatedTexts_Tags.Where(m => m.Assigned_TextAnnotation_ID == Assigned_Anno.Selected_Assigned_Annotation.Assigned_Anno_ID).CountAsync();
                //Assigned_Anno.Selected_Assigned_Annotation.Count_Annotations = Count_Annotated_Tags;
                //_context.Update(Assigned_Anno.Selected_Assigned_Annotation);
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Selected_Assigned_Anno_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Assigned_Anno_ID"));
            Selected_Project_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Project_ID"));
            return RedirectToAction("ViewProject", new { Selected_Assigned_Anno_ID = Selected_Assigned_Anno_ID, UserID = LoggedIn_User_ID, Project_ID = Selected_Project_ID });




        }
        public IActionResult ViewRecord_Prev(int PageNum)
        {
            //await Select_All_Users();
            //await Select_All_Classifications();
            Select_All_Projects();
            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Selected_Assigned_Anno_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Assigned_Anno_ID"));
            Selected_Project_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Project_ID"));
//            CL_UsersAnnotations_Home_Page HP = new CL_UsersAnnotations_Home_Page();
            HP.allAnnotations = All_Assigned_Anno_ToUsers(HP.PageNum, Selected_Project_ID);
            HP.allLabels = Select_Annotation_Labels(Selected_Project_ID);
            // HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation(Selected_Assigned_Anno_ID, LoggedIn_User_ID, Selected_Project_ID);
            HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation_AfterSave(Selected_Assigned_Anno_ID, LoggedIn_User_ID, Selected_Project_ID);
          //  HP.all_ClassifiedText_Tags = await Select_All_ClassifiedText_Tags(Selected_Assigned_Classification_ID);

            HP.ShortcutKeys_Press_Script = Create_ShortcutKeys_Press_Script(HP.allLabels);

            Select_All_Projects_of_LoggedInUser(LoggedIn_User_ID);



            return RedirectToAction("ViewProject", new { Selected_Assigned_Anno_ID = (Selected_Assigned_Anno_ID - 1), UserID = LoggedIn_User_ID, Project_ID = Selected_Project_ID, PageNum = PageNum });
        }

        public IActionResult ViewProject(int Selected_Assigned_Anno_ID, int UserID, int Project_ID, int PageNum)
        {           
//            CL_UsersAnnotations_Home_Page HP = new CL_UsersAnnotations_Home_Page();
            HP.LoggedinUserID = UserID;
            HP.SelectedProjectID = Project_ID;
            HP.PageNum = PageNum;
            HP.allAnnotations = All_Assigned_Anno_ToUsers(HP.PageNum, HP.SelectedProjectID);
            
            HP.allLabels = Select_Annotation_Labels(HP.SelectedProjectID);
            HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation(Selected_Assigned_Anno_ID, HP.LoggedinUserID, HP.SelectedProjectID);
            HP.ShortcutKeys_Press_Script = Create_ShortcutKeys_Press_Script(HP.allLabels);

            HttpContext.Session.SetString("Selected_Assigned_Anno_ID", Selected_Assigned_Anno_ID.ToString());
            HttpContext.Session.SetString("Selected_Project_ID", HP.SelectedProjectID.ToString());




            //await Select_All_Users();
            //await Select_All_Annotations();
            // Select_All_Projects();
            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            
            Select_All_Projects_of_LoggedInUser(LoggedIn_User_ID);
           
            return  View("Index", HP);
        }

        public IActionResult ViewRecord_AfterSave()
        {
            Select_All_Users();
            Select_All_Annotations();
            Select_All_Projects();
            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Selected_Assigned_Anno_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Assigned_Anno_ID"));
            Selected_Project_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Project_ID"));
 //           CL_UsersAnnotations_Home_Page HP = new CL_UsersAnnotations_Home_Page();
            HP.allAnnotations = All_Assigned_Anno_ToUsers(HP.PageNum, Selected_Project_ID);
            HP.allLabels = Select_Annotation_Labels(Selected_Project_ID);
           // HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation(Selected_Assigned_Anno_ID, LoggedIn_User_ID, Selected_Project_ID);
            HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation_AfterSave(Selected_Assigned_Anno_ID, LoggedIn_User_ID, Selected_Project_ID);

            HP.ShortcutKeys_Press_Script = Create_ShortcutKeys_Press_Script(HP.allLabels);

            Select_All_Projects_of_LoggedInUser(LoggedIn_User_ID);

            
           // return RedirectToAction("ViewProject", new { Selected_Assigned_Anno_ID = (Selected_Assigned_Anno_ID) , UserID = LoggedIn_User_ID,  Project_ID = Selected_Project_ID });
            return RedirectToAction("ViewProject", new { Selected_Assigned_Anno_ID = (Selected_Assigned_Anno_ID + 1), UserID = LoggedIn_User_ID, Project_ID = Selected_Project_ID });
        }

        public  void Insert_TagData_InDB(int Assigned_TextAnnotation_ID, int AnnotationLabel_ID, int Label_Start_Index, int Label_End_Index, string Annotated_Substring, string Annotation_ID_InText)
        {
            AnnotatedTexts_Tags TagData = new AnnotatedTexts_Tags { Assigned_TextAnnotation_ID = Assigned_TextAnnotation_ID, AnnotationLabel_ID = AnnotationLabel_ID, Label_Start_Index = Label_Start_Index, Label_End_Index = Label_End_Index , Annotated_Substring = Annotated_Substring , Annotation_ID_InText = Annotation_ID_InText };
            _context.Add(TagData);
            _context.SaveChanges();            
        }
        public IActionResult SaveRecord()
        {
            //await Select_All_Users();
            //await Select_All_Annotations();
            //Select_All_Projects();
           


            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Selected_Assigned_Anno_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Assigned_Anno_ID"));
            Selected_Project_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Project_ID"));
 //           CL_UsersAnnotations_Home_Page HP = new CL_UsersAnnotations_Home_Page();
            HP.allAnnotations = All_Assigned_Anno_ToUsers(HP.PageNum, Selected_Project_ID);
            HP.allLabels = Select_Annotation_Labels(Selected_Project_ID);
            // HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation(Selected_Assigned_Anno_ID, LoggedIn_User_ID, Selected_Project_ID);
            HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation_AfterSave(Selected_Assigned_Anno_ID, LoggedIn_User_ID, Selected_Project_ID);

            HP.ShortcutKeys_Press_Script = Create_ShortcutKeys_Press_Script(HP.allLabels);

            Select_All_Projects_of_LoggedInUser(LoggedIn_User_ID);


            // return RedirectToAction("ViewProject", new { Selected_Assigned_Anno_ID = (Selected_Assigned_Anno_ID) , UserID = LoggedIn_User_ID,  Project_ID = Selected_Project_ID });
            return RedirectToAction("ViewProject", new { Selected_Assigned_Anno_ID = (Selected_Assigned_Anno_ID), UserID = LoggedIn_User_ID, Project_ID = Selected_Project_ID, PageNum = HP.PageNum });
        }
        public HtmlString Create_ShortcutKeys_Press_Script(List<Labels_TB> List_Labels)
        {
            string Code_STR = "";
            Code_STR += " <script> ";
            Code_STR += " let apparea = document.getElementById('apparea'); ";
            Code_STR += " apparea.addEventListener(\"keydown\", (e) => { ";
            foreach (var item in List_Labels)
            {
                //Code_STR += " if (e.key == \""+ item.Labels_BG_Colours_TB.Label_ShortCut_Key.ToLower() +"\" || e.key == \""+ item.Labels_BG_Colours_TB.Label_ShortCut_Key.ToUpper() +"\")  App.handlers.applyOnclickAnnotation('"+ item.Label_Text  +"'); ";                       
                Code_STR += " if (e.key == \"" + item.Label_ShortCut_Key.ToLower() + "\" || e.key == \"" + item.Label_ShortCut_Key.ToUpper() + "\")  App.handlers.applyOnclickAnnotation('" + item.Label_Text + "'); ";
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

        public Assigned_Annotations_ToUsers_TB Selected_Assigned_Annotation_AfterSave(int id, int UserID, int? Project_ID = 0)
        {
            string ActiveUserID = HttpContext.Session.GetString("UserID");

            if (ActiveUserID == UserID.ToString())
            {
                var Selected_Annotation = _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB")
               .FirstOrDefault(m => m.Assigned_Anno_ID == (id+1) && m.User_ID == UserID && m.Project_ID == Project_ID);
                if (Selected_Annotation == null)
                {
                    return new Assigned_Annotations_ToUsers_TB();
                }
                return Selected_Annotation;
            }

            return new Assigned_Annotations_ToUsers_TB();
        }
        private IPagedList<Assigned_Annotations_ToUsers_TB> All_Assigned_Anno_ToUsers(int PageNum, int? Project_ID = 0)
        {
            if (PageNum == 0)
            {
                HP.PageNum = 1;
            }

            var UserID = "";

            if (!IsValidUser())
            {
                //return RedirectToAction("Index", "Login");
            }
            if (HttpContext.Session.GetString("UserID") != null)
            {
                UserID = HttpContext.Session.GetString("UserID").ToUpper();
            }

            HP.Selected_UserID = Convert.ToInt32(UserID);
            HP.NumRecordsInEachPage = 6;
            HP.TotalNumPages = _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Where(m => m.User_ID.ToString() == UserID && m.Project_ID == Project_ID).ToList().Count() / HP.NumRecordsInEachPage;
            if ((HP.TotalNumPages % HP.NumRecordsInEachPage) > 1)
            {
                HP.TotalNumPages += 1;
            }
            return  _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Where(m => m.User_ID.ToString() == UserID && m.Project_ID == Project_ID).ToPagedList(HP.PageNum, HP.NumRecordsInEachPage);

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
            var Labels =  _context.Labels_TB.Include("Projects_TB").Where(m => m.Project_ID == Project_ID).ToList();
            //var Labels = _context.Labels_TB.Include("Projects_TB").Include("Labels_BG_Colours_TB").Where(m => m.Project_ID.ToString() == HttpContext.Session.GetString("ProjectID")).ToList();
            return Labels;
            }
        public IActionResult Details(int? AnnotaionID)
        {           
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }
            
           
            var Selected_Annotation = _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Where(m => m.Assigned_Anno_ID == AnnotaionID).FirstOrDefault();
         
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

        public async Task<IActionResult> Save_Comment(CL_UsersAnnotations_Home_Page Assigned_Anno)
        {
            try
            {
                _context.Update(Assigned_Anno.Selected_Assigned_Annotation);
                await _context.SaveChangesAsync();


            }
            catch (DbUpdateConcurrencyException)
            {

            }

            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Selected_Assigned_Anno_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Assigned_Anno_ID"));
            Selected_Project_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Project_ID"));
            return RedirectToAction("ViewProject", new { Selected_Assigned_Anno_ID = Selected_Assigned_Anno_ID, UserID = LoggedIn_User_ID, Project_ID = Selected_Project_ID });



        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> GoNext(CL_UsersAnnotations_Home_Page Assigned_Anno)
        {
         //   await Select_All_Users();
         //   await Select_All_Annotations();
         //   await Select_All_Projects();
          

            
            //if (ModelState.IsValid)
            //    {
            //        try
            //        {
                        _context.Update(Assigned_Anno.Selected_Assigned_Annotation);
                        await _context.SaveChangesAsync();
                    //}
                    //catch (DbUpdateConcurrencyException)
                    //{
                       
                    //}

                return RedirectToAction("ViewRecord_AfterSave", "UserAnnotation");
             
           // }
          
       // return RedirectToAction("Index", "UserAnnotation");
            
        }

        //public async Task<IActionResult> Save(CL_UsersAnnotations_Home_Page Assigned_Anno)
        public  void Save(CL_UsersAnnotations_Home_Page Assigned_Anno)
        {
             //Select_All_Users();
             //Select_All_Annotations();
             //Select_All_Projects();



            //if (ModelState.IsValid)
            //{
            //    try
            //    {
                    _context.Update(Assigned_Anno.Selected_Assigned_Annotation);
                     _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {

            //    }

            //  //  return RedirectToAction("SaveRecord", "UserAnnotation");

            //}

         //   return RedirectToAction("Index", "UserAnnotation");

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
        public  void Select_All_Projects_of_LoggedInUser(int userID)
        {
            ViewBag.UserProjects =  _context.Assigned_Annotations_ToUsers_TB.Where(x => x.User_ID == userID).Select(c => new { Project_ID = c.Project_ID , Project_Name = c.Projects_TB.Project_Name }).Distinct().ToList();
           // return ViewBag.UserProjects;
        }    
    }
}
