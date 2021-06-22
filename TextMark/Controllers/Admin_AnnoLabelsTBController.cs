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
    public class Admin_AnnoLabelsTBController : Controller
    {
        private readonly TextMarkContext _context;
        public Admin_AnnoLabelsTBController(TextMarkContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Home");
            }
            return View(await _context.Annotations_Labels_TB.Include("Annotations_TB").Include("Labels_TB").ToListAsync());
            // return View();
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
                return RedirectToAction("Index", "Home");
            }

            Select_All_Annotations();
            Select_All_Labels();
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Anno_Label_ID,Annotation_ID,Label_ID")] Annotations_Labels_TB anno_Labels_tb)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                _context.Add(anno_Labels_tb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anno_Labels_tb);
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
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var Anno_Labels = await _context.Annotations_Labels_TB.Include("Annotations_TB").Include("Labels_TB")
                .FirstOrDefaultAsync(m => m.Anno_Label_ID == id);
            if (Anno_Labels == null)
            {
                return NotFound();
            }

            return View(Anno_Labels);
        }
        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Home");
            }

            Select_All_Annotations();
            Select_All_Labels();

            if (id == null)
            {
                return NotFound();
            }

            //   var login = await _context.Users_TB.FindAsync(id);
            var login = await _context.Annotations_Labels_TB.Include("Annotations_TB").Include("Labels_TB").FirstOrDefaultAsync(m => m.Anno_Label_ID == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Anno_Label_ID,Annotation_ID,Label_ID")] Annotations_Labels_TB Anno_Labels_tb)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Home");
            }

            if (id != Anno_Labels_tb.Anno_Label_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Anno_Labels_tb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Anno_Label_Exists(Anno_Labels_tb.Anno_Label_ID))
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
            return View(Anno_Labels_tb);
        }

        private bool Anno_Label_Exists(int id)
        {
            return _context.Annotations_Labels_TB.Any(e => e.Anno_Label_ID == id);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var anno_Label = await _context.Annotations_Labels_TB.Include("Annotations_TB").Include("Labels_TB").FirstOrDefaultAsync(m => m.Anno_Label_ID == id);

            if (anno_Label == null)
            {
                return NotFound();
            }

            return View(anno_Label);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Home");
            }

            var anno_Label = await _context.Annotations_Labels_TB.FindAsync(id);
            _context.Annotations_Labels_TB.Remove(anno_Label);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public List<Annotations_TB> Select_All_Annotations()
        {
            //ViewBag.Roles = new SelectList(_context.Roles_TB, "Role_ID", "Role_Text");
            ViewBag.Annotations = _context.Annotations_TB.ToList();
            return ViewBag.Annotations;
        }
        public List<Labels_TB> Select_All_Labels()
        {
            //ViewBag.Roles = new SelectList(_context.Roles_TB, "Role_ID", "Role_Text");
            ViewBag.Labels = _context.Labels_TB.ToList();
            return ViewBag.labels;
        }
    }
}
