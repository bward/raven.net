using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace BJW.Raven
{
    internal class KeyProvider
    {
        private static string LoadKey(string key)
        {
            return Certificates.ResourceManager.GetString("pub" + key + "x509")
                .Replace("-----BEGIN CERTIFICATE-----", "")
                .Replace("-----END CERTIFICATE-----", "")
                .Replace("\n", "");
        }

        public static RSA RSAFromKey(string key)
        {
            var encoded = LoadKey(key);
            var certificate = new X509Certificate2(Convert.FromBase64String(encoded));
            return certificate.GetRSAPublicKey();
        }
    }
}