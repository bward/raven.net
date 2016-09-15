using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BJW.Raven
{
    public class WebAuthClient
    {
        protected virtual string BaseUrl => "https://raven.cam.ac.uk/auth/authenticate.html";
        protected virtual string[] Kids => new [] {"2"};
        private readonly string _hostName;

        public WebAuthClient(string hostName)
        {
            _hostName = hostName;
        }

        public string AuthenticationUrl(string returnUrl = "/", string failureUrl = "/", string desc = "")
        {
            var queryString = "?ver=3&url=" + _hostName + "/raven/login" + "&desc=" + Uri.EscapeDataString(desc) + "&params=" + Uri.EscapeDataString("returnUrl=" + returnUrl + "&failureUrl=" + failureUrl);
            return BaseUrl + queryString;
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

            if (response.Url != _hostName + "/raven/login")
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
