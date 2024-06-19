using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Spotivy_Nick_en_Niels
{
    internal class Login
    {
        internal static class Utility
        {
            public static byte[] GetBytes(string str)
            {
                return Encoding.UTF8.GetBytes(str);
            }

            public static string GetString(byte[] bytes)
            {
                return Convert.ToBase64String(bytes);
            }

            public static byte[] GetBytesFromBase64String(string base64Str)
            {
                return Convert.FromBase64String(base64Str);
            }
        }


        public static class SaltGenerator
        {
            private static RNGCryptoServiceProvider m_cryptoServiceProvider;
            private const int SALT_SIZE = 24;

            static SaltGenerator()
            {
                m_cryptoServiceProvider = new RNGCryptoServiceProvider();
            }

            public static string GetSaltString()
            {
                byte[] saltBytes = new byte[SALT_SIZE];
                m_cryptoServiceProvider.GetNonZeroBytes(saltBytes);
                return Utility.GetString(saltBytes);
            }
        }

        public class HashComputer
        {
            public string GetPasswordHashAndSalt(string message)
            {
                using (SHA256 sha = new SHA256CryptoServiceProvider())
                {
                    byte[] dataBytes = Utility.GetBytes(message);
                    byte[] resultBytes = sha.ComputeHash(dataBytes);
                    return Utility.GetString(resultBytes);
                }
            }
        }



        public class PasswordManager
        {
            HashComputer m_hashComputer = new HashComputer();

            public string GeneratePasswordHash(string plainTextPassword, out string salt)
            {
                salt = SaltGenerator.GetSaltString();
                string finalString = plainTextPassword + salt;
                return m_hashComputer.GetPasswordHashAndSalt(finalString);
            }

            public bool IsPasswordMatch(string password, string salt, string hash)
            {
                string finalString = password + salt;
                Console.WriteLine($"wachtwoord compaired: {finalString}");
                return hash == m_hashComputer.GetPasswordHashAndSalt(finalString);
                

            }
        }
    }
}
