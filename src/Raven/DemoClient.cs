namespace Raven
{
    public class DemoClient : Client
    {
        protected override string BaseUrl => "https://demo.raven.cam.ac.uk/auth/authenticate.html";
        protected override string[] Kids => new[] { "901" };
        public DemoClient(string baseUrl, string redirectUrl) : base(baseUrl, redirectUrl) { }
    }
}
