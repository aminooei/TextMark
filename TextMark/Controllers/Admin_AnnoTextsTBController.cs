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
using ServiceStack;
using System.IO;
using ExcelDataReader;
using PagedList;

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

        public ViewResult Index(int PageNum)
        {
            Select_All_File_Names();
            //if (!IsValidUser())
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            if(PageNum ==0)
            {
                PageNum = 1;
            }

            var Active_ProjectID = HttpContext.Session.GetString("Active_ProjectID");

            List_Annotation_Records LAR = new List_Annotation_Records();
            LAR.PageNum = PageNum;
            LAR.TotalNumPages = _context.Annotations_TB.Include("Projects_TB").Where(m => m.Project_ID.ToString() == Active_ProjectID).ToList().Count() / 10;

            if ((LAR.TotalNumPages % 10) > 1)
            {
                LAR.TotalNumPages += 1;
            }
            
            LAR.List_Annotation_Record = _context.Annotations_TB.Include("Projects_TB").Where(m => m.Project_ID.ToString() == Active_ProjectID).ToPagedList(PageNum, 10); 
           

            return View(LAR);

            //  return View(_context.Annotations_TB.Include("Projects_TB").Where(m => m.Project_ID.ToString() == Active_ProjectID).ToPagedList(PageNum,10));
            // return View(await _context.Annotations_TB.Include("Projects_TB").Where(m => m.Project_ID.ToString() == Active_ProjectID).Select(x => new { Annotation_ID = x.Annotation_ID, Annotation_ID_InFile = x.Annotation_ID_InFile, Annotation_Title = x.Annotation_Title, Annotation_Text = x.Annotation_Text, Annotation_Date = x.Annotation_Date, Annotation_Source = x.Annotation_Source, Source_File_Name = x.Source_File_Name, RowNumber = EF.Functions.RowNumber(x.Annotation_ID) }).Where(i => (i.RowNumber >= 1 && i.RowNumber <10)).ToListAsync());
            //  return View(students.ToPagedList(pageNumber, pageSize));

            //_context.Annotations_TB.Include("Projects_TB").Where(m => m.Project_ID.ToString() == Active_ProjectID).ToPagedList(PageNum, 10);
        }

        [HttpPost]
        public ViewResult Index(string Source_File_Name, int PageNum)
        {
            Select_All_File_Names();
            
            //if (!IsValidUser())
            //{
            //    return RedirectToAction("Index", "Login");
            //}

            // Select_All_Projects();
            if (PageNum == 0)
            {
                PageNum = 1;
            }

            // var Active_ProjectID = HttpContext.Session.GetString("Active_ProjectID");
            var Active_ProjectID = Convert.ToInt32(HttpContext.Session.GetString("Active_ProjectID"));
            List_Annotation_Records LAR = new List_Annotation_Records();
            LAR.PageNum = PageNum;
           

            if (Source_File_Name != "" && Active_ProjectID > 0)
            {
              //  return View(await _context.Annotations_TB.Where(m => m.Project_ID == Active_ProjectID && m.Source_File_Name == Source_File_Name).ToListAsync());

                
                LAR.TotalNumPages = _context.Annotations_TB.Include("Projects_TB").Where(m => m.Project_ID == Active_ProjectID && m.Source_File_Name == Source_File_Name).ToList().Count() / 10;

                if ((LAR.TotalNumPages % 10) > 1)
                {
                    LAR.TotalNumPages += 1;
                }

                LAR.List_Annotation_Record = _context.Annotations_TB.Include("Projects_TB").Where(m => m.Project_ID == Active_ProjectID && m.Source_File_Name == Source_File_Name).ToPagedList(PageNum, 10);
                return View(LAR);
            }
            else if (Source_File_Name == "")
            {
             //   return View(await _context.Annotations_TB.Where(m => m.Project_ID == Active_ProjectID).ToListAsync());

                LAR.TotalNumPages = _context.Annotations_TB.Include("Projects_TB").Where(m => m.Project_ID == Active_ProjectID).ToList().Count() / 10;

                if ((_context.Annotations_TB.Include("Projects_TB").Where(m => m.Project_ID == Active_ProjectID).ToList().Count() % 10) > 1)
                {
                    LAR.TotalNumPages += 1;
                }

                LAR.List_Annotation_Record = _context.Annotations_TB.Include("Projects_TB").Where(m => m.Project_ID == Active_ProjectID).ToPagedList(PageNum, 10);
                return View(LAR);
            }
            //else if (Active_ProjectID == 0)
            //{
            //    return View(await _context.Annotations_TB.Where(m => m.Source_File_Name == Source_File_Name).ToListAsync());
            //}

            //  return View(await _context.Annotations_TB.Include("Projects_TB").Where(m => m.Project_ID.ToString() == Active_ProjectID.ToString()).ToListAsync());
            return View(LAR);
        }

        public async Task<IActionResult> DeleteFilter(string Source_File_Name)
        {
            int Active_ProjectID = Convert.ToInt32(HttpContext.Session.GetString("Active_ProjectID"));

            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }


            //  var Assigned_Anno = await _context.Assigned_Annotations_ToUsers_TB.FindAsync(id);
            // _context.Assigned_Annotations_ToUsers_TB.Remove(Assigned_Anno);
            if (Source_File_Name != "" && Active_ProjectID > 0)
            {
                _context.Annotations_TB.RemoveRange(_context.Annotations_TB.Where(x => x.Source_File_Name == Source_File_Name && x.Project_ID == Active_ProjectID));
            }
            else if (Source_File_Name == "")
            {
                _context.Annotations_TB.RemoveRange(_context.Annotations_TB.Where(x => x.Project_ID == Active_ProjectID));
            }
            //else if (Active_ProjectID == 0)
            //{
            //    _context.Annotations_TB.RemoveRange(_context.Annotations_TB.Where(x => x.User_ID == User_ID));
            //}
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> IsAnnoDuplicated(string Anno_Text, int? ProjectID, int Anno_ID)
        {
            var Annotation = await _context.Annotations_TB.Include("Projects_TB").FirstOrDefaultAsync(m => m.Annotation_Text == Anno_Text && m.Project_ID == ProjectID &&  m.Annotation_ID != Anno_ID);
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
        //public async Task<IActionResult> Create([Bind("Annotation_ID,Annotation_Title,Annotation_Text,Project_ID")] Annotations_TB annotations_tb/*, UploadFile UploadFile*/)
        public async Task<IActionResult> Create(IFormFile ExcelFile, int Project_ID)        
        {
            List<Annotations_TB> users = new List<Annotations_TB>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream())
            {
                ExcelFile.CopyTo(stream);
                stream.Position = 0;
                
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        while (reader.Read() && reader.GetValue(0) != null) //Each row of the file
                        {
                        try
                            {
                                Annotations_TB annoTB = new Annotations_TB { Annotation_ID_InFile = reader.GetValue(0).ToString(), Annotation_Title = reader.GetValue(1).ToString(), Annotation_Text = reader.GetValue(2).ToString(), Annotation_Date = (Convert.ToDateTime(reader.GetValue(3).ToString())).ToString("dd-MM-yyyy"), Annotation_Source = reader.GetValue(4).ToString(), Source_File_Name = ExcelFile.FileName, Project_ID = Project_ID };
                               _context.Add(annoTB);
                               await _context.SaveChangesAsync();
                            }
                            catch
                            {

                            }
                           
                           
                        }
                    }


            }
            Select_All_Projects();
            return RedirectToAction(nameof(Index));

           
           
           
        }
      

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
        public async Task<IActionResult> Edit(int id, [Bind("Annotation_ID,Annotation_ID_InFile,Annotation_Title,Annotation_Text,Annotation_Date,Annotation_Source,Source_File_Name,Project_ID")] Annotations_TB Anno_tb)
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
            if (await IsAnnoDuplicated(Anno_tb.Annotation_Text, Anno_tb.Project_ID, Anno_tb.Annotation_ID))
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
        public void Select_All_File_Names()
        {
            var Active_ProjectID = HttpContext.Session.GetString("Active_ProjectID");    
            ViewBag.FileNames = _context.Annotations_TB.Where(m => m.Project_ID.ToString() == Active_ProjectID).Select(x => new { Source_File_Name = x.Source_File_Name }).Distinct().ToList();
           // return ViewBag.FileNames;
        }
    }
}
