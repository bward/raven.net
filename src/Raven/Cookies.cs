using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Raven
{
    public class Cookies
    {
        public static CookieAuthenticationOptions AuthenticationOptions =>
            new CookieAuthenticationOptions
            {
                AuthenticationScheme = "RavenCookieMiddlewareInstance",
                LoginPath = new PathString("/Raven/Unauthorised/"),
                AccessDeniedPath = new PathString("/Raven/Forbidden/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            };
    }
}
