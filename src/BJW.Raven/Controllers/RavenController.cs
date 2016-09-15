using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BJW.Raven;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BJW.Raven.Controllers
{
    public class RavenController : Controller { 
    
        private readonly WebAuthClient _client;

        public RavenController(WebAuthClient client)
        {
            _client = client;
        }

        public IActionResult Unauthorised(string returnUrl)
        {
            var failureUrl = new Uri(Request.Headers["Referer"].ToString() ?? "/");
            return Redirect(_client.AuthenticationUrl(returnUrl, failureUrl.AbsolutePath));
        }

        public async Task<IActionResult> Login([Bind(Prefix = "WLS-Response")] string parameters)
        {
            var response = _client.ParseResponse(parameters);

            if (_client.Verify(response))
            {
                var claims = new List<Claim> {new Claim(ClaimTypes.Name, response.Principal)};
                var identity = new ClaimsIdentity(claims, "RavenCookieMiddlewareInstance");
                await
                    HttpContext.Authentication.SignInAsync("RavenCookieMiddlewareInstance",
                        new ClaimsPrincipal(identity));
            }
            else
            {
                if (response.Status == HttpStatusCode.Gone)
                    return Redirect(response.Params["failureUrl"]);
                return RedirectToAction("LoginFailed", new {returnUrl = response.Params["failureUrl"]});
            }

            return Redirect(response.Params["returnUrl"]);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("RavenCookieMiddlewareInstance");
            return Redirect("/");
        }

        public IActionResult LoginFailed(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }
    }
}
