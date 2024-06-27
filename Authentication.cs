using System;
using System.Security.Cryptography;
using System.Text;

namespace Spotivy_Nick_en_Niels
{
    internal class Authentication
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
                return hash == m_hashComputer.GetPasswordHashAndSalt(finalString);
            }
        }
        static Authentication.PasswordManager pwdManager = new Authentication.PasswordManager();
        static User currentUser = null;

        public static User GetCurrentUser()
        {
            return currentUser;
        }
        public static void AskUserLogin()
        {
            bool loggedIn = false;
            while (!loggedIn)
            {
                Console.WriteLine("Do you want to login?(L), sign up?(S), or logout?(O)");
                string userChoice = Console.ReadLine() ?? string.Empty;

                if (userChoice.Equals("S", StringComparison.OrdinalIgnoreCase))
                {
                    UserCreation();
                }
                else if (userChoice.Equals("L", StringComparison.OrdinalIgnoreCase))
                {
                    UserLogin();
                    loggedIn = true;
                }
                else if (userChoice.Equals("O", StringComparison.OrdinalIgnoreCase))
                {
                    UserLogout();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Try L, S or O");
                }
            }
        }

        public static void UserCreation()
        {
            Console.WriteLine("Please enter username");
            string username = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Please enter password");
            string password = Console.ReadLine() ?? string.Empty;

            // Create User object with username and password
            User user = new User(username, password);
        }


        public static void UserLogin()
        {
            Console.WriteLine("");

            bool isAuthenticated = false;

            while (!isAuthenticated)
            {
                Console.WriteLine("Please enter username");
                string username = Console.ReadLine() ?? string.Empty;

                Console.WriteLine("Please enter password");
                string password = Console.ReadLine() ?? string.Empty;

                User user = Data.GetUsers().FirstOrDefault(u => u.Name.Equals(username, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    Console.WriteLine("User not found. Please try again.");
                    AskUserLogin();
                    break;
                }


                bool result = pwdManager.IsPasswordMatch(password, user.Salt, user.PasswordHash);
                 
                if (result)
                {
                    Console.WriteLine("Password Matched");
                    currentUser = user;
                    isAuthenticated = true;
                }
                else
                {
                    Console.WriteLine("Password not Matched. Please try again.");
                    AskUserLogin();
                    break;
                }
            }
        }

        public static void UserLogout()
        {
            if (currentUser == null)
            {
                Console.WriteLine("No user is currently logged in.");
            }
            else
            {
                currentUser = null;
                Console.WriteLine("User logged out successfully.");
                Console.WriteLine("");
                AskUserLogin();

            }
        }

    }

    
}
