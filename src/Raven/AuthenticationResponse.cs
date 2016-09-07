using System;
using System.Linq;
using System.Net;

namespace Raven
{
    public class AuthenticationResponse
    {
        public int Ver;
        public HttpStatusCode Status;
        public string Msg;
        public DateTime Issue;
        public string Id;
        public string Url;
        public string Principal;
        public string[] Ptags;
        public string Auth;
        public string Sso;
        public string Life;
        public string Params;
        public string Kid;
        public string Sig;
        public string Signed;

        public static AuthenticationResponse AuthenticationResponseFromParameters(string[] parameters)
        {
            return new AuthenticationResponse
            {
                Ver = int.Parse(parameters[0]),
                Status = (HttpStatusCode) int.Parse(parameters[1]),
                Msg = parameters[2],
                Issue = ParseIssue(parameters[3]),
                Id = parameters[4],
                Url = Uri.UnescapeDataString(parameters[5]),
                Principal = parameters[6],
                Ptags = parameters[7].Split(','),
                Auth = parameters[8],
                Sso = parameters[9],
                Life = parameters[10],
                Params = parameters[11],
                Kid = parameters[12],
                Sig = parameters[13].Replace('-', '+').Replace('.', '/').Replace('_', '='),
                Signed = string.Join("!", parameters.Take(12))
        };
        }

        private static DateTime ParseIssue(string issue)
        {
            var year = int.Parse(issue.Substring(0, 4));
            var month = int.Parse(issue.Substring(4, 2));
            var day = int.Parse(issue.Substring(6, 2));
            var hour = int.Parse(issue.Substring(9, 2));
            var minute = int.Parse(issue.Substring(11, 2));
            var second = int.Parse(issue.Substring(13, 2));

            return new DateTime(year, month, day, hour, minute, second);
        }
    }
}