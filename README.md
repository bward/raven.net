# raven.net

.NET Core Ucam-WebAuth and Raven application agent

With raven.net, the University of Cambridge [Raven authentication service](https://raven.cam.ac.uk) can be easily integrated into any .NET Core application.

The library provides various features which can be used either individually to integrate Raven into a pre-existing authentication system, or together for a ready-to-use authentication package. A [demo project](/src/RavenDemo) demonstrating everything below is available.

# Quick start
Import the library with ```using BJW.Raven;```.
Inject a WebAuthClient:
~~~~ cs
public WebAuthClient RavenClientProvider(IServiceProvider provider)
{
  return new DemoWebAuthClient("http://localhost:63399", "/private");
}
~~~~
and then
~~~~ cs
services.AddSingleton(RavenClientProvider);
~~~~
in ```ConfigureServices```. Also in ```ConfigureServices```, load the controllers:
~~~~ cs
services.AddMvc().AddApplicationPart(Assembly.Load(new AssemblyName("BJW.Raven")))
~~~~
Finally, configure your app to use Raven cookie authentication in ```Configure```, above ```app.useMvc```:
~~~~ cs
app.UseCookieAuthentication(CookieAuthentication.DefaultOptions);
~~~~

Raven authentication is now enabled - to use it, just add ```[Authorise]``` attributes to to controllers which require authentication to access.
