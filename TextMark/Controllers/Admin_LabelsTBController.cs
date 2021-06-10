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

namespace TextMark.Controllers
{
    [Authorize]
    public class Admin_LabelsTBController : Controller
    {
        private readonly TextMarkContext _context;
        public Admin_LabelsTBController(TextMarkContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Labels_TB.ToListAsync());
            // return View();
        }
        // GET: Logins/Create
        public IActionResult Create()
        {
       //     Select_All_Roles();
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Label_ID,Label_Text")] Labels_TB labels_tb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labels_tb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labels_tb);
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

            var Labels_tb = await _context.Labels_TB
                .FirstOrDefaultAsync(m => m.Label_ID == id);
            if (Labels_tb == null)
            {
                return NotFound();
            }

            return View(Labels_tb);
        }
        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //Select_All_Roles();
            if (id == null)
            {
                return NotFound();
            }

            var label = await _context.Labels_TB.FindAsync(id);
            //var login = await _context.Labels_TB.Include("Roles_TB").FirstOrDefaultAsync(m => m.User_ID == id);
            if (label == null)
            {
                return NotFound();
            }
            return View(label);
        }
        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Label_ID,Label_Text")] Labels_TB Labels_tb)
        {
            if (id != Labels_tb.Label_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Labels_tb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabelExists(Labels_tb.Label_ID))
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
            return View(Labels_tb);
        }

        private bool LabelExists(int id)
        {
            return _context.Labels_TB.Any(e => e.Label_ID == id);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var label = await _context.Labels_TB.FirstOrDefaultAsync(m => m.Label_ID == id);
              
            if (label == null)
            {
                return NotFound();
            }

            return View(label);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var label = await _context.Labels_TB.FindAsync(id);
            _context.Labels_TB.Remove(label);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public List<Roles_TB> Select_All_Roles()
        //{
        //    //ViewBag.Roles = new SelectList(_context.Roles_TB, "Role_ID", "Role_Text");
        //    ViewBag.Roles = _context.Roles_TB.ToList();
        //    return ViewBag.Roles;
        //}        
    }
}
