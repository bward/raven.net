using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Raven
{
    public class Client
    {
        public string RedirectUrl => _baseUrl + _redirectUrl;
        protected virtual string BaseUrl => "https://raven.cam.ac.uk/auth/authenticate.html";
        protected virtual string[] Kids => new [] {"2"};
        private readonly string _baseUrl;
        private readonly string _redirectUrl;

        public Client(string baseUrl, string redirectUrl)
        {
            _baseUrl = baseUrl;
            _redirectUrl = redirectUrl;
        }

        public Uri AuthenticationUrl(string desc = "")
        {
            var queryString = Uri.EscapeUriString("?ver=3&url=" + _baseUrl + "/raven/login" + "&desc=" + desc);
            return new Uri(BaseUrl + queryString);
        }

        public AuthenticationResponse ParseResponse(string data)
        {
            var parameters = data.Split('!');
            return AuthenticationResponse.AuthenticationResponseFromParameters(parameters);
        }

        public bool Verify(AuthenticationResponse response)
        {
            if (!Kids.Contains(response.Kid))
                return false;

            var now = DateTime.UtcNow;
            var difference = now.Subtract(response.Issue);
            if (difference.TotalSeconds > 30)
                return false;

            if (response.Auth != "pwd" && response.Sso != "pwd")
                return false;

            if (response.Url != _baseUrl + "/raven/login")
                return false;

            using (var rsa = KeyProvider.RSAFromKey(response.Kid))
            {
                return rsa.VerifyData(
                    Encoding.ASCII.GetBytes(response.Signed),
                    Convert.FromBase64String(response.Sig),
                    HashAlgorithmName.SHA1,
                    RSASignaturePadding.Pkcs1);
            }
        }
    }
}
