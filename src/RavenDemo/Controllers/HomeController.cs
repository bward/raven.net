using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raven;
using System.Diagnostics;

namespace RavenDemo.Controllers
{
    public class HomeController : Controller { 
    
        private readonly RavenClient _client;

        public HomeController(RavenClient client)
        {
            _client = client;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            ViewData["AuthenticationUrl"] = _client.AuthenticationUrl("this is a description");
            return View();
        }

        [HttpGet("/login")]
        public IActionResult Login([Bind(Prefix = "WLS-Response")] string parameters)
        {
            var response = _client.ParseResponse(parameters);
            Debug.WriteLine(response.Kid);
            return View();
        }
    }
}
