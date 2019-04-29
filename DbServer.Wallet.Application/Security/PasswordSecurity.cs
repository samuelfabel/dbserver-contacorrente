using System;
using System.Security.Cryptography;
using System.Text;

namespace DbServer.Wallet.Application.Security
{
    public class PasswordSecurity
    {
        public bool Verify(string data, string sign)
        {
            return sign == Encrypt(data);
        }

        public string Encrypt(string text)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(result).Replace("-", string.Empty);
            }
        }
    }
}