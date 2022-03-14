using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TextMark.Data;
using TextMark.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PagedList;

namespace TextMark.Controllers
{
    //[Authorize]
    public class Admin_Assigned_TextClassificationsTBController : Controller
    {
        private readonly TextMarkContext _context;
       
        public Admin_Assigned_TextClassificationsTBController(TextMarkContext context)
        {
            _context = context;           
        }

        public ViewResult Index(int PageNum)
        {
            if (PageNum == 0)
            {
                PageNum = 1;
            }

            Details_Assigned_TextClassifications_ToUsers DT = new Details_Assigned_TextClassifications_ToUsers();
            DT.PageNum = PageNum;
            //if (!IsValidUser())
            //{
            //    return RedirectToAction("Index", "Login");
            //}

            int Active_ProjectID = Convert.ToInt32(HttpContext.Session.GetString("Active_ProjectID"));
            int Selected_User_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_User_ID"));
            Select_All_Users();

            if (Selected_User_ID == 0)
            {
                DT.TotalNumPages = _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == 0 && m.User_ID == 0).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToList().Count() / 10;
                if ((DT.TotalNumPages % 10) > 1)
                {
                    DT.TotalNumPages += 1;
                }
                //return View(await _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == 0 && m.User_ID == 0).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync());
                DT.allClassifications = _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == 0 && m.User_ID == 0).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToPagedList(PageNum, 10);
                DT.ClassifiedTexts_Tags = Select_All_Classified_Tags(Selected_User_ID, Active_ProjectID);
                return View(DT);
            }
            else if (Selected_User_ID == 2) //All Users
            {
                DT.Selected_UserID = Selected_User_ID;
                DT.TotalNumPages = _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToList().Count() / 10;
                if ((DT.TotalNumPages % 10) > 1)
                {
                    DT.TotalNumPages += 1;
                }
                //return View(await _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID && m.User_ID == Selected_User_ID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync());
                DT.allClassifications = _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToPagedList(PageNum, 10);
                DT.ClassifiedTexts_Tags = Select_All_Classified_Tags(Selected_User_ID, Active_ProjectID);
                return View(DT);
            }
            else
            {
                DT.Selected_UserID = Selected_User_ID;
                DT.TotalNumPages = _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID && m.User_ID == Selected_User_ID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToList().Count() / 10;
                if ((DT.TotalNumPages % 10) > 1)
                {
                    DT.TotalNumPages += 1;
                }
                //return View(await _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID && m.User_ID == Selected_User_ID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync());
                DT.allClassifications = _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID && m.User_ID == Selected_User_ID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToPagedList(PageNum, 10);
                DT.ClassifiedTexts_Tags = Select_All_Classified_Tags(Selected_User_ID, Active_ProjectID);
                return View(DT);
            }
           
        }

        [HttpPost]
        public ViewResult Index(int User_ID, int PageNum)
        {
            if (PageNum == 0)
            {
                PageNum = 1;
            }

            var Active_ProjectID = Convert.ToInt32(HttpContext.Session.GetString("Active_ProjectID"));

            Details_Assigned_TextClassifications_ToUsers DT = new Details_Assigned_TextClassifications_ToUsers();
            DT.PageNum = PageNum;
            {
                HttpContext.Session.SetString("Selected_User_ID", User_ID.ToString());
            }
            int Active_UserID = Convert.ToInt32(HttpContext.Session.GetString("Selected_User_ID"));
            //if (!IsValidUser())
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            Select_All_Users();
            Select_All_Projects();
            if (Active_UserID == 2) //All Users
            {
                DT.Selected_UserID = Active_UserID;
                DT.TotalNumPages = _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToList().Count() / 10;
                if ((DT.TotalNumPages % 10) > 1)
                {
                    DT.TotalNumPages += 1;
                }

                //return View( _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID && m.User_ID == User_ID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync());
                DT.allClassifications = _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToPagedList(PageNum, 10);
                DT.ClassifiedTexts_Tags = Select_All_Classified_Tags(User_ID, Active_ProjectID);
                return View(DT);
            }
            else if (Active_UserID > 0 && Active_ProjectID > 0)
            {
                DT.Selected_UserID = Active_UserID;
                DT.TotalNumPages = _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID && m.User_ID == User_ID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToList().Count() / 10;
                if ((DT.TotalNumPages % 10) > 1)
                {
                    DT.TotalNumPages += 1;
                }

                //return View( _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID && m.User_ID == User_ID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync());
                DT.allClassifications = _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID && m.User_ID == User_ID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToPagedList(PageNum, 10);
                DT.ClassifiedTexts_Tags = Select_All_Classified_Tags(User_ID, Active_ProjectID);
                return View(DT);
            }
            else if (Active_UserID == 0)
            {
                DT.TotalNumPages = _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToList().Count() / 10;
                if ((DT.TotalNumPages % 10) > 1)
                {
                    DT.TotalNumPages += 1;
                }

                DT.allClassifications = _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToPagedList(PageNum, 10);
                DT.ClassifiedTexts_Tags = Select_All_Classified_Tags_ForAllUsers(Active_ProjectID);
                return View(DT);
            }
            //else if (User_ID == 0)
            //{
            //    return View( _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync());
            //}
            //else if (Active_ProjectID == 0)
            //{
            //    return View( _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.User_ID == User_ID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync());
            //}
            //  return View( _context.Assigned_TextClassifications_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync());
            DT.allClassifications = _context.Assigned_TextClassifications_ToUsers_TB.Where(m => m.Project_ID == 0 && m.User_ID == 0).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToPagedList(PageNum, 10);
            DT.ClassifiedTexts_Tags = Select_All_Classified_Tags(0, 0);
            return View(DT);
        }

        public List<ClassifiedTexts_Tags> Select_All_Classified_Tags(int User_ID, int Project_ID)
        {
            var ClassifiedTexts_Tags = _context.ClassifiedTexts_Tags.Where(m => m.Assigned_TextClassifications_ToUsers_TB.User_ID == User_ID && m.Assigned_TextClassifications_ToUsers_TB.Project_ID == Project_ID).Include("ClassificationLabels_TB").ToList();
            return ClassifiedTexts_Tags;
        }
        public List<ClassifiedTexts_Tags> Select_All_Classified_Tags_ForAllUsers(int Project_ID)
        {
            var ClassifiedTexts_Tags = _context.ClassifiedTexts_Tags.Where(m => m.Assigned_TextClassifications_ToUsers_TB.Project_ID == Project_ID).Include("ClassificationLabels_TB").ToList();
            return ClassifiedTexts_Tags;
        }
        public Assigned_TextClassifications_ToUsers_TB Selected_Assigned_Classification(int Assigned_Anno_ID)
        {
            
                var Selected_Annotation = _context.Assigned_TextClassifications_ToUsers_TB
               .FirstOrDefault(m => m.Assigned_TextClassification_ID == Assigned_Anno_ID);
               
                if (Selected_Annotation == null)
                {
                    return new Assigned_TextClassifications_ToUsers_TB();
                }
                return Selected_Annotation;
            
        }

        public List<ClassificationLabels_TB> Select_Classification_Labels(int? Project_ID = 0)
        {
            var Labels = _context.ClassificationLabels_TB.Include("Projects_TB").Where(m => m.Project_ID == Project_ID).ToList();
            //var Labels = _context.Labels_TB.Include("Projects_TB").Include("Labels_BG_Colours_TB").Where(m => m.Project_ID.ToString() == HttpContext.Session.GetString("ProjectID")).ToList();
            return Labels;
        }
        [HttpPost]

        private async Task<bool> IsAssignedAnnoDuplicated(int Anno_ID,int UserID, int? ProjectID)
        {
            var Classification = await _context.Assigned_TextClassifications_ToUsers_TB.Include("Projects_TB").FirstOrDefaultAsync(m => m.User_ID == UserID && m.TextClassification_ID == Anno_ID  && m.Project_ID == ProjectID);
            if (Classification == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsValidUser()
        {

            string usertype = "";

            if (HttpContext.Session.GetString("UserType") != null)
            {
                usertype = HttpContext.Session.GetString("UserType").ToUpper();
            }


            if (usertype != "ADMIN")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        // GET: Logins/Create
        public IActionResult Create()
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            Select_All_Users();
            Select_All_Annotations();
            Select_All_Projects();
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Assigned_Anno_ID,User_ID,Annotation_ID,Project_ID,Annotated_Text,Count_Annotations")] Assigned_TextClassifications_ToUsers_TB Assigned_TextClassifications_ToUsers_TB)
        //{
        //    if (!IsValidUser())
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }

        //    if (await IsAssignedAnnoDuplicated(Assigned_TextClassifications_ToUsers_TB.Annotation_ID,Assigned_TextClassifications_ToUsers_TB.User_ID,Assigned_TextClassifications_ToUsers_TB.Project_ID))
        //    {
        //        ViewBag.Error = "This Annotation is already assigned to this user in this project";

        //    }
        //    else
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var a = _context.Annotations_TB.FirstOrDefault(m => m.Annotation_ID == Assigned_TextClassifications_ToUsers_TB.Annotation_ID);
        //            Assigned_TextClassifications_ToUsers_TB.Annotated_Text = a.Annotation_Text;
        //            _context.Add(Assigned_TextClassifications_ToUsers_TB);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    Select_All_Projects();
        //    Select_All_Annotations();
        //    Select_All_Users();
        //    return View(Assigned_TextClassifications_ToUsers_TB);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Project_ID, int User_ID)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            
            var a = _context.Annotations_TB.Where(m => m.Project_ID == Project_ID).ToList();
            
            foreach (var item in a)
            {
               // Assigned_TextClassifications_ToUsers_TB.Annotated_Text = item.Annotation_Text;
              Assigned_TextClassifications_ToUsers_TB Assigned_Anno = new Assigned_TextClassifications_ToUsers_TB { TextClassification_ID = item.Annotation_ID, TextClassification_HtmlTags = "", User_ID = User_ID, Project_ID = Project_ID, Count_Classifications = 0, Not_Sure = false, Comments = "" };
                _context.Add(Assigned_Anno);
                await _context.SaveChangesAsync();
            }
                  
               
            Select_All_Projects();
            Select_All_Annotations();
            Select_All_Users();
            return RedirectToAction(nameof(Index));
        }

        

        public ActionResult Details(int id, int projectID)
        {
            Details_Assigned_TextClassifications_ToUsers HP = new Details_Assigned_TextClassifications_ToUsers();           
            HP.allClassificationLabels = Select_Classification_Labels(projectID);
            HP.Assigned_TextClassifications_ToUsers_TB = Selected_Assigned_Classification(id);
            HP.ClassifiedTexts_Tags = Select_All_ClassifiedText_Tags(id);
            return View(HP);

           

            
        }

       

        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            Select_All_Users();
            Select_All_Annotations();
            Select_All_Projects();
            
            if (id == null)
            {
                return NotFound();
            }

            //   var login = await _context.Users_TB.FindAsync(id);
            var Assigned_Anno = await _context.Assigned_TextClassifications_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").FirstOrDefaultAsync(m => m.Assigned_TextClassification_ID == id);
            if (Assigned_Anno == null)
            {
                return NotFound();
            }
            return View(Assigned_Anno);
        }
        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Assigned_Anno_ID,User_ID,Annotation_ID,Project_ID,Count_Annotations,Not_Sure,Comments")] Assigned_TextClassifications_ToUsers_TB Assigned_Classification)
        {
            Select_All_Users();
            Select_All_Annotations();
            Select_All_Projects();

            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id != Assigned_Classification.Assigned_TextClassification_ID)
            {
                return NotFound();
            }

            if (await IsAssignedAnnoDuplicated(Assigned_Classification.TextClassification_ID, Assigned_Classification.User_ID, Assigned_Classification.Project_ID))
            {
                ViewBag.Error = "This Annotation is already assigned to this user in this project";

            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Assigned_Classification);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!Assigned_Anno_Exists(Assigned_Classification.Assigned_TextClassification_ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(Assigned_Classification);
        }

        private bool Assigned_Anno_Exists(int id)
        {
            return _context.Assigned_TextClassifications_ToUsers_TB.Any(e => e.Assigned_TextClassification_ID == id);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            var Assigned_Anno = await _context.Assigned_TextClassifications_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").FirstOrDefaultAsync(m => m.Assigned_TextClassification_ID == id);

            if (Assigned_Anno == null)
            {
                return NotFound();
            }

            return View(Assigned_Anno);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            var Assigned_Anno = await _context.Assigned_TextClassifications_ToUsers_TB.FindAsync(id);
            _context.Assigned_TextClassifications_ToUsers_TB.Remove(Assigned_Anno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteFilter(int User_ID)
        {
            int Active_ProjectID = Convert.ToInt32(HttpContext.Session.GetString("Active_ProjectID"));

            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }


            
            if (User_ID > 0 && Active_ProjectID > 0)
            {
                _context.Assigned_TextClassifications_ToUsers_TB.RemoveRange(_context.Assigned_TextClassifications_ToUsers_TB.Where(x => x.User_ID == User_ID && x.Project_ID == Active_ProjectID));
            }
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public List<ClassifiedTexts_Tags> Select_All_ClassifiedText_Tags(int ClsTxt_ID)
        {
            var Active_ProjectID = Convert.ToInt32(HttpContext.Session.GetString("Active_ProjectID"));
            return _context.ClassifiedTexts_Tags.Where(m => m.Assigned_TextClassification_ID == ClsTxt_ID).ToList();

        }
        public List<Users_TB> Select_All_Users()
        {
            var Active_ProjectID = HttpContext.Session.GetString("Active_ProjectID");
            var users = _context.Users_TB.Where(m => m.Username.ToUpper() == "ALL USERS" || (m.Roles_TB.Role_Text.ToLower() != "admin" && m.Roles_TB.Project_ID.ToString() == Active_ProjectID)).ToList();
           
            //users.Add(new Users_TB { User_ID = -2, Username = "All Users", Password = "12345", Role_ID = users[0].Role_ID });
            ViewBag.Users = users;
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
