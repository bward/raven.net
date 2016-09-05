using System;
using System.Collections.Generic;
using System.Net;

namespace Raven
{
    public class RavenClient
    {
        private readonly string _url;
        private const string BaseUrl = "https://demo.raven.cam.ac.uk/auth/authenticate.html";

        public RavenClient(string url)
        {
            _url = url;
        }

        public Uri AuthenticationUrl(string desc = "")
        {
            var queryString = Uri.EscapeUriString("?ver=3&url=" + _url + "&desc=" + desc);
            return new Uri(BaseUrl + queryString);
        }

        public AuthenticationResponse ParseResponse(string response)
        {
            var parameters = response.Split('!');
            return new AuthenticationResponse
            {
                Ver = int.Parse(parameters[0]),
                Status = (HttpStatusCode) int.Parse(parameters[1]),
                Msg = parameters[2],
                Issue = parameters[3],
                Id = parameters[4],
                Url = new Uri(Uri.UnescapeDataString(parameters[5])),
                Principal = parameters[6],
                Ptags = parameters[7].Split(','),
                Auth = parameters[8],
                Sso = parameters[9],
                Life = parameters[10],
                Params = parameters[11],
                Kid = parameters[12],
                Sig = parameters[13]
            };
        }
    }
}
