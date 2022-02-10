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
    public class Admin_RolesTBController : Controller
    {
        private readonly TextMarkContext _context;
        public Admin_RolesTBController(TextMarkContext context)
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
            return View(await _context.Roles_TB.Include("Projects_TB").Where(m => m.Project_ID.ToString() == Active_ProjectID && m.Role_Text.ToUpper() != "ADMIN").ToListAsync());
            
        }
        private async Task<bool> IsRoleDuplicated(string RoleText, int? ProjectID)
        {              
            var Role = await _context.Roles_TB.FirstOrDefaultAsync(m => m.Role_Text == RoleText && m.Project_ID == ProjectID);
            if (Role == null)
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
        public async Task<IActionResult> Create([Bind("Role_ID,Role_Text,Project_ID")] Roles_TB roles_tb)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (await IsRoleDuplicated(roles_tb.Role_Text, roles_tb.Project_ID))
            { 
                ViewBag.Error = "This Role is already registered for this project!"; 
                
            }
            if (roles_tb.Role_Text.ToUpper() == "ADMIN")
            {
                ViewBag.Error = "The ADMIN Role is a reserved role and cannot be assigned!";
            }
            else
            {
                if (ModelState.IsValid && roles_tb.Role_Text.ToUpper() != "ADMIN")
                {
                    _context.Add(roles_tb);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            Select_All_Projects();
            return View(roles_tb);
        }
        //public async Task<IActionResult> Details()
        //{            
        //    return View(await _context.Users_TB.ToListAsync());
        //}

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(int? id, int? ProjectID)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            var Roles_tb = await _context.Roles_TB
                .FirstOrDefaultAsync(m => m.Role_ID == id);
            if (Roles_tb == null)
            {
                return NotFound();
            }

            return View(Roles_tb);
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

            
            var Role = await _context.Roles_TB.Include("Projects_TB").FirstOrDefaultAsync(m => m.Role_ID == id);
            if (Role == null)
            {
                return NotFound();
            }
            return View(Role);
        }
        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Role_ID,Role_Text,Project_ID")] Roles_TB Roles_tb)
        {
            Select_All_Projects();
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id != Roles_tb.Role_ID)
            {
                return NotFound();
            }

            if (await IsRoleDuplicated(Roles_tb.Role_Text, Roles_tb.Project_ID))
            {
                ViewBag.Error = "This role is already registered for this project";

            }
            if (Roles_tb.Role_Text.ToUpper() == "ADMIN")
            {
                ViewBag.Error = "The ADMIN Role is a reserved role and cannot be assigned!";
            }
            else
            {
                if (ModelState.IsValid && Roles_tb.Role_Text.ToUpper() != "ADMIN")
                {
                    try
                    {
                        _context.Update(Roles_tb);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!RoleExists(Roles_tb.Role_ID))
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
            return View(Roles_tb);
        }

        private bool RoleExists(int id)
        {
            return _context.Roles_TB.Any(e => e.Role_ID == id);
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

            var login = await _context.Roles_TB.Include("Projects_TB").FirstOrDefaultAsync(m => m.Role_ID == id);

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

            var Role = await _context.Roles_TB.FindAsync(id);
            _context.Roles_TB.Remove(Role);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public List<Projects_TB> Select_All_Projects()
        {           
            ViewBag.Projects = _context.Projects_TB.ToList();
            
            return ViewBag.Projects;
        }
        
    }
}
