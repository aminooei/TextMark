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
    public class UserClassificationController : Controller
    {        
        private readonly TextMarkContext _context;
        int LoggedIn_User_ID = 0;
        int? Selected_Project_ID = 0;
        int Selected_Assigned_Classification_ID = 0;
      //  CL_UsersClassifications_Home_Page HP = new CL_UsersClassifications_Home_Page();

        public UserClassificationController(TextMarkContext context)
        {
            _context = context;            
        }
        
       
        public IActionResult Index(int Selected_Assigned_Classification_ID, int UserID)
        {           

            CL_UsersClassifications_Home_Page HP = new CL_UsersClassifications_Home_Page();
            HP.LoggedinUserID = UserID;
            HP.PageNum = 0;
            HP.NumRecordsInEachPage = 6;
            HP.TotalNumPages = 0;

            HP.allClassifications = All_Assigned_Classifications_ToUsers(HP.PageNum, HP.NumRecordsInEachPage, Selected_Project_ID);
            HP.allClassificationLabels =  Select_Classification_Labels();
            HP.Selected_Assigned_Classification =  Selected_Assigned_Classification(Selected_Assigned_Classification_ID, UserID);
            HP.ClassificationShortcutKeys_Press_Script = Create_ClassificationShortcutKeys_Press_Script(HP.allClassificationLabels);
            HP.all_ClassifiedText_Tags = Select_All_ClassifiedText_Tags(0);

            //await Select_All_Users();
            //await Select_All_Classifications();
            //Select_All_Projects();
            LoggedIn_User_ID = Convert.ToInt32( HttpContext.Session.GetString("UserID"));
            Select_All_Projects_of_LoggedInUser(LoggedIn_User_ID);

            return View(HP);
        }

      

        public IActionResult ViewProject(int Selected_Assigned_Cls_ID, int UserID, int Project_ID, int PageNum)
        {

            CL_UsersClassifications_Home_Page HP = new CL_UsersClassifications_Home_Page();
            HP.LoggedinUserID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            HP.SelectedProjectID = Project_ID;

            HP.PageNum = PageNum;

            HP.NumRecordsInEachPage = 6;
            HP.TotalNumPages = _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Where(m => m.User_ID == HP.LoggedinUserID && m.Project_ID == HP.SelectedProjectID).ToList().Count() / HP.NumRecordsInEachPage;

            if ((HP.TotalNumPages % HP.NumRecordsInEachPage) > 1)
            {
                HP.TotalNumPages += 1;
            }

            HP.allClassifications =  All_Assigned_Classifications_ToUsers(HP.PageNum, HP.NumRecordsInEachPage, HP.SelectedProjectID);
            HP.allClassificationLabels =  Select_Classification_Labels(Project_ID);
            HP.Selected_Assigned_Classification =  Selected_Assigned_Classification(Selected_Assigned_Cls_ID, UserID, Project_ID);
            HP.ClassificationShortcutKeys_Press_Script = Create_ClassificationShortcutKeys_Press_Script(HP.allClassificationLabels);
            HP.all_ClassifiedText_Tags =  Select_All_ClassifiedText_Tags(Selected_Assigned_Cls_ID);

            HttpContext.Session.SetString("Selected_Assigned_Anno_ID", Selected_Assigned_Cls_ID.ToString());
            HttpContext.Session.SetString("Selected_Project_ID", Project_ID.ToString());


            //await Select_All_Users();
            //await Select_All_Classifications();
            // Select_All_Projects();
            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Select_All_Projects_of_LoggedInUser(LoggedIn_User_ID);

            return  View("Index", HP);
        }

        public IActionResult ViewRecord_Next(int PageNum)
        {
            //await Select_All_Users();
            //await Select_All_Classifications();
            //await Select_All_Projects();
            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Selected_Assigned_Classification_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Assigned_Anno_ID"));
            Selected_Project_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Project_ID"));
            CL_UsersClassifications_Home_Page HP = new CL_UsersClassifications_Home_Page();
            HP.allClassifications = All_Assigned_Classifications_ToUsers(HP.PageNum, HP.NumRecordsInEachPage, Selected_Project_ID);
            HP.allClassificationLabels = Select_Classification_Labels(Selected_Project_ID);
           // HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation(Selected_Assigned_Anno_ID, LoggedIn_User_ID, Selected_Project_ID);
            HP.Selected_Assigned_Classification =  Selected_Assigned_Classification_AfterSave(Selected_Assigned_Classification_ID, LoggedIn_User_ID, Selected_Project_ID);
            HP.all_ClassifiedText_Tags = Select_All_ClassifiedText_Tags(Selected_Assigned_Classification_ID);

            HP.ClassificationShortcutKeys_Press_Script = Create_ClassificationShortcutKeys_Press_Script(HP.allClassificationLabels);

              Select_All_Projects_of_LoggedInUser(LoggedIn_User_ID);

            
           
            return RedirectToAction("ViewProject", new { Selected_Assigned_Cls_ID = (Selected_Assigned_Classification_ID + 1), UserID = LoggedIn_User_ID, Project_ID = Selected_Project_ID, PageNum = PageNum });
        }

        public IActionResult ViewRecord_Prev(int PageNum)
        {
            //await Select_All_Users();
            //await Select_All_Classifications();
            // Select_All_Projects();
            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Selected_Assigned_Classification_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Assigned_Anno_ID"));
            Selected_Project_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Project_ID"));
            CL_UsersClassifications_Home_Page HP = new CL_UsersClassifications_Home_Page();
            HP.allClassifications =  All_Assigned_Classifications_ToUsers(HP.PageNum, HP.NumRecordsInEachPage, Selected_Project_ID);
            HP.allClassificationLabels =  Select_Classification_Labels(Selected_Project_ID);
            // HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation(Selected_Assigned_Anno_ID, LoggedIn_User_ID, Selected_Project_ID);
            HP.Selected_Assigned_Classification =  Selected_Assigned_Classification_AfterSave(Selected_Assigned_Classification_ID, LoggedIn_User_ID, Selected_Project_ID);
            HP.all_ClassifiedText_Tags =  Select_All_ClassifiedText_Tags(Selected_Assigned_Classification_ID);

            HP.ClassificationShortcutKeys_Press_Script = Create_ClassificationShortcutKeys_Press_Script(HP.allClassificationLabels);

            Select_All_Projects_of_LoggedInUser(LoggedIn_User_ID);


           
            return RedirectToAction("ViewProject", new { Selected_Assigned_Cls_ID = (Selected_Assigned_Classification_ID - 1), UserID = LoggedIn_User_ID, Project_ID = Selected_Project_ID, PageNum = PageNum });
        }
        public IActionResult SaveRecord()
        {
            //await Select_All_Users();
            //await Select_All_Classifications();
            //await Select_All_Projects();
            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Selected_Assigned_Classification_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Assigned_Anno_ID"));
            Selected_Project_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Project_ID"));
            CL_UsersClassifications_Home_Page HP = new CL_UsersClassifications_Home_Page();
            HP.allClassifications =  All_Assigned_Classifications_ToUsers(HP.PageNum, HP.NumRecordsInEachPage, Selected_Project_ID);
            HP.allClassificationLabels =  Select_Classification_Labels(Selected_Project_ID);
            // HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation(Selected_Assigned_Anno_ID, LoggedIn_User_ID, Selected_Project_ID);
            HP.Selected_Assigned_Classification =  Selected_Assigned_Classification_AfterSave(Selected_Assigned_Classification_ID, LoggedIn_User_ID, Selected_Project_ID);

            HP.ClassificationShortcutKeys_Press_Script = Create_ClassificationShortcutKeys_Press_Script(HP.allClassificationLabels);

            Select_All_Projects_of_LoggedInUser(LoggedIn_User_ID);


            // return RedirectToAction("ViewProject", new { Selected_Assigned_Anno_ID = (Selected_Assigned_Anno_ID) , UserID = LoggedIn_User_ID,  Project_ID = Selected_Project_ID });
            return RedirectToAction("ViewProject", new { Selected_Assigned_Anno_ID = Selected_Assigned_Classification_ID, UserID = LoggedIn_User_ID, Project_ID = Selected_Project_ID });
        }
        public HtmlString Create_ClassificationShortcutKeys_Press_Script(List<ClassificationLabels_TB> List_ClassLabels)
        {
            string Code_STR = "";
            Code_STR += " <script> ";
            Code_STR += " let apparea = document.getElementById('apparea'); ";
            Code_STR += " apparea.addEventListener(\"keydown\", (e) => { ";
            foreach (var item in List_ClassLabels)
            {
                //Code_STR += " if (e.key == \""+ item.Labels_BG_Colours_TB.Label_ShortCut_Key.ToLower() +"\" || e.key == \""+ item.Labels_BG_Colours_TB.Label_ShortCut_Key.ToUpper() +"\")  App.handlers.applyOnclickAnnotation('"+ item.Label_Text  +"'); ";                       
                Code_STR += " if (e.key == \"" + item.ClassificationLabel_ShortCut_Key.ToLower() + "\" || e.key == \"" + item.ClassificationLabel_ShortCut_Key.ToUpper() + "\")  App.handlers.applyOnclickAnnotation('" + item.ClassificationLabel_Text + "'); ";
            }

            Code_STR += " else { $(\".example\").attr(\"contenteditable\", false); }";

            
            Code_STR += " }); ";

            Code_STR += " apparea.addEventListener(\"mousedown\", (e) => { ";
            Code_STR += " $(\".example\").attr(\"contenteditable\", true); ";
            Code_STR += " }); ";
            Code_STR += "  </script> ";
            return new HtmlString(Code_STR);
        }
        public Assigned_TextClassifications_ToUsers_TB Selected_Assigned_Classification(int id, int UserID, int? Project_ID = 0 )
        {
            string ActiveUserID = HttpContext.Session.GetString("UserID");

            if (ActiveUserID == UserID.ToString())
            {
                var Selected_Classification =  _context.Assigned_TextClassifications_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB")
               .FirstOrDefault(m => m.Assigned_TextClassification_ID == id && m.User_ID == UserID && m.Project_ID == Project_ID);
                if (Selected_Classification == null)
                {
                    return new Assigned_TextClassifications_ToUsers_TB();
                }
                return Selected_Classification;
            }

            return new Assigned_TextClassifications_ToUsers_TB();
        }

        public Assigned_TextClassifications_ToUsers_TB Selected_Assigned_Classification_AfterSave(int id, int UserID, int? Project_ID = 0)
        {
            string ActiveUserID = HttpContext.Session.GetString("UserID");

            if (ActiveUserID == UserID.ToString())
            {
                var Selected_Annotation = _context.Assigned_TextClassifications_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB")
               .FirstOrDefault(m => m.Assigned_TextClassification_ID == (id+1) && m.User_ID == UserID && m.Project_ID == Project_ID);
                if (Selected_Annotation == null)
                {
                    return new Assigned_TextClassifications_ToUsers_TB();
                }
                return Selected_Annotation;
            }

            return new Assigned_TextClassifications_ToUsers_TB();
        }
        private IPagedList<Assigned_TextClassifications_ToUsers_TB> All_Assigned_Classifications_ToUsers(int PageNum, int PageSize, int? Project_ID = 0)
        {
            CL_UsersClassifications_Home_Page HP = new CL_UsersClassifications_Home_Page();

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
            if (PageNum > 0)
            {
                return _context.Assigned_TextClassifications_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Where(m => m.User_ID.ToString() == UserID && m.Project_ID == Project_ID).ToPagedList(PageNum, PageSize);
            }
            else
            {
                return _context.Assigned_TextClassifications_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Where(m => m.User_ID.ToString() == UserID && m.Project_ID == Project_ID).ToPagedList(1, 1);
            }

            

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
        public List<ClassificationLabels_TB> Select_Classification_Labels(int? Project_ID = 0)
        {
            var ClassificationLabels = _context.ClassificationLabels_TB.Include("Projects_TB").Where(m => m.Project_ID == Project_ID).ToList();
            //var Labels = _context.Labels_TB.Include("Projects_TB").Include("Labels_BG_Colours_TB").Where(m => m.Project_ID.ToString() == HttpContext.Session.GetString("ProjectID")).ToList();
            return ClassificationLabels;
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

        public List<ClassifiedTexts_Tags> Select_All_ClassifiedText_Tags(int? RecID)
        {               
            return _context.ClassifiedTexts_Tags.Include("Assigned_TextClassifications_ToUsers_TB").Include("ClassificationLabels_TB").Where(m => m.Assigned_TextClassification_ID == RecID).ToList();           
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
        public IActionResult Edit( CL_UsersClassifications_Home_Page Assigned_Anno)
        {
             Select_All_Users();
             Select_All_Classifications();
            // Select_All_Projects();
          

            
            if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Assigned_Anno.Selected_Assigned_Classification);
                        _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                       
                    }

                return RedirectToAction("ViewRecord_AfterSave", "UserClassification");
             
            }
          
        return RedirectToAction("Index", "UserClassification");
            
        }

        public async Task<IActionResult> Save(CL_UsersClassifications_Home_Page Assigned_Anno)
        {
            //await Select_All_Users();
            //await Select_All_Classifications();
            //await Select_All_Projects();



            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Assigned_Anno.Selected_Assigned_Classification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }

                return RedirectToAction("SaveRecord", "UserClassification");

            }

            return RedirectToAction("Index", "UserClassification");

        }

        public async Task<IActionResult> Save_Comment(CL_UsersClassifications_Home_Page Assigned_Anno)
        {
            try
            {      
                _context.Update(Assigned_Anno.Selected_Assigned_Classification);
                await _context.SaveChangesAsync();


            }
            catch (DbUpdateConcurrencyException)
            {

            }

            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Selected_Assigned_Classification_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Assigned_Anno_ID"));
            Selected_Project_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Project_ID"));
            return RedirectToAction("ViewProject", new { Selected_Assigned_Anno_ID = Selected_Assigned_Classification_ID, UserID = LoggedIn_User_ID, Project_ID = Selected_Project_ID });

           

        }
       
        public async Task<IActionResult> Save_NotSure(CL_UsersClassifications_Home_Page Assigned_Anno)
        {
            try
            {
                _context.Update(Assigned_Anno.Selected_Assigned_Classification);
                await _context.SaveChangesAsync();


            }
            catch (DbUpdateConcurrencyException)
            {

            }

            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Selected_Assigned_Classification_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Assigned_Anno_ID"));
            Selected_Project_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Project_ID"));
            return RedirectToAction("ViewProject", new { Selected_Assigned_Anno_ID = Selected_Assigned_Classification_ID, UserID = LoggedIn_User_ID, Project_ID = Selected_Project_ID });



        }

        public async Task<IActionResult> Save_TextsTags(CL_UsersClassifications_Home_Page Assigned_Anno)
        {
            try
            {
                ClassifiedTexts_Tags tb = new ClassifiedTexts_Tags();
                tb.Assigned_TextClassification_ID = Assigned_Anno.Selected_Assigned_Classification.Assigned_TextClassification_ID;
                tb.ClassificationLabel_ID = Convert.ToInt32(Assigned_Anno.Selected_Assigned_Classification.TextClassification_HtmlTags);
                _context.Add(tb);
                await _context.SaveChangesAsync();


                var Count_Classified_Tags = await _context.ClassifiedTexts_Tags.Where(m => m.Assigned_TextClassification_ID == Assigned_Anno.Selected_Assigned_Classification.Assigned_TextClassification_ID).CountAsync();
                Assigned_Anno.Selected_Assigned_Classification.Count_Classifications = Count_Classified_Tags;
                _context.Update(Assigned_Anno.Selected_Assigned_Classification);
                await _context.SaveChangesAsync();


            }
            catch (DbUpdateConcurrencyException)
                {

                }

            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Selected_Assigned_Classification_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Assigned_Anno_ID"));
            Selected_Project_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Project_ID"));
            return RedirectToAction("ViewProject", new { Selected_Assigned_Anno_ID = Selected_Assigned_Classification_ID, UserID = LoggedIn_User_ID, Project_ID = Selected_Project_ID });

            //   return RedirectToAction("SaveRecord", "UserClassification");

            //ViewProject(int Selected_Assigned_Anno_ID, int UserID, int Project_ID)

        }

        public async Task<IActionResult> Delete_TextsTags(CL_UsersClassifications_Home_Page Assigned_Anno)
        {
                      
            try
            {
            //    ClassifiedTexts_Tags tb = new ClassifiedTexts_Tags();
            //    tb.Assigned_TextClassification_ID = Assigned_Anno.Selected_Assigned_Classification.Assigned_TextClassification_ID;
            //    tb.ClassificationLabel_ID = Convert.ToInt32(Assigned_Anno.Selected_Assigned_Classification.TextClassification_HtmlTags);

                var Selected_tag = await _context.ClassifiedTexts_Tags.Where(m => m.Assigned_TextClassification_ID == Assigned_Anno.Selected_Assigned_Classification.Assigned_TextClassification_ID && m.ClassificationLabel_ID == Convert.ToInt32(Assigned_Anno.Selected_Assigned_Classification.TextClassification_HtmlTags)).FirstOrDefaultAsync();

                var tag = await _context.ClassifiedTexts_Tags.FindAsync(Selected_tag.ID);
                _context.ClassifiedTexts_Tags.Remove(tag);              
                await _context.SaveChangesAsync();

                var Count_Classified_Tags = await _context.ClassifiedTexts_Tags.Where(m => m.Assigned_TextClassification_ID == Assigned_Anno.Selected_Assigned_Classification.Assigned_TextClassification_ID).CountAsync();
                Assigned_Anno.Selected_Assigned_Classification.Count_Classifications = Count_Classified_Tags;
                _context.Update(Assigned_Anno.Selected_Assigned_Classification);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            LoggedIn_User_ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            Selected_Assigned_Classification_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Assigned_Anno_ID"));
            Selected_Project_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_Project_ID"));
            return RedirectToAction("ViewProject", new { Selected_Assigned_Anno_ID = Selected_Assigned_Classification_ID, UserID = LoggedIn_User_ID, Project_ID = Selected_Project_ID });




        }
        private bool AnnoExists(int id)
        {
            return  _context.Assigned_Annotations_ToUsers_TB.Any(e => e.Assigned_Anno_ID == id);
        }

        public List<Users_TB> Select_All_Users()
        {           
            ViewBag.Users =  _context.Users_TB.Where(m => m.Roles_TB.Role_Text.ToLower() != "admin").ToList();
            return ViewBag.Users;
        }
        public List<Annotations_TB> Select_All_Classifications()
        {            
            ViewBag.Classifcations =  _context.Annotations_TB.ToList();
            return ViewBag.Classifcations;
        }
        //public async Task<List<Projects_TB>> Select_All_Projects()
        //{
        //    ViewBag.Projects = await _context.Projects_TB.ToListAsync();
        //    return ViewBag.Projects;
        //}
        //public List<Assigned_Annotations_ToUsers_TB> Select_All_Projects_of_LoggedInUser(int userID)
        public  void Select_All_Projects_of_LoggedInUser(int userID)
        {
            ViewBag.UserProjects =  _context.Assigned_Annotations_ToUsers_TB.Where(x => x.User_ID == userID).Select(c => new { Project_ID = c.Project_ID , Project_Name = c.Projects_TB.Project_Name }).Distinct().ToList();
           // return ViewBag.UserProjects;
        }    
    }
}
