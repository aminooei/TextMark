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

namespace TextMark.Controllers
{
    public class Admin_UsersAccessLevelController : Controller
    {
        private readonly TextMarkContext _context;

        public Admin_UsersAccessLevelController(TextMarkContext context)
        {
            _context = context;
        }

     
        public async Task<IActionResult> Index(int id)
        {           
            var Selected_User_ID_Access_Level = id;
            HttpContext.Session.SetString("Active_UserID", id.ToString());
            string Active_Username = _context.Users_TB.FirstOrDefault(m => m.User_ID == id).Username;
            HttpContext.Session.SetString("Active_Username", Active_Username);
            var Active_ProjectID = HttpContext.Session.GetString("Active_ProjectID");
            return View(await _context.Users_Access_Level_TB.Include("Users_TB").Include("Projects_TB").Where(m => (m.Users_TB.Roles_TB.Project_ID.ToString() == Active_ProjectID && m.User_ID == Selected_User_ID_Access_Level)).ToListAsync());
            // return View(await _context.Users_TB.Include("Roles_TB").Select(x => new { Username = x.Username, Password = x.Password, Role = x.Roles_TB.Role_Text + "(" + x.Roles_TB.Projects_TB.Project_Name + ")" }).ToListAsync());

        }
        private async Task<bool> IsUserDuplicated(int UserID, string Source_File_Name)
        {
            var User = await _context.Users_Access_Level_TB.Include("Users_TB").Include("Projects_TB").FirstOrDefaultAsync(m => m.User_ID == UserID && m.Source_File_Name == Source_File_Name);
            if (User == null)
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

           
            Select_All_FileNames();
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("User_ID,Text_Annotation_Allowed,Text_Classification_Allowed,Source_File_Name,Project_ID")] Users_Access_Level_TB Users_Access_Level_tb)
        {
            var Active_ProjectID = Convert.ToInt32(HttpContext.Session.GetString("Active_ProjectID"));
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (await IsUserDuplicated(Users_Access_Level_tb.User_ID, Users_Access_Level_tb.Source_File_Name))
            {
                ViewBag.Error = "This Source File Name is already registered for this User";

            }
            else
            {


                if (ModelState.IsValid)
                {
                  //  Users_Access_Level_tb.Project_ID = Active_ProjectID;
                    _context.Add(Users_Access_Level_tb);
                    await _context.SaveChangesAsync();

                    await Assign_Or_Delete_Source_File_Name(Users_Access_Level_tb.User_ID, Users_Access_Level_tb.Source_File_Name, Users_Access_Level_tb.Text_Annotation_Allowed, Users_Access_Level_tb.Text_Classification_Allowed, Active_ProjectID);


                        return RedirectToAction(nameof(Index), new { id = Users_Access_Level_tb.User_ID });
                   // return RedirectToAction("Index", new { id = Users_Access_Level_tb.User_ID } );
                }
            }
            //Select_All_Roles();
            return RedirectToAction("Index", new { id = Users_Access_Level_tb.User_ID });
        }
        public async Task<IActionResult> Assign_TextClassifications_To_User(int Project_ID, int User_ID, string Source_File_Name)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }


            var a = _context.Annotations_TB.Where(m => (m.Project_ID == Project_ID && m.Source_File_Name == Source_File_Name)).ToList();

            foreach (var item in a)
            {
                var b = _context.Assigned_TextClassifications_ToUsers_TB.Where(m => (m.TextClassification_ID == item.Annotation_ID && m.User_ID == User_ID && m.Project_ID == Project_ID)).ToList();
                if (b.Count == 0)
                {
                    Assigned_TextClassifications_ToUsers_TB Assigned_Classififcation = new Assigned_TextClassifications_ToUsers_TB { TextClassification_ID = item.Annotation_ID, TextClassification_HtmlTags = item.Annotation_Text, User_ID = User_ID, Project_ID = Project_ID, Count_Classifications = 0 };
                    _context.Add(Assigned_Classififcation);
                    await _context.SaveChangesAsync();
                }
            }



            return RedirectToAction(nameof(Index));
        }
        
             public async Task<IActionResult> Delete_TextClassifications_of_User(int Project_ID, int User_ID, string Source_File_Name)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }


            var a = _context.Annotations_TB.Where(m => (m.Project_ID == Project_ID && m.Source_File_Name == Source_File_Name)).ToList();

            foreach (var item in a)
            {
                var b = _context.Assigned_TextClassifications_ToUsers_TB.Where(m => (m.TextClassification_ID == item.Annotation_ID && m.User_ID == User_ID && m.Project_ID == Project_ID)).ToList();
                if (b.Count == 1)
                {
                    var Assigned_Classififcation = await _context.Assigned_TextClassifications_ToUsers_TB.FindAsync(b[0].Assigned_TextClassification_ID);
                    _context.Assigned_TextClassifications_ToUsers_TB.Remove(Assigned_Classififcation);
                    await _context.SaveChangesAsync();
                }
            }



            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Assign_TextAnnotations_To_User(int Project_ID, int User_ID, string Source_File_Name)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }


            var a = _context.Annotations_TB.Where(m => (m.Project_ID == Project_ID && m.Source_File_Name == Source_File_Name)).ToList();

            foreach (var item in a)
            {
                var b = _context.Assigned_Annotations_ToUsers_TB.Where(m => (m.Annotation_ID == item.Annotation_ID && m.User_ID == User_ID && m.Project_ID == Project_ID)).ToList();
                if (b.Count == 0)
                {
                    Assigned_Annotations_ToUsers_TB Assigned_Anno = new Assigned_Annotations_ToUsers_TB { Annotation_ID = item.Annotation_ID, Annotated_Text = item.Annotation_Text, User_ID = User_ID, Project_ID = Project_ID, Count_Annotations = 0 };
                    _context.Add(Assigned_Anno);
                    await _context.SaveChangesAsync();
                }
            }



            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete_TextAnnotations_of_User(int Project_ID, int User_ID, string Source_File_Name)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }


            var a = _context.Annotations_TB.Where(m => (m.Project_ID == Project_ID && m.Source_File_Name == Source_File_Name)).ToList();

            foreach (var item in a)
            {
                var b = _context.Assigned_Annotations_ToUsers_TB.Where(m => (m.Annotation_ID == item.Annotation_ID && m.User_ID == User_ID && m.Project_ID == Project_ID)).ToList();
                if (b.Count == 1)
                {
                    var Assigned_Anno = await _context.Assigned_Annotations_ToUsers_TB.FindAsync(b[0].Assigned_Anno_ID);
                    _context.Assigned_Annotations_ToUsers_TB.Remove(Assigned_Anno);
                    await _context.SaveChangesAsync();                  
                }
            }



            return RedirectToAction(nameof(Index));
        }
        //public async Task<IActionResult> Details()
        //{            
        //    return View(await _context.Users_TB.ToListAsync());
        //}

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            var User_Access_Level = await _context.Users_Access_Level_TB.Include("Users_TB").Include("Projects_TB")
                .FirstOrDefaultAsync(m => m.ID == id);
            if (User_Access_Level == null)
            {
                return NotFound();
            }

            return View(User_Access_Level);
        }
        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            Select_All_FileNames();
           /// Select_All_Roles();
            if (id == null)
            {
                return NotFound();
            }

            //   var login = await _context.Users_TB.FindAsync(id);
            var UserAccessLevel = await _context.Users_Access_Level_TB.Include("Users_TB").FirstOrDefaultAsync(m => m.ID == id);
            if (UserAccessLevel == null)
            {
                return NotFound();
            }
            return View(UserAccessLevel);
        }

        public async Task<IActionResult> Assign_Source_File_Name(int? id)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            Select_All_FileNames();
            Select_All_Roles();
            if (id == null)
            {
                return NotFound();
            }

            // var login = await _context.Users_TB.FindAsync(id);
            var login = await _context.Users_TB.Include("Roles_TB").FirstOrDefaultAsync(m => m.User_ID == id);
            if (login == null)
            {
                return NotFound();
            }
            return View(login);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign_Or_Delete_Source_File_Name(int UserID, string Source_File_Name, bool Text_Annotation_Allowed, bool Text_Classification_Allowed, int Project_ID)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            Select_All_FileNames();
            Select_All_Roles();
            if (UserID == 0)
            {
                return NotFound();
            }


            var users_tb = await _context.Users_Access_Level_TB.Include("Users_TB").FirstOrDefaultAsync(m => m.User_ID == UserID);



            // var a = await _context.Roles_TB.Include("Projects_TB")
            //.FirstOrDefaultAsync(m => m.Role_ID == users_tb.Role_ID);

            if (Text_Annotation_Allowed)
            {
                await Assign_TextAnnotations_To_User(Project_ID, UserID, Source_File_Name);
            }
            if(!Text_Annotation_Allowed)
            {
                await Delete_TextAnnotations_of_User(Project_ID, UserID, Source_File_Name);
            }
            if (Text_Classification_Allowed)
            {
                await Assign_TextClassifications_To_User(Project_ID, UserID, Source_File_Name);
            }
            if (!Text_Classification_Allowed)
            {
                await Delete_TextClassifications_of_User(Project_ID, UserID, Source_File_Name);
            }
            return RedirectToAction(nameof(Index));

        }



        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,User_ID,Text_Annotation_Allowed,Text_Classification_Allowed,Source_File_Name,Project_ID")] Users_Access_Level_TB Users_Access_Level_tb)
        {
            Select_All_FileNames();
           // Select_All_Roles();
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id != Users_Access_Level_tb.ID)
            {
                return NotFound();
            }

            //if (await IsUserDuplicated(Users_tb.Username, Users_tb.Role_ID))
            //{
            //    ViewBag.Error = "This Username is already registered for this role";

            //}
            //else
            //{
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Users_Access_Level_tb);
                    await _context.SaveChangesAsync();
                    var Active_ProjectID = Convert.ToInt32(HttpContext.Session.GetString("Active_ProjectID"));
                    await Assign_Or_Delete_Source_File_Name(Users_Access_Level_tb.User_ID, Users_Access_Level_tb.Source_File_Name, Users_Access_Level_tb.Text_Annotation_Allowed, Users_Access_Level_tb.Text_Classification_Allowed, Active_ProjectID);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(Users_Access_Level_tb.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = Users_Access_Level_tb.User_ID });
            }
            // }
            return View(Users_Access_Level_tb);
        }

        private bool UserExists(int id)
        {
            return _context.Users_TB.Any(e => e.User_ID == id);
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

            var User_Access_Level = await _context.Users_Access_Level_TB.Include("Users_TB").Include("Projects_TB").FirstOrDefaultAsync(m => m.ID == id);

            if (User_Access_Level == null)
            {
                return NotFound();
            }

            return View(User_Access_Level);
            
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

            var User_Access_Level = await _context.Users_Access_Level_TB.FindAsync(id);
            _context.Users_Access_Level_TB.Remove(User_Access_Level);
            await _context.SaveChangesAsync();

            var Active_ProjectID = Convert.ToInt32(HttpContext.Session.GetString("Active_ProjectID"));
            await Assign_Or_Delete_Source_File_Name(User_Access_Level.User_ID, User_Access_Level.Source_File_Name, false, false, Active_ProjectID);

            return RedirectToAction(nameof(Index), new { id = User_Access_Level.User_ID });
            
        }

        public void Select_All_Roles()
        {
            var Active_ProjectID = HttpContext.Session.GetString("Active_ProjectID");
            // ViewBag.Roles = _context.Roles_TB.Include("Projects_TB").Where(m => m.Project_ID.ToString() == Active_ProjectID).Select(x => new { Role_ID = x.Role_ID, Role_Project_Name = x.Role_Text + "("+x.Projects_TB.Project_Name+")"  }).ToList();
            ViewBag.Roles = _context.Roles_TB.Include("Projects_TB").Where(m => m.Project_ID.ToString() == Active_ProjectID).Select(x => new { Role_ID = x.Role_ID, Role_Project_Name = x.Role_Text }).ToList();

            // return ViewBag.Roles;
        }
        public void Select_All_FileNames()
        {
            var Active_ProjectID = HttpContext.Session.GetString("Active_ProjectID");
            // ViewBag.Roles = _context.Roles_TB.Include("Projects_TB").Where(m => m.Project_ID.ToString() == Active_ProjectID).Select(x => new { Role_ID = x.Role_ID, Role_Project_Name = x.Role_Text + "("+x.Projects_TB.Project_Name+")"  }).ToList();
            ViewBag.Source_File_Names = _context.Annotations_TB.Where(m => m.Project_ID.ToString() == Active_ProjectID).Select(x => new { Source_File_Name = x.Source_File_Name }).Distinct().ToList();

            // return ViewBag.Roles;
        }
    
}
}
