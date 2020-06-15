using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HtmlAnalysis.Services.Help
{
    public static class PrimaryKeyProcessor
    {
        private static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string GenerateHash(string input)
        {
            Random rnd = new Random();
            byte[] bytes = Encoding.UTF8.GetBytes(input + "1");
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
