using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace butakov_pr3.Services
{
    public class Hash
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(password);

                byte[] hash = sha256.ComputeHash(sourceBytes);

                return BitConverter.ToString(hash)
                    .Replace("-", string.Empty);
            }
        }
    }
}