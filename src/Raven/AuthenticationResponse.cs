using System;
using System.Net;

namespace Raven
{
    public class AuthenticationResponse
    {
        public int Ver;
        public HttpStatusCode Status;
        public string Msg;
        public string Issue;
        public string Id;
        public Uri Url;
        public string Principal;
        public string[] Ptags;
        public string Auth;
        public string Sso;
        public string Life;
        public string Params;
        public string Kid;
        public string Sig;
    }
}