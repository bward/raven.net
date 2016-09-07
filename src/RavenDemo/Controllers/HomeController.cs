using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raven;
using System.Diagnostics;
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
            ViewData["AuthenticationUrl"] = _client.AuthenticationUrl("this is a description");
            return View();
        }

        [HttpGet("/login")]
        public IActionResult Login([Bind(Prefix = "WLS-Response")] string parameters)
        {
            var response = _client.ParseResponse(parameters);
            Debug.WriteLine(_client.Verify(response));
            ViewData["Params"] = parameters;
            ViewData["Signed"] = response.Signed;
            ViewData["Kid"] = response.Kid;
            ViewData["Sig"] = response.Sig;
            ViewData["Results"] = _client.Verify(response);
            return View();
        }
    }
}
