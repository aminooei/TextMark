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
    public class Admin_ProjectsTBController : Controller
    {
        private readonly TextMarkContext _context;

        public Admin_ProjectsTBController(TextMarkContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            return View(await _context.Projects_TB.ToListAsync());
            // return View();
        }

        private async Task<bool> IsProjectDuplicated(string ProjectText)
        {
            var Project = await _context.Projects_TB.FirstOrDefaultAsync(m => m.Project_Name == ProjectText);
            if (Project == null)
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

            Select_All_Projects();
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Project_ID,Project_Name")] Projects_TB Projects_TB)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (await IsProjectDuplicated(Projects_TB.Project_Name))
            {
                ViewBag.Error = "This Project Name is already registered.";

            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Projects_TB);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(Projects_TB);
        }
        //public async Task<IActionResult> Details()
        //{            
        //    return View(await _context.Projects_TB.ToListAsync());
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

            var Projects_TB = await _context.Projects_TB
                .FirstOrDefaultAsync(m => m.Project_ID == id);
            if (Projects_TB == null)
            {
                return NotFound();
            }

            return View(Projects_TB);
        }
        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }


            Select_All_Projects();
            if (id == null)
            {
                return NotFound();
            }

            //   var login = await _context.Projects_TB.FindAsync(id);
            var project = await _context.Projects_TB.FirstOrDefaultAsync(m => m.Project_ID == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }
        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Project_ID,Project_Name")] Projects_TB Projects_TB)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id != Projects_TB.Project_ID)
            {
                return NotFound();
            }

            if (await IsProjectDuplicated(Projects_TB.Project_Name))
            {
                ViewBag.Error = "This Project Name is already registered.";

            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Projects_TB);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProjectExists(Projects_TB.Project_ID))
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
            return View(Projects_TB);
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects_TB.Any(e => e.Project_ID == id);
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

            var project = await _context.Projects_TB.FirstOrDefaultAsync(m => m.Project_ID == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
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

            var project = await _context.Projects_TB.FindAsync(id);
            _context.Projects_TB.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public List<Projects_TB> Select_All_Projects()
        {

            //ViewBag.Roles = new SelectList(_context.Roles_TB, "Role_ID", "Role_Text");
            ViewBag.Projects = _context.Projects_TB.ToList();
            return ViewBag.Projects;
        }
    }
}
