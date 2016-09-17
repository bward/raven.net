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
                LoginPath = new PathString("/raven/unauthorised/"),
                AccessDeniedPath = new PathString("/raven/forbidden/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            };
    }
}
