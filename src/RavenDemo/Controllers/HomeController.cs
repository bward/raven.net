using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raven;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace RavenDemo.Controllers
{
    public class HomeController : Controller { 
    
        private readonly Client _client;

        public HomeController(Client client)
        {
            _client = client;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            ViewData["Status"] = HttpContext.User.Identity.IsAuthenticated;
            return View();
        }

        [Authorize]
        [HttpGet("/private")]
        public IActionResult Private()
        {
            ViewData["User"] = HttpContext.User.Identity.Name;
            return View();
        }
    }
}
