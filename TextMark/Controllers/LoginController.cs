using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TextMark.Data;
using TextMark.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace TextMark.Controllers
{
    public class LoginController : Controller
    {
        private readonly TextMarkContext _context;
        public LoginController(TextMarkContext context)
        {            
            _context = context;

           
        }

        public IActionResult Index()
        {
            ResetUserDetails();
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        private void ResetUserDetails()
        {
            HttpContext.Session.SetString("UserType", "");
            HttpContext.Session.SetString("UserID", "");
            HttpContext.Session.SetString("Username", "");
           // TempData["Username"] = "";
        }
        public void Check_Initialize_Admin()
        {
            var count_Projects = _context.Projects_TB.ToList().Count();
            if (count_Projects == 0)
            {
                Create_Project();
                Create_Role();
                Create_User();
                //Create_Labels_BG_Colour_ShortKey();
                Create_Labels();
                Create_Annotation_Texts();
                Create_Assigned_TextAnnotations();
                Create_Assigned_TextClassifications();
            }
        }
        public  IActionResult DetailsAsync(string username, string password)
        {
            Check_Initialize_Admin();

            var login_admin = _context.Users_TB.Include("Roles_TB")
               .FirstOrDefault(m => m.Username == username && m.Password == password && m.Roles_TB.Role_Text == "ADMIN");

            if (login_admin != null)               
            {   
                HttpContext.Session.SetString("UserType", "ADMIN");
                HttpContext.Session.SetString("UserID", login_admin.User_ID.ToString());
                HttpContext.Session.SetString("Username", username);
                //TempData["Username"] = username;
                return RedirectToAction("Index", "Admin_ProjectsTB");
            }

            else
            {
              
                var login_user = _context.Users_TB.Include("Roles_TB").Select(c => new { Username = c.Username, Password = c.Password , Role = c.Roles_TB.Role_Text , Project_ID = c.Roles_TB.Project_ID, User_ID = c.User_ID})
              .FirstOrDefault(m => m.Username == username && m.Password == password && m.Role != "ADMIN");

                if (login_user != null)
                {
                    HttpContext.Session.SetString("UserType", "USER");
                    HttpContext.Session.SetString("UserID", login_user.User_ID.ToString());
                    HttpContext.Session.SetString("ProjectID", login_user.Project_ID.ToString());
                    HttpContext.Session.SetString("Username", username);
                    TempData["UserID"] = login_user.User_ID.ToString();
                    TempData["Username"] = username;
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Login");


        }
        public  void Create_Role()
        {    
            Roles_TB RT2 = new Roles_TB();
            RT2.Role_Text = "User";
            RT2.Project_ID = 1;
            _context.Add(RT2);

            Roles_TB RT1 = new Roles_TB();
            RT1.Role_Text = "Admin";
            RT1.Project_ID = 1;
            _context.Add(RT1);

            _context.SaveChanges(); 
            
        }

        public void Create_Project()
        {  
            Projects_TB PT1 = new Projects_TB();
            PT1.Project_Name = "Project 1";
            PT1.Is_Active = true;
            _context.Add(PT1);

            Projects_TB PT2 = new Projects_TB();
            PT2.Project_Name = "Project 2";
            PT2.Is_Active = false;
            _context.Add(PT2);
            _context.SaveChanges();
        }

        public void Create_User()
        {
            Users_TB UT2 = new Users_TB();
            UT2.Username = "hadi1";
            UT2.Password = "12345";
            UT2.ConfirmPassword = "12345";
            UT2.Role_ID = 2;
            _context.Add(UT2);

            Users_TB UT1 = new Users_TB();
            UT1.Username = "aminoo";
            UT1.Password = "12345";
            UT1.ConfirmPassword = "12345";
            UT1.Role_ID = 1;          
            _context.Add(UT1);

            _context.SaveChanges();
        }

        //public void Create_Labels_BG_Colour_ShortKey()
        //{
        //    Labels_BG_Colours_TB LBG1 = new Labels_BG_Colours_TB();
        //    LBG1.Label_Background_Color = "#f2711c";
        //    LBG1.Label_ShortCut_Key = "R";
        //    _context.Add(LBG1);

        //    Labels_BG_Colours_TB LBG2 = new Labels_BG_Colours_TB();
        //    LBG2.Label_Background_Color = "#009c95";
        //    LBG2.Label_ShortCut_Key = "B";
        //    _context.Add(LBG2);

        //    Labels_BG_Colours_TB LBG3 = new Labels_BG_Colours_TB();
        //    LBG3.Label_Background_Color = "#2185d0";
        //    LBG3.Label_ShortCut_Key = "G";
        //    _context.Add(LBG3);
        //    _context.SaveChanges();
        //}

        public void Create_Labels()
        {
            Labels_TB LB1 = new Labels_TB();
            // LB1.Label_BGColour_ID = 1;
            LB1.Label_Background_Color = "#f2711c";
            LB1.Label_ShortCut_Key = "R";
            LB1.Label_Text = "Location";
            LB1.Project_ID = 1;
            _context.Add(LB1);

            Labels_TB LB2 = new Labels_TB();
            //LB2.Label_BGColour_ID = 2;
            LB2.Label_Background_Color = "#009c95";
            LB2.Label_ShortCut_Key = "B";
            LB2.Label_Text = "City";
            LB2.Project_ID = 1;
            _context.Add(LB2);

            Labels_TB LB3 = new Labels_TB();
            //LB3.Label_BGColour_ID = 3;
            LB3.Label_Background_Color = "#2185d0";
            LB3.Label_ShortCut_Key = "G";
            LB3.Label_Text = "Country";
            LB3.Project_ID = 1;
            _context.Add(LB3);
            _context.SaveChanges();
        }

        public void Create_Annotation_Texts()
        {
            Annotations_TB Anno1 = new Annotations_TB();
            Anno1.Project_ID = 1;
            Anno1.Annotation_ID_InFile = "abcd1";
            Anno1.Annotation_Title = "Anno Title 1 dfgadsgagasdgasdgasdgadgasdgasd";
            Anno1.Annotation_Text = "Marketing may be quite effective when done correctly; marketing can increase brand recognition, position your organization as the ideal answer for potential clients, and eventually generate leads and sales, (CLICK, 2020). A business plan outlines and directs your company's operating activities";
            Anno1.Annotation_Date = "01/01/2020";
            Anno1.Annotation_Source = "Yahoo";
            Anno1.Source_File_Name = "file1.xls";
            _context.Add(Anno1);

            Annotations_TB Anno2 = new Annotations_TB();
            Anno2.Project_ID = 1;
            Anno2.Annotation_ID_InFile = "abcd2";
            Anno2.Annotation_Title = "Anno Title 2 asdgasdgahgah dfg dgjdfgj hsfghshsdfhsdfhs";
            Anno2.Annotation_Text = "Increase brand recognition, position your organization Marketing may be quite effective when done correctly; marketing can as the ideal answer for potential clients, and eventually generate leads and sales, (CLICK, 2020). A business plan outlines and directs your company's operating activities";
            Anno2.Annotation_Date = "05/11/2021";
            Anno2.Annotation_Source = "Google";
            Anno2.Source_File_Name = "file1.xls";
            _context.Add(Anno2);

            Annotations_TB Anno3 = new Annotations_TB();
            Anno3.Project_ID = 1;
            Anno3.Annotation_ID_InFile = "abcd3";
            Anno3.Annotation_Title = "Anno Title 3";
            Anno3.Annotation_Text = "Effective when done correctly; marketing can as increase brand recognition, position your organization Marketing may be quite the ideal answer for potential clients, and eventually generate leads and sales, (CLICK, 2020). A business plan outlines and directs your company's operating activities";
            Anno3.Annotation_Date = "01/01/2020";
            Anno3.Annotation_Source = "MSN";
            Anno3.Source_File_Name = "file1.xls";
            _context.Add(Anno3);
            _context.SaveChanges();
        }

       

        public void Create_Assigned_TextAnnotations()
        {
            Assigned_Annotations_ToUsers_TB Assigned_Anno1 = new Assigned_Annotations_ToUsers_TB();
            Assigned_Anno1.Project_ID = 1;
            Assigned_Anno1.User_ID = 2;
            Assigned_Anno1.Annotation_ID = 1;
            Assigned_Anno1.Not_Sure = false;
            Assigned_Anno1.Comments = null;
            Assigned_Anno1.Annotated_Text= "Marketing may be quite effective when done correctly; marketing can increase brand recognition, position your organization as the ideal answer for potential clients, and eventually generate leads and sales, (CLICK, 2020). A business plan outlines and directs your company's operating activities";
            _context.Add(Assigned_Anno1);

            Assigned_Annotations_ToUsers_TB Assigned_Anno2 = new Assigned_Annotations_ToUsers_TB();
            Assigned_Anno2.Project_ID = 1;
            Assigned_Anno2.User_ID = 2;
            Assigned_Anno2.Annotation_ID = 2;
            Assigned_Anno2.Not_Sure = false;
            Assigned_Anno2.Comments = "Some comments ......";
            Assigned_Anno2.Annotated_Text = "Increase brand recognition, position your organization Marketing may be quite effective when done correctly; marketing can as the ideal answer for potential clients, and eventually generate leads and sales, (CLICK, 2020). A business plan outlines and directs your company's operating activities";
            _context.Add(Assigned_Anno2);

            Assigned_Annotations_ToUsers_TB Assigned_Anno3 = new Assigned_Annotations_ToUsers_TB();
            Assigned_Anno3.Project_ID = 1;
            Assigned_Anno3.User_ID = 2;
            Assigned_Anno3.Annotation_ID = 3;
            Assigned_Anno3.Not_Sure = true;
            Assigned_Anno3.Comments = null;
            Assigned_Anno3.Annotated_Text = "Effective when done correctly; marketing can as increase brand recognition, position your organization Marketing may be quite the ideal answer for potential clients, and eventually generate leads and sales, (CLICK, 2020). A business plan outlines and directs your company's operating activities";
            _context.Add(Assigned_Anno3);
            _context.SaveChanges();
        }

        public void Create_Assigned_TextClassifications()
        {
            Assigned_TextClassifications_ToUsers_TB Assigned_TextClassification1 = new Assigned_TextClassifications_ToUsers_TB();
            Assigned_TextClassification1.Project_ID = 1;
            Assigned_TextClassification1.User_ID = 2;
            Assigned_TextClassification1.TextClassification_ID = 1;
            Assigned_TextClassification1.Not_Sure = false;
            Assigned_TextClassification1.Comments = null;
            Assigned_TextClassification1.TextClassification_Text = "Marketing may be quite effective when done correctly; marketing can increase brand recognition, position your organization as the ideal answer for potential clients, and eventually generate leads and sales, (CLICK, 2020). A business plan outlines and directs your company's operating activities";
            _context.Add(Assigned_TextClassification1);

            Assigned_TextClassifications_ToUsers_TB Assigned_TextClassification2 = new Assigned_TextClassifications_ToUsers_TB();
            Assigned_TextClassification2.Project_ID = 1;
            Assigned_TextClassification2.User_ID = 2;
            Assigned_TextClassification2.TextClassification_ID = 2;
            Assigned_TextClassification2.Not_Sure = false;
            Assigned_TextClassification2.Comments = "Some comments ......";
            Assigned_TextClassification2.TextClassification_Text = "Increase brand recognition, position your organization Marketing may be quite effective when done correctly; marketing can as the ideal answer for potential clients, and eventually generate leads and sales, (CLICK, 2020). A business plan outlines and directs your company's operating activities";
            _context.Add(Assigned_TextClassification2);

            Assigned_TextClassifications_ToUsers_TB Assigned_TextClassification3 = new Assigned_TextClassifications_ToUsers_TB();
            Assigned_TextClassification3.Project_ID = 1;
            Assigned_TextClassification3.User_ID = 2;
            Assigned_TextClassification3.TextClassification_ID = 3;
            Assigned_TextClassification3.Not_Sure = true;
            Assigned_TextClassification3.Comments = null;
            Assigned_TextClassification3.TextClassification_Text = "Effective when done correctly; marketing can as increase brand recognition, position your organization Marketing may be quite the ideal answer for potential clients, and eventually generate leads and sales, (CLICK, 2020). A business plan outlines and directs your company's operating activities";
            _context.Add(Assigned_TextClassification3);
            _context.SaveChanges();
        }
    }
}
