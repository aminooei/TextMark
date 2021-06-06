using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TextMark.Data;
using TextMark.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TextMark.Controllers
{
    public class Admin_RolesTBController : Controller
    {
        private readonly TextMarkContext _context;
        public Admin_RolesTBController(TextMarkContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles_TB.ToListAsync());
            // return View();
        }
        // GET: Logins/Create
        public IActionResult Create()
        {
         //   Select_All_Roles();
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Role_ID,Role_Text")] Roles_TB roles_tb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roles_tb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roles_tb);
        }
        //public async Task<IActionResult> Details()
        //{            
        //    return View(await _context.Users_TB.ToListAsync());
        //}

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
           // Select_All_Roles();
            if (id == null)
            {
                return NotFound();
            }

              var login = await _context.Roles_TB.FindAsync(id);
           // var login = await _context.Users_TB.Include("Roles_TB").FirstOrDefaultAsync(m => m.User_ID == id);
            if (login == null)
            {
                return NotFound();
            }
            return View(login);
        }
        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Role_ID,Role_Text")] Roles_TB Roles_tb)
        {
            if (id != Roles_tb.Role_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Roles_tb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(Roles_tb.Role_ID))
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
            return View(Roles_tb);
        }

        private bool UserExists(int id)
        {
            return _context.Users_TB.Any(e => e.User_ID == id);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Roles_TB.FirstOrDefaultAsync(m => m.Role_ID == id);

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
            var Role = await _context.Roles_TB.FindAsync(id);
            _context.Roles_TB.Remove(Role);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
    }
}
