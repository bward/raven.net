namespace BJW.Raven
{
    public class DemoWebAuthClient : WebAuthClient
    {
        protected override string BaseUrl => "https://demo.raven.cam.ac.uk/auth/authenticate.html";
        protected override string[] Kids => new[] { "901" };
        public DemoWebAuthClient(string baseUrl) : base(baseUrl) { }
    }
}
