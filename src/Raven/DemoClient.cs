namespace Raven
{
    public class DemoClient : Client
    {
        protected new const string BaseUrl = "https://demo.raven.cam.ac.uk/auth/authenticate.html";
        public DemoClient(string url) : base(url) { }
    }
}
