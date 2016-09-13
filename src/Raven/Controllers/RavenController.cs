using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raven;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Raven.Controllers
{
    public class RavenController : Controller { 
    
        private readonly Client _client;

        public RavenController(Client client)
        {
            _client = client;
        }

        public IActionResult Unauthorised()
        {
            Debug.WriteLine("Raven unauthorised");
            return Redirect(_client.AuthenticationUrl().AbsoluteUri);
        }

        public async Task<IActionResult> Login([Bind(Prefix = "WLS-Response")] string parameters)
        {
            var response = _client.ParseResponse(parameters);
            Debug.WriteLine(_client.Verify(response));

            if (_client.Verify(response))
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, response.Principal));
                var identity = new ClaimsIdentity(claims, "RavenCookieMiddlewareInstance");
                await HttpContext.Authentication.SignInAsync("RavenCookieMiddlewareInstance", new ClaimsPrincipal(identity));
                Debug.WriteLine("logged in!");
                Debug.WriteLine(_client.RedirectUrl);
            }

            return Redirect(_client.RedirectUrl);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("RavenCookieMiddlewareInstance");
            return Redirect("/");
        }
    }
}
