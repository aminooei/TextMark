using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TextMark.Data;
using TextMark.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TextMark.Controllers
{
    //[Authorize]
    public class Admin_Assigned_AnnotationsTBController : Controller
    {
        private readonly TextMarkContext _context;
        
      
        public Admin_Assigned_AnnotationsTBController(TextMarkContext context)
        {
            _context = context;           
        }

        public  ActionResult Index()
        {
            Details_Assigned_TextAnnotations_ToUsers DT = new Details_Assigned_TextAnnotations_ToUsers();
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }
            Select_All_Users();
            int Active_ProjectID = Convert.ToInt32(HttpContext.Session.GetString("Active_ProjectID"));
            int Selected_User_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_User_ID"));
            if (Selected_User_ID == 0)
            {
                DT.allAnnotations =  _context.Assigned_Annotations_ToUsers_TB.Where(m => m.Project_ID == 0 && m.User_ID == 0).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToList();
                DT.Annotated_Tags =  Select_All_Annotated_Tags(Selected_User_ID, Active_ProjectID);
                return View(DT);
            }
            else
            {
                DT.allAnnotations = _context.Assigned_Annotations_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID && m.User_ID == Selected_User_ID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToList();
                DT.Annotated_Tags = Select_All_Annotated_Tags(Selected_User_ID, Active_ProjectID);
                return View(DT);
            }
        }

        [HttpPost]
        public  IActionResult Index(int User_ID)
        {
            int Active_ProjectID = Convert.ToInt32(HttpContext.Session.GetString("Active_ProjectID"));
           

            Details_Assigned_TextAnnotations_ToUsers DT = new Details_Assigned_TextAnnotations_ToUsers();

            HttpContext.Session.SetString("Selected_User_ID", User_ID.ToString());
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }
            Select_All_Users();
            Select_All_Projects();
            if (User_ID > 0 && Active_ProjectID > 0)
            {
               // return View(await _context.Assigned_Annotations_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID && m.User_ID == User_ID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync());
                DT.allAnnotations = _context.Assigned_Annotations_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID && m.User_ID == User_ID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToList();
                DT.Annotated_Tags = Select_All_Annotated_Tags(User_ID, Active_ProjectID);
                return View(DT);
            }
            else if(User_ID == -2)
            {
                DT.allAnnotations = _context.Assigned_Annotations_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToList();
                DT.Annotated_Tags = Select_All_Annotated_Tags_ForAllUsers(Active_ProjectID);
                return View(DT);
            }
           
            DT.allAnnotations = _context.Assigned_Annotations_ToUsers_TB.Where(m => m.User_ID == 0 && m.Project_ID == 0).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToList();
            DT.Annotated_Tags = Select_All_Annotated_Tags(0, 0);
            return View(DT);
        }

        [HttpGet]
        public void Export(Assigned_Annotations_ToUsers_TB Assigned_Annotations_ToUsers)
        {
           



            //// var bookings = _cache.GetOrCreate(BookingsCacheKey, BookingListFactory);
            //int Active_ProjectID = Convert.ToInt32(HttpContext.Session.GetString("Active_ProjectID"));
            //int Selected_User_ID = Convert.ToInt32(HttpContext.Session.GetString("Selected_User_ID"));

            //var User_Annotated_List = _context.Assigned_Annotations_ToUsers_TB.Where(m => m.Project_ID == Active_ProjectID && m.User_ID == Selected_User_ID).Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").ToListAsync();
            //string output_STR = JsonConvert.SerializeObject(User_Annotated_List);

            //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(output_STR);
            //var output = new FileContentResult(bytes, "application/octet-stream");
            //output.FileDownloadName = "download.txt";

            //return output;
        }
        public Assigned_Annotations_ToUsers_TB Selected_Assigned_Annotation(int Assigned_Anno_ID)
        {
            
                var Selected_Annotation = _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB")
               .FirstOrDefault(m => m.Assigned_Anno_ID == Assigned_Anno_ID);
                //.FirstOrDefault(m => m.Assigned_Anno_ID == id && m.User_ID == UserID && m.Project_ID == Project_ID);
                if (Selected_Annotation == null)
                {
                    return new Assigned_Annotations_ToUsers_TB();
                }
                return Selected_Annotation;
            
        }

        public List<Labels_TB> Select_Annotation_Labels(int? Project_ID = 0)
        {
            var Labels = _context.Labels_TB.Include("Projects_TB").Where(m => m.Project_ID == Project_ID).ToList();
            //var Labels = _context.Labels_TB.Include("Projects_TB").Include("Labels_BG_Colours_TB").Where(m => m.Project_ID.ToString() == HttpContext.Session.GetString("ProjectID")).ToList();
            return Labels;
        }
       
        private async Task<bool> IsAssignedAnnoDuplicated(int Anno_ID,int UserID, int? ProjectID)
        {
            var Annotation = await _context.Assigned_Annotations_ToUsers_TB.Include("Projects_TB").FirstOrDefaultAsync(m => m.User_ID == UserID && m.Annotation_ID == Anno_ID  && m.Project_ID == ProjectID);
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

            Select_All_Users();
            Select_All_Annotations();
            Select_All_Projects();
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Assigned_Anno_ID,User_ID,Annotation_ID,Project_ID,Annotated_Text,Count_Annotations")] Assigned_Annotations_ToUsers_TB assigned_annotations_tousers_tb)
        //{
        //    if (!IsValidUser())
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }

        //    if (await IsAssignedAnnoDuplicated(assigned_annotations_tousers_tb.Annotation_ID,assigned_annotations_tousers_tb.User_ID,assigned_annotations_tousers_tb.Project_ID))
        //    {
        //        ViewBag.Error = "This Annotation is already assigned to this user in this project";

        //    }
        //    else
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var a = _context.Annotations_TB.FirstOrDefault(m => m.Annotation_ID == assigned_annotations_tousers_tb.Annotation_ID);
        //            assigned_annotations_tousers_tb.Annotated_Text = a.Annotation_Text;
        //            _context.Add(assigned_annotations_tousers_tb);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    Select_All_Projects();
        //    Select_All_Annotations();
        //    Select_All_Users();
        //    return View(assigned_annotations_tousers_tb);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Project_ID, int User_ID)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            
            var a = _context.Annotations_TB.Where(m => m.Project_ID == Project_ID).ToList();
            
            foreach (var item in a)
            {
               // assigned_annotations_tousers_tb.Annotated_Text = item.Annotation_Text;
              Assigned_Annotations_ToUsers_TB Assigned_Anno = new Assigned_Annotations_ToUsers_TB { Annotation_ID = item.Annotation_ID, Annotated_Text = item.Annotation_Text, User_ID = User_ID, Project_ID = Project_ID, Count_Annotations = 0, Not_Sure = false, Comments = "" };
                _context.Add(Assigned_Anno);
                await _context.SaveChangesAsync();
            }
                  
               
            Select_All_Projects();
            Select_All_Annotations();
            Select_All_Users();
            return RedirectToAction(nameof(Index));
        }

        public  List<AnnotatedTexts_Tags> Select_All_Annotated_Tags(int User_ID, int Project_ID)
        {
            var Annotated_tags =  _context.AnnotatedTexts_Tags.Where(m => m.Assigned_Annotations_ToUsers_TB.User_ID == User_ID && m.Assigned_Annotations_ToUsers_TB.Project_ID == Project_ID).Include("Labels_TB").Include("Assigned_Annotations_ToUsers_TB.Users_TB").ToList();
            return Annotated_tags;
        }

        public List<AnnotatedTexts_Tags> Select_All_Annotated_Tags_ForAllUsers(int Project_ID)
        {
            var Annotated_tags = _context.AnnotatedTexts_Tags.Where(m => m.Assigned_Annotations_ToUsers_TB.Project_ID == Project_ID).Include("Labels_TB").Include("Assigned_Annotations_ToUsers_TB.Users_TB").ToList();
            return Annotated_tags;
        }
        public List<AnnotatedTexts_Tags> Select_All_Annotated_Tags_For_a_Record(int id)
        {
            var Annotated_tags_For_A_Record = _context.AnnotatedTexts_Tags.Where(m => m.Assigned_TextAnnotation_ID == id).ToList();
            return Annotated_tags_For_A_Record;
        }

        public ActionResult Details(int id, int projectID)
        {
            Details_Assigned_TextAnnotations_ToUsers HP = new Details_Assigned_TextAnnotations_ToUsers();           
            HP.allLabels = Select_Annotation_Labels(projectID);
            HP.Selected_Assigned_Annotation = Selected_Assigned_Annotation(id);
            HP.Annotated_Tags = Select_All_Annotated_Tags_For_a_Record(id);
            return View(HP);
        }

       

        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            Select_All_Users();
            Select_All_Annotations();
            Select_All_Projects();
            
            if (id == null)
            {
                return NotFound();
            }

            //   var login = await _context.Users_TB.FindAsync(id);
            var Assigned_Anno = await _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").FirstOrDefaultAsync(m => m.Assigned_Anno_ID == id);
            if (Assigned_Anno == null)
            {
                return NotFound();
            }
            return View(Assigned_Anno);
        }
        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Assigned_Anno_ID,User_ID,Annotation_ID,Project_ID,Count_Annotations,Not_Sure,Comments")] Assigned_Annotations_ToUsers_TB Assigned_Anno)
        {
            Select_All_Users();
            Select_All_Annotations();
            Select_All_Projects();

            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id != Assigned_Anno.Assigned_Anno_ID)
            {
                return NotFound();
            }

            if (await IsAssignedAnnoDuplicated(Assigned_Anno.Annotation_ID, Assigned_Anno.User_ID, Assigned_Anno.Project_ID))
            {
                ViewBag.Error = "This Annotation is already assigned to this user in this project";

            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Assigned_Anno);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!Assigned_Anno_Exists(Assigned_Anno.Assigned_Anno_ID))
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
            return View(Assigned_Anno);
        }

        private bool Assigned_Anno_Exists(int id)
        {
            return _context.Assigned_Annotations_ToUsers_TB.Any(e => e.Assigned_Anno_ID == id);
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

            var Assigned_Anno = await _context.Assigned_Annotations_ToUsers_TB.Include("Users_TB").Include("Annotations_TB").Include("Projects_TB").FirstOrDefaultAsync(m => m.Assigned_Anno_ID == id);

            if (Assigned_Anno == null)
            {
                return NotFound();
            }

            return View(Assigned_Anno);
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

            var Assigned_Anno = await _context.Assigned_Annotations_ToUsers_TB.FindAsync(id);
            _context.Assigned_Annotations_ToUsers_TB.Remove(Assigned_Anno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteFilter(int User_ID)
        {
            int Active_ProjectID = Convert.ToInt32(HttpContext.Session.GetString("Active_ProjectID"));

            if (!IsValidUser())
            {
                return RedirectToAction("Index", "Login");
            }


            //  var Assigned_Anno = await _context.Assigned_Annotations_ToUsers_TB.FindAsync(id);
            // _context.Assigned_Annotations_ToUsers_TB.Remove(Assigned_Anno);
            if (User_ID > 0 && Active_ProjectID > 0)
            {
                _context.Assigned_Annotations_ToUsers_TB.RemoveRange(_context.Assigned_Annotations_ToUsers_TB.Where(x => x.User_ID == User_ID && x.Project_ID == Active_ProjectID));
            }
            else if (User_ID == 0)
            {
                _context.Assigned_Annotations_ToUsers_TB.RemoveRange(_context.Assigned_Annotations_ToUsers_TB.Where(x => x.Project_ID == Active_ProjectID));
            }
            //else if (Active_ProjectID == 0)
            //{
            //    _context.Assigned_Annotations_ToUsers_TB.RemoveRange(_context.Assigned_Annotations_ToUsers_TB.Where(x => x.User_ID == User_ID));
            //}
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public List<Users_TB> Select_All_Users()
        {
            var Active_ProjectID = HttpContext.Session.GetString("Active_ProjectID");
            var users = _context.Users_TB.Where(m => m.Roles_TB.Role_Text.ToLower() != "admin" && m.Roles_TB.Project_ID.ToString() == Active_ProjectID).ToList();
            users.Add(new Users_TB { User_ID = -2 , Username = "All Users", Password = "12345", ConfirmPassword = "12345", Role_ID = users[0].Role_ID });
            ViewBag.Users = users;
            return ViewBag.Users;
        }
        public List<Annotations_TB> Select_All_Annotations()
        {
            ViewBag.Annotations = _context.Annotations_TB.ToList();
            return ViewBag.Annotations;
        }
        public List<Projects_TB> Select_All_Projects()
        {
            ViewBag.Projects = _context.Projects_TB.ToList();
            return ViewBag.Projects;
        }
    }
}
