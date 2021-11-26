using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TextMark.Data;
using TextMark.Models;
using Microsoft.AspNetCore.Http;



namespace TextMark.Controllers
{
    public class Admin_ClassificationLabelsTBController : Controller
    {
        private readonly TextMarkContext _context;
        public Admin_ClassificationLabelsTBController(TextMarkContext context)
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
            return View(await _context.ClassificationLabels_TB.Include("Projects_TB").Where(m => m.Project_ID.ToString() == Active_ProjectID).ToListAsync());
        }

        private async Task<bool> IsClassificationLabelDuplicated(string LabelText, int? ProjectID, int ClassificationLabel_ID)
        {
            var Label = await _context.ClassificationLabels_TB.FirstOrDefaultAsync(m => m.ClassificationLabel_Text.ToUpper() == LabelText.ToUpper() && m.Project_ID == ProjectID && m.ClassificationLabel_ID != ClassificationLabel_ID);
            if (Label == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task<bool> IsClassification_ShortCut_Duplicated(int? ProjectID, string ClassificationLabel_ShortCut_Key, int ClassificationLabel_ID)
        {
            var Label = await _context.ClassificationLabels_TB.FirstOrDefaultAsync(m => m.ClassificationLabel_ShortCut_Key.ToUpper() == ClassificationLabel_ShortCut_Key.ToUpper() && m.Project_ID == ProjectID && m.ClassificationLabel_ID != ClassificationLabel_ID);
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

            Select_All_ClassificationLabels_BGColours();
            Select_All_Projects();
            return View();
        }
        private async Task<bool> IsBGColour_Shortcut_Duplicated(string ClassificationShortcut_Key, int? ProjectID)
        {
            var ShortcutKey = await _context.ClassificationLabels_TB.FirstOrDefaultAsync(m => m.ClassificationLabel_ShortCut_Key == ClassificationShortcut_Key && m.Project_ID == ProjectID);

            if (ShortcutKey == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task<bool> IsBGColour_Duplicated(string ClassificationLabel_Background_Color, int? ProjectID)
        {
            var BG_Colour = await _context.Labels_TB.FirstOrDefaultAsync(m => m.Label_Background_Color == ClassificationLabel_Background_Color && m.Project_ID == ProjectID);

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
        public async Task<IActionResult> Create([Bind("ClassificationLabel_ID,ClassificationLabel_Text,Project_ID,ClassificationLabel_Background_Color,ClassificationLabel_ShortCut_Key")] ClassificationLabels_TB Classificationlabels_tb)
        {

            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (await IsClassificationLabelDuplicated(Classificationlabels_tb.ClassificationLabel_Text, Classificationlabels_tb.Project_ID, Classificationlabels_tb.ClassificationLabel_ID))
            {
                ViewBag.Error = "This Classification Label Text is already registered for this Project";
            }
            else if (await IsClassification_ShortCut_Duplicated(Classificationlabels_tb.Project_ID, Classificationlabels_tb.ClassificationLabel_ShortCut_Key, Classificationlabels_tb.ClassificationLabel_ID))
            {
                ViewBag.Error = "This Classification Label Shortcut Key is already registered for this Project";
            }
           
            else
            {
                if (ModelState.IsValid)
                {
                    Classificationlabels_tb.ClassificationLabel_ShortCut_Key = Classificationlabels_tb.ClassificationLabel_ShortCut_Key.ToUpper();
                    _context.Add(Classificationlabels_tb);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            Select_All_ClassificationLabels_BGColours();
            Select_All_Projects();
            return View(Classificationlabels_tb);
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

            var ClassificationLabels_tb = await _context.ClassificationLabels_TB.Include("Projects_TB").FirstOrDefaultAsync(m => m.ClassificationLabel_ID == id);
            if (ClassificationLabels_tb == null)
            {
                return NotFound();
            }

            return View(ClassificationLabels_tb);
        }
        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            Select_All_ClassificationLabels_BGColours();
            Select_All_Projects();
            if (id == null)
            {
                return NotFound();
            }

            var Classificationlabel = await _context.ClassificationLabels_TB.Include("Projects_TB").FirstOrDefaultAsync(m => m.ClassificationLabel_ID == id);
           
            if (Classificationlabel == null)
            {
                return NotFound();
            }
            return View(Classificationlabel);
        }
        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassificationLabel_ID,ClassificationLabel_Text,Project_ID,ClassificationLabel_Background_Color,ClassificationLabel_ShortCut_Key")] ClassificationLabels_TB ClassificationLabels_tb)
        {
            Select_All_ClassificationLabels_BGColours();
            Select_All_Projects();
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }


            if (id != ClassificationLabels_tb.ClassificationLabel_ID)
            {
                return NotFound();
            }
            if (await IsClassificationLabelDuplicated(ClassificationLabels_tb.ClassificationLabel_Text, ClassificationLabels_tb.Project_ID))
            {
                ViewBag.Error = "This Classification Label is already registered for this Project";
            }
            else if (await IsClassification_ShortCut_Duplicated(ClassificationLabels_tb.Project_ID, ClassificationLabels_tb.ClassificationLabel_ShortCut_Key))
            {
                ViewBag.Error = "This Classification Shortcut Key is already registered for this Project";                
            }            
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var entry = _context.Labels_TB.First(e => e.Label_ID == id);
                        ClassificationLabels_tb.ClassificationLabel_ShortCut_Key = ClassificationLabels_tb.ClassificationLabel_ShortCut_Key.ToUpper();
                        _context.Entry(entry).CurrentValues.SetValues(ClassificationLabels_tb);
                        await _context.SaveChangesAsync();
                        //return true;


                        //Labels_tb.Label_ShortCut_Key = Labels_tb.Label_ShortCut_Key.ToUpper();
                        //_context.Update(Labels_tb);
                        //await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClassificationLabelExists(ClassificationLabels_tb.ClassificationLabel_ID))
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
            return View(ClassificationLabels_tb);
        }

        private bool ClassificationLabelExists(int id)
        {
            return _context.ClassificationLabels_TB.Any(e => e.ClassificationLabel_ID == id);
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

            var Classificationlabel = await _context.ClassificationLabels_TB.Include("Projects_TB").FirstOrDefaultAsync(m => m.ClassificationLabel_ID == id);

            if (Classificationlabel == null)
            {
                return NotFound();
            }

            return View(Classificationlabel);
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

            var ClassificationLabel = await _context.ClassificationLabels_TB.FindAsync(id);
            _context.ClassificationLabels_TB.Remove(ClassificationLabel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public List<Projects_TB> Select_All_Projects()
        {
            ViewBag.Projects = _context.Projects_TB.ToList();

            return ViewBag.Projects;
        }
        public List<ClassificationLabels_TB> Select_All_ClassificationLabels_BGColours()
        {
            ViewBag.ClassificationLabels = _context.ClassificationLabels_TB.ToList();

            return ViewBag.ClassificationLabels;
        }
    }
}
