using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BJW.Raven
{
    public class CookieAuthentication
    {
        public static CookieAuthenticationOptions DefaultOptions =>
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
