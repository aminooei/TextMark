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
    //[Authorize]
    public class Admin_UsersTBController : Controller
    {
        private readonly TextMarkContext _context;
      
        public Admin_UsersTBController(TextMarkContext context)
        {           
            _context = context; 
        }

        public async Task<IActionResult> Index()
        {               
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }
            var Active_ProjectID = HttpContext.Session.GetString("Active_ProjectID");
            return View(await _context.Users_TB.Include("Roles_TB").Include("Roles_TB.Projects_TB").Where(m => m.Roles_TB.Project_ID.ToString() == Active_ProjectID).ToListAsync());
           // return View(await _context.Users_TB.Include("Roles_TB").Select(x => new { Username = x.Username, Password = x.Password, Role = x.Roles_TB.Role_Text + "(" + x.Roles_TB.Projects_TB.Project_Name + ")" }).ToListAsync());
            
            
        }

        private async Task<bool> IsUserDuplicated(string Username, int? RoleID)
        {
            var User = await _context.Users_TB.Include("Roles_TB").FirstOrDefaultAsync(m => m.Username == Username && m.Role_ID == RoleID);
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

            Select_All_Roles();
            Select_All_FileNames();
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("User_ID,Username,Password,ConfirmPassword,Text_Annotation_Allowed,Text_Classification_Allowed,Role_ID")] Users_TB users_tb, string Source_File_Name)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (await IsUserDuplicated(users_tb.Username, users_tb.Role_ID))
            {
                ViewBag.Error = "This User is already registered for this role";

            }
            else
            {
                

                if (ModelState.IsValid)
                {

                    _context.Add(users_tb);
                    await _context.SaveChangesAsync();

                    await Assign_Source_File_Name(users_tb.User_ID, Source_File_Name);


                    return RedirectToAction(nameof(Index));
                }
            }
            //Select_All_Roles();
            return View(users_tb);
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

            var Users_tb = await _context.Users_TB.Include("Roles_TB").Include("Roles_TB.Projects_TB")
                .FirstOrDefaultAsync(m => m.User_ID == id);
            if (Users_tb == null)
            {
                return NotFound();
            }

            return View(Users_tb);
        }
        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

             //   var login = await _context.Users_TB.FindAsync(id);
            var login = await _context.Users_TB.Include("Roles_TB").FirstOrDefaultAsync(m => m.User_ID == id);
            if (login == null)
            {
                return NotFound();
            }
            return View(login);
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
        public async Task<IActionResult> Assign_Source_File_Name(int id, string Source_File_Name)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            Select_All_FileNames();
            Select_All_Roles();
            if (id == 0)
            {
                return NotFound();
            }


            var users_tb = await _context.Users_TB.Include("Roles_TB").FirstOrDefaultAsync(m => m.User_ID == id);
           


            var a = await _context.Roles_TB.Include("Projects_TB")
           .FirstOrDefaultAsync(m => m.Role_ID == users_tb.Role_ID);

            if (users_tb.Text_Annotation_Allowed)
            {
                await Assign_TextAnnotations_To_User(a.Project_ID, id, Source_File_Name);
            }
            if (users_tb.Text_Classification_Allowed)
            {
                await Assign_TextClassifications_To_User(a.Project_ID, id, Source_File_Name);
            }
            return RedirectToAction(nameof(Index));

        }



        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("User_ID,Username,Password,ConfirmPassword,Text_Annotation_Allowed,Text_Classification_Allowed,Role_ID")] Users_TB Users_tb, string Source_File_Name, int RoleID)
        {
            Select_All_FileNames();
            Select_All_Roles();
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id != Users_tb.User_ID)
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
                        _context.Update(Users_tb);
                        await _context.SaveChangesAsync();

                        await Assign_Source_File_Name(Users_tb.User_ID, Source_File_Name);
                }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(Users_tb.User_ID))
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
           // }
            return View(Users_tb);
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

            var login = await _context.Users_TB.Include("Roles_TB").FirstOrDefaultAsync(m => m.User_ID == id);
              
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
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

            var login = await _context.Users_TB.FindAsync(id);
            _context.Users_TB.Remove(login);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
