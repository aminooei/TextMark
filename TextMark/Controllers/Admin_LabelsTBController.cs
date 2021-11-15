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
    public class Admin_LabelsTBController : Controller
    {
        private readonly TextMarkContext _context;
        public Admin_LabelsTBController(TextMarkContext context)
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
            return View(await _context.Labels_TB.Include("Projects_TB").Where(m => m.Project_ID.ToString() == Active_ProjectID).ToListAsync());
        }

        private async Task<bool> IsLabelDuplicated(string LabelText, int? ProjectID , string Label_ShortCut_Key)
        {
            var Label = await _context.Labels_TB.FirstOrDefaultAsync(m => m.Label_Text == LabelText && m.Project_ID == ProjectID && m.Label_ShortCut_Key == Label_ShortCut_Key);
            if (Label == null)
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

            Select_All_Labels_BGColours();
            Select_All_Projects();
            return View();
        }
        private async Task<bool> IsBGColour_Shortcut_Duplicated(string Shortcut_Key, int? ProjectID)
        {
            var ShortcutKey = await _context.Labels_TB.FirstOrDefaultAsync(m => m.Label_ShortCut_Key == Shortcut_Key && m.Project_ID == ProjectID);
           
            if (ShortcutKey == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task<bool> IsBGColour_Duplicated(string Label_Background_Color, int? ProjectID)
        {
            var BG_Colour = await _context.Labels_TB.FirstOrDefaultAsync(m => m.Label_Background_Color == Label_Background_Color && m.Project_ID == ProjectID);

            if (BG_Colour == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //   public async Task<IActionResult> Create([Bind("Label_ID,Label_Text,Project_ID,Label_BGColour_ID")] Labels_TB labels_tb)
        public async Task<IActionResult> Create([Bind("Label_ID,Label_Text,Project_ID,Label_Background_Color,Label_ShortCut_Key")] Labels_TB labels_tb)
        {
           
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (await IsLabelDuplicated(labels_tb.Label_Text, labels_tb.Project_ID, labels_tb.Label_ShortCut_Key))
            {
                ViewBag.Error = "This Label is already registered for this Project";             
            }
            //else if (await IsBGColour_Shortcut_Duplicated(labels_tb.Label_ShortCut_Key, labels_tb.Project_ID))
            //{
            //    ViewBag.Error = "This Shortcut Key is already registered";               
            //}
            //else if (await IsBGColour_Duplicated(labels_tb.Label_Background_Color, labels_tb.Project_ID))
            //{
            //    ViewBag.Error = "This Background Colour is already registered";              
            //}
            else
            {
                if (ModelState.IsValid)
                {
                    labels_tb.Label_ShortCut_Key = labels_tb.Label_ShortCut_Key.ToUpper();
                    _context.Add(labels_tb);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            Select_All_Labels_BGColours();
            Select_All_Projects();
            return View(labels_tb);
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

            var Labels_tb = await _context.Labels_TB.Include("Projects_TB").Include("Labels_BG_Colours_TB")
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
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            Select_All_Labels_BGColours();
            Select_All_Projects();
            if (id == null)
            {
                return NotFound();
            }

            var label = await _context.Labels_TB.Include("Projects_TB").FirstOrDefaultAsync(m => m.Label_ID == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Label_ID,Label_Text,Project_ID,Label_Background_Color,Label_ShortCut_Key")] Labels_TB Labels_tb)
        {
            Select_All_Labels_BGColours();
            Select_All_Projects();
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }


            if (id != Labels_tb.Label_ID)
            {
                return NotFound();
            }
            if (await IsLabelDuplicated(Labels_tb.Label_Text, Labels_tb.Project_ID, Labels_tb.Label_ShortCut_Key))
            {
                ViewBag.Error = "This Label with this Shortcut Key is already registered for this Project";
            }
            //else if (await IsBGColour_Shortcut_Duplicated(Labels_tb.Label_ShortCut_Key, Labels_tb.Project_ID))
            //{
            //    ViewBag.Error = "This Shortcut Key is already registered";
            //}
            //else if (await IsBGColour_Duplicated(Labels_tb.Label_Background_Color, Labels_tb.Project_ID))
            //{
            //    ViewBag.Error = "This Background Colour is already registered";
            //}
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var entry = _context.Labels_TB.First(e => e.Label_ID == id);
                        Labels_tb.Label_ShortCut_Key = Labels_tb.Label_ShortCut_Key.ToUpper();
                        _context.Entry(entry).CurrentValues.SetValues(Labels_tb);
                        await _context.SaveChangesAsync();
                        //return true;


                        //Labels_tb.Label_ShortCut_Key = Labels_tb.Label_ShortCut_Key.ToUpper();
                        //_context.Update(Labels_tb);
                        //await _context.SaveChangesAsync();
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
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            var label = await _context.Labels_TB.Include("Projects_TB").FirstOrDefaultAsync(m => m.Label_ID == id);
              
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
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            var label = await _context.Labels_TB.FindAsync(id);
            _context.Labels_TB.Remove(label);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public List<Projects_TB> Select_All_Projects()
        {
            ViewBag.Projects = _context.Projects_TB.ToList();

            return ViewBag.Projects;
        }
        public List<Labels_TB> Select_All_Labels_BGColours()
        {
            ViewBag.Labels = _context.Labels_TB.ToList();

            return ViewBag.Labels;
        }

    }
}
