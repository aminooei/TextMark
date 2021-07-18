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
            return View(await _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync());
            // return View();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Assigned_Anno_ID,User_ID,Annotation_ID,Project_ID,Annotated_Text")] Assigned_Annotations_ToUsers_TB assigned_annotations_tousers_tb)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (await IsAssignedAnnoDuplicated(assigned_annotations_tousers_tb.Annotation_ID,assigned_annotations_tousers_tb.User_ID,assigned_annotations_tousers_tb.Project_ID))
            {
                ViewBag.Error = "This Annotation is already assigned to this user in this project";

            }
            else
            {
                if (ModelState.IsValid)
                {
                    var a = _context.Annotations_TB.FirstOrDefault(m => m.Annotation_ID == assigned_annotations_tousers_tb.Annotation_ID);
                    assigned_annotations_tousers_tb.Annotated_Text = a.Annotation_Text;
                    _context.Add(assigned_annotations_tousers_tb);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            Select_All_Projects();
            Select_All_Annotations();
            Select_All_Users();
            return View(assigned_annotations_tousers_tb);
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

            var Assigned_Anno_TB = await _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB")
                .FirstOrDefaultAsync(m => m.Assigned_Anno_ID == id);
            if (Assigned_Anno_TB == null)
            {
                return NotFound();
            }

            return View(Assigned_Anno_TB);
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
        public async Task<IActionResult> Edit(int id, [Bind("Assigned_Anno_ID,User_ID,Annotation_ID,Project_ID")] Assigned_Annotations_ToUsers_TB Assigned_Anno)
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
