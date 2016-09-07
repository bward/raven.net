using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Raven.Keys
{
    public class KeyProvider
    {
        private static string LoadKey(string key)
        {
            return Resource.ResourceManager.GetString("pub" + key + "x509")
                .Replace("-----BEGIN CERTIFICATE-----", "")
                .Replace("-----END CERTIFICATE-----", "")
                .Replace("\n", "");
        }

        public static RSA RSAFromKey(string key)
        {
            var encoded = LoadKey(key);
            var certificate = new X509Certificate2(Convert.FromBase64String(encoded));
            return RSACertificateExtensions.GetRSAPublicKey(certificate);
        }
    }
}