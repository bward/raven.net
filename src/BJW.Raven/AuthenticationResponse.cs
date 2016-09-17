using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace BJW.Raven
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
        public Dictionary<string, StringValues> Params;
        public string Kid;
        public string Sig;
        public string Signed;

        public static AuthenticationResponse AuthenticationResponseFromParameters(string[] parameters)
        {
            var protocolVersion = int.Parse(parameters[0]);
            var indexOffset = protocolVersion == 3 ? 0 : -1;

            return new AuthenticationResponse
            {
                Ver = int.Parse(parameters[0]),
                Status = (HttpStatusCode) int.Parse(parameters[1]),
                Msg = parameters[2],
                Issue = ParseIssue(parameters[3]),
                Id = parameters[4],
                Url = Uri.UnescapeDataString(parameters[5]),
                Principal = parameters[6],
                Ptags = protocolVersion == 3 ? parameters[7].Split(',') : null,
                Auth = parameters[8 + indexOffset],
                Sso = parameters[9 + indexOffset],
                Life = parameters[10 + indexOffset],
                Params = QueryHelpers.ParseQuery(Uri.UnescapeDataString(parameters[11 + indexOffset])),
                Kid = parameters[12 + indexOffset],
                Sig = parameters[13 + indexOffset].Replace('-', '+').Replace('.', '/').Replace('_', '='),
                Signed = string.Join("!", parameters.Take(12 + indexOffset))
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