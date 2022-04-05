using System;
using System.Security.Cryptography;
using System.Text;

namespace Application.Common.Helpers
{
    public class PasswordHashGenerator
    {
        public static PasswordHashingResult GenerateHash(string password, string saltHash = null)
        {
            if(saltHash == null)
            {
                saltHash = GenerateSalt();
            }
            SHA512 sha512 = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", password, saltHash));
            byte[] hash = sha512.ComputeHash(bytes);

            return new PasswordHashingResult { SaltHash = saltHash, PasswordHash = GetStringFromHash(hash) };
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for(int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        private static string GenerateSalt()
        {
            string salt = string.Empty;
            Random random = new Random();
            int j = 0;
            for(int i = 0; i < 64; i++)
            {
                j = random.Next(62);
                j += ((j < 10) ? 48 : ((j < 36) ? 55 : 61));
                salt += ((char)j).ToString();
            }
            return salt;
        }
    }
}
