﻿using Microsoft.AspNetCore.Mvc;
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
    public class Admin_Assigned_AnnotationsTBController : Controller
    {
        private readonly TextMarkContext _context;
       
        public Admin_Assigned_AnnotationsTBController(TextMarkContext context)
        {
            _context = context;           
        }

        public async Task<IActionResult> Index()
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }
            Select_All_Users();
            Select_All_Projects();
            return View(await _context.Assigned_Annotations_ToUsers_TB.Where(m => m.Project_ID == 0 && m.User_ID == 0).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync());
        
        }


        public Assigned_Annotations_ToUsers_TB Selected_Assigned_Annotation(int Assigned_Anno_ID)
        {
            
                var Selected_Annotation = _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB")
               .FirstOrDefault(m => m.Assigned_Anno_ID == Assigned_Anno_ID);
                //.FirstOrDefault(m => m.Assigned_Anno_ID == id && m.User_ID == UserID && m.Project_ID == Project_ID);
                if (Selected_Annotation == null)
                {
                    return new Assigned_Annotations_ToUsers_TB();
                }
                return Selected_Annotation;
            
        }

        public List<Labels_TB> Select_Annotation_Labels(int? Project_ID = 0)
        {
            var Labels = _context.Labels_TB.Include("Projects_TB").Include("Labels_BG_Colours_TB").Where(m => m.Project_ID == Project_ID).ToList();
            //var Labels = _context.Labels_TB.Include("Projects_TB").Include("Labels_BG_Colours_TB").Where(m => m.Project_ID.ToString() == HttpContext.Session.GetString("ProjectID")).ToList();
            return Labels;
        }
        [HttpPost]
        public async Task<IActionResult> Index(int User_ID, int Project_ID)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }
            Select_All_Users();
            Select_All_Projects();
            if (User_ID > 0 && Project_ID > 0)
            {
                return View(await _context.Assigned_Annotations_ToUsers_TB.Where(m => m.Project_ID == Project_ID && m.User_ID == User_ID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync());
            }
            else if (User_ID == 0)
            {
                return View(await _context.Assigned_Annotations_ToUsers_TB.Where(m => m.Project_ID == Project_ID ).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync());
            }
            else if (Project_ID == 0)
            {
                return View(await _context.Assigned_Annotations_ToUsers_TB.Where(m => m.User_ID == User_ID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync());
            }
            return View(await _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync());
        }

        private async Task<bool> IsAssignedAnnoDuplicated(int Anno_ID,int UserID, int? ProjectID)
        {
            var Annotation = await _context.Assigned_Annotations_ToUsers_TB.Include("Projects_TB").FirstOrDefaultAsync(m => m.User_ID == UserID && m.Annotation_ID == Anno_ID  && m.Project_ID == ProjectID);
            if (Annotation == null)
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
        //public async Task<IActionResult> Create([Bind("Assigned_Anno_ID,User_ID,Annotation_ID,Project_ID,Annotated_Text,Count_Annotations")] Assigned_Annotations_ToUsers_TB assigned_annotations_tousers_tb)
        //{
        //    if (!IsValidUser())
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }

        //    if (await IsAssignedAnnoDuplicated(assigned_annotations_tousers_tb.Annotation_ID,assigned_annotations_tousers_tb.User_ID,assigned_annotations_tousers_tb.Project_ID))
        //    {
        //        ViewBag.Error = "This Annotation is already assigned to this user in this project";

        //    }
        //    else
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var a = _context.Annotations_TB.FirstOrDefault(m => m.Annotation_ID == assigned_annotations_tousers_tb.Annotation_ID);
        //            assigned_annotations_tousers_tb.Annotated_Text = a.Annotation_Text;
        //            _context.Add(assigned_annotations_tousers_tb);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    Select_All_Projects();
        //    Select_All_Annotations();
        //    Select_All_Users();
        //    return View(assigned_annotations_tousers_tb);
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
               // assigned_annotations_tousers_tb.Annotated_Text = item.Annotation_Text;
              Assigned_Annotations_ToUsers_TB Assigned_Anno = new Assigned_Annotations_ToUsers_TB { Annotation_ID = item.Annotation_ID, Annotated_Text = item.Annotation_Text, User_ID = User_ID, Project_ID = Project_ID, Count_Annotations = 0 };
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
            CL_Users_Home_Page HP = new CL_Users_Home_Page();           
            HP.allLabels = Select_Annotation_Labels(projectID);
            HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation(id);
            
            return View(HP);

           

            
        }

        // GET: Logins/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (!IsValidUser())
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var Assigned_Anno_TB = await _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB")
        //        .FirstOrDefaultAsync(m => m.Assigned_Anno_ID == id);
        //    if (Assigned_Anno_TB == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(Assigned_Anno_TB);
        //}
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
            var Assigned_Anno = await _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").FirstOrDefaultAsync(m => m.Assigned_Anno_ID == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Assigned_Anno_ID,User_ID,Annotation_ID,Project_ID,Count_Annotations")] Assigned_Annotations_ToUsers_TB Assigned_Anno)
        {
            Select_All_Users();
            Select_All_Annotations();
            Select_All_Projects();

            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id != Assigned_Anno.Assigned_Anno_ID)
            {
                return NotFound();
            }

            if (await IsAssignedAnnoDuplicated(Assigned_Anno.Annotation_ID, Assigned_Anno.User_ID, Assigned_Anno.Project_ID))
            {
                ViewBag.Error = "This Annotation is already assigned to this user in this project";

            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Assigned_Anno);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!Assigned_Anno_Exists(Assigned_Anno.Assigned_Anno_ID))
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
            return View(Assigned_Anno);
        }

        private bool Assigned_Anno_Exists(int id)
        {
            return _context.Assigned_Annotations_ToUsers_TB.Any(e => e.Assigned_Anno_ID == id);
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

            var Assigned_Anno = await _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").FirstOrDefaultAsync(m => m.Assigned_Anno_ID == id);

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

            var Assigned_Anno = await _context.Assigned_Annotations_ToUsers_TB.FindAsync(id);
            _context.Assigned_Annotations_ToUsers_TB.Remove(Assigned_Anno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteFilter(int User_ID, int Project_ID)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }


            //  var Assigned_Anno = await _context.Assigned_Annotations_ToUsers_TB.FindAsync(id);
            // _context.Assigned_Annotations_ToUsers_TB.Remove(Assigned_Anno);
            if (User_ID > 0 && Project_ID > 0)
            {
                _context.Assigned_Annotations_ToUsers_TB.RemoveRange(_context.Assigned_Annotations_ToUsers_TB.Where(x => x.User_ID == User_ID && x.Project_ID == Project_ID));
            }
            else if(User_ID == 0)
            {
                _context.Assigned_Annotations_ToUsers_TB.RemoveRange(_context.Assigned_Annotations_ToUsers_TB.Where(x => x.Project_ID == Project_ID));
            }
            else if (Project_ID == 0)
            {
                _context.Assigned_Annotations_ToUsers_TB.RemoveRange(_context.Assigned_Annotations_ToUsers_TB.Where(x => x.User_ID == User_ID));
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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
