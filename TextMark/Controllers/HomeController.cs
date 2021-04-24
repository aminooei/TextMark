using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TextMark.Data;
using TextMark.Models;
using Microsoft.EntityFrameworkCore;

namespace TextMark.Controllers
{
    public class HomeController : Controller
    {        
        private readonly TextMarkContext _context;

        public HomeController(TextMarkContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            
            return  View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
