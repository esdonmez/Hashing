using System;
using System.Security.Cryptography;
using System.Text;

namespace hashing
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] passwords = {
                "123456",
                "password",
                "admin",
                "one2***"
            };

            Console.WriteLine("SHA256");
            foreach (var password in passwords)
            {
                string salt = getSalt();
                Console.WriteLine($@"{{
    'password': '{password}',
    'salt': '{salt}',
    'hash': '{getHash256(password + salt)}'
}}");  
            }

            Console.WriteLine("\nSHA512");
            foreach (var password in passwords)
            {
                string salt = getSalt();
                Console.WriteLine($@"{{
    'password': '{password}',
    'salt': '{salt}',
    'hash': '{getHash512(password + salt)}'
}}");
            }

            Console.Read();
        }

        private static string getHash256(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private static string getHash512(string text)
        {
            using (var sha512 = SHA512.Create())
            {
                var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(text));

                //return Convert.ToBase64String(hashedBytes, 0, hashedBytes.Length)
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private static string getSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
