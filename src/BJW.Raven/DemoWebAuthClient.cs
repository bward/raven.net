namespace BJW.Raven
{
    public class DemoWebAuthClient : WebAuthClient
    {
        public DemoWebAuthClient(string hostName) : base(hostName)
        {
        }

        protected override string BaseUrl => "https://demo.raven.cam.ac.uk/auth/authenticate.html";
        protected override string[] Kids => new[] {"901"};
    }
}