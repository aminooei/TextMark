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
    public class Admin_AnnoTextsTBController : Controller
    {
        private readonly TextMarkContext _context;
        public Admin_AnnoTextsTBController(TextMarkContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }
            return View(await _context.Annotations_TB.Include("Projects_TB").ToListAsync());
          
        }

        private async Task<bool> IsAnnoDuplicated(string Anno_Text, int? ProjectID)
        {
            var Annotation = await _context.Annotations_TB.Include("Projects_TB").FirstOrDefaultAsync(m => m.Annotation_Text == Anno_Text && m.Project_ID == ProjectID);
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

            Select_All_Projects();
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Annotation_ID,Annotation_Title,Annotation_Text,Project_ID")] Annotations_TB annotations_tb)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (await IsAnnoDuplicated(annotations_tb.Annotation_Text, annotations_tb.Project_ID))
            {
                ViewBag.Error = "This Label is already registered for this Project";

            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(annotations_tb);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            Select_All_Projects();
            return View(annotations_tb);
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

            var Annotations_tb = await _context.Annotations_TB.Include("Projects_TB").FirstOrDefaultAsync(m => m.Annotation_ID == id);
            if (Annotations_tb == null)
            {
                return NotFound();
            }

            return View(Annotations_tb);
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

            var Anno = await _context.Annotations_TB.Include("Projects_TB").FirstOrDefaultAsync(m => m.Annotation_ID == id);
          
            if (Anno == null)
            {
                return NotFound();
            }
            return View(Anno);
        }
        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Annotation_ID,Annotation_Text,Project_ID")] Annotations_TB Anno_tb)
        {
            Select_All_Projects();
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id != Anno_tb.Annotation_ID)
            {
                return NotFound();
            }
            if (await IsAnnoDuplicated(Anno_tb.Annotation_Text, Anno_tb.Project_ID))
            {
                ViewBag.Error = "This Annotation Text is already registered for this Project";

            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Anno_tb);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AnnoExists(Anno_tb.Annotation_ID))
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
            return View(Anno_tb);
        }

        private bool AnnoExists(int id)
        {
            return _context.Annotations_TB.Any(e => e.Annotation_ID == id);
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

            var anno = await _context.Annotations_TB.Include("Projects_TB").FirstOrDefaultAsync(m => m.Annotation_ID == id);

            if (anno == null)
            {
                return NotFound();
            }

            return View(anno);
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

            var anno = await _context.Annotations_TB.FindAsync(id);
            _context.Annotations_TB.Remove(anno);
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
