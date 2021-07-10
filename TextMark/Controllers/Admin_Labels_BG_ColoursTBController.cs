using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TextMark.Data;
using TextMark.Models;

namespace TextMark.Controllers
{
    public class Admin_Labels_BG_ColoursTBController : Controller
    {
        private readonly TextMarkContext _context;
        public Admin_Labels_BG_ColoursTBController(TextMarkContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            return View(_context.Labels_BG_Colours_TB.ToList());
                                 
        }
        private async Task<bool> IsBGColourDuplicated(string Label_Background_Color, string Shortcut_Key)
        {
            var BG_Colour = await _context.Labels_BG_Colours_TB.FirstOrDefaultAsync(m => m.Label_Background_Color == Label_Background_Color && m.Label_ShortCut_Key == Shortcut_Key);
            if (BG_Colour == null)
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

           // Select_All_Projects();
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Label_BGColour_ID,Label_Background_Color,Label_ShortCut_Key")] Labels_BG_Colours_TB Labels_BG_Colours_tb)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }
            // IsBGColourDuplicated(string Label_Background_Color, char? Shortcut_Key)
            if (await IsBGColourDuplicated(Labels_BG_Colours_tb.Label_Background_Color, Labels_BG_Colours_tb.Label_ShortCut_Key))
            {
                ViewBag.Error = "This Background Colour OR Shortkey is already registered";

            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Labels_BG_Colours_tb);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            //Select_All_Projects();
            return View();
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

            var Labels_BG_Colours_tb = await _context.Labels_BG_Colours_TB
                .FirstOrDefaultAsync(m => m.Label_BGColour_ID == id);
            if (Labels_BG_Colours_tb == null)
            {
                return NotFound();
            }

            return View(Labels_BG_Colours_tb);
        }
        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

           // Select_All_Projects();
            if (id == null)
            {
                return NotFound();
            }


            var Role = await _context.Labels_BG_Colours_TB.FirstOrDefaultAsync(m => m.Label_BGColour_ID == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Label_BGColour_ID,Label_Background_Color,Label_ShortCut_Key")] Labels_BG_Colours_TB Labels_BG_Colour_tb)
        {
            //Select_All_Projects();
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id != Labels_BG_Colour_tb.Label_BGColour_ID)
            {
                return NotFound();
            }

            if (await IsBGColourDuplicated(Labels_BG_Colour_tb.Label_Background_Color, Labels_BG_Colour_tb.Label_ShortCut_Key))
            {
                ViewBag.Error = "This Background Colour OR Shortkey is already registered";

            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Labels_BG_Colour_tb);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (await Label_ColourBG_Exists(Labels_BG_Colour_tb.Label_BGColour_ID))
                        {
                            throw;
                        }
                        else
                        {
                            return NotFound(); 
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(Labels_BG_Colour_tb);
        }

        private async Task<bool> Label_ColourBG_Exists(int id)
        {
            return await _context.Labels_BG_Colours_TB.AnyAsync(e => e.Label_BGColour_ID == id);
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

            var Label_BGColor = await _context.Labels_BG_Colours_TB.FirstOrDefaultAsync(m => m.Label_BGColour_ID == id);

            if (Label_BGColor == null)
            {
                return NotFound();
            }

            return View(Label_BGColor);
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

            var Label_BGColour = await _context.Labels_BG_Colours_TB.FindAsync(id);
            _context.Labels_BG_Colours_TB.Remove(Label_BGColour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       

    }
}
