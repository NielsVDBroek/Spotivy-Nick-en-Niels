using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Spotivy_Nick_en_Niels
{
    internal class Login
    {
        static class Utility
        {
            // utilty function to convert string to byte[]        
            public static byte[] GetBytes(string str)
            {
                byte[] bytes = new byte[str.Length * sizeof(char)];
                System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
                return bytes;
            }

            // utilty function to convert byte[] to string        
            public static string GetString(byte[] bytes)
            {
                char[] chars = new char[bytes.Length / sizeof(char)];
                System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
                return new string(chars);
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
                // Lets create a byte array to store the salt bytes
                byte[] saltBytes = new byte[SALT_SIZE];

                // lets generate the salt in the byte array
                m_cryptoServiceProvider.GetNonZeroBytes(saltBytes);

                // Let us get some string representation for this salt
                string saltString = Utility.GetString(saltBytes);

                // Now we have our salt string ready lets return it to the caller
                return saltString;
            }
        }

        public class HashComputer
        {
            public string GetPasswordHashAndSalt(string message)
            {
                // Let us use SHA256 algorithm to 
                // generate the hash from this salted password
                SHA256 sha = new SHA256CryptoServiceProvider();
                byte[] dataBytes = Utility.GetBytes(message);
                byte[] resultBytes = sha.ComputeHash(dataBytes);

                // return the hash string to the caller
                return Utility.GetString(resultBytes);
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

        public class User
        {
            public string UserId { get; set; } = string.Empty;
            public string PasswordHash { get; set; } = string.Empty;
            public string Salt { get; set; } = string.Empty;

            // Parameterless constructor
            public User() { }

            // Constructor with parameters
            public User(string userId, string passwordHash, string salt)
            {
                UserId = userId;
                PasswordHash = passwordHash;
                Salt = salt;
            }

            public override string ToString()
            {
                return $"UserId: {UserId}, PasswordHash: {PasswordHash}, Salt: {Salt}";
            }
        }

        class MockUserRepository
        {
            List<User> users = new List<User>();

            // Function to add the user to in-memory dummy DB
            public void AddUser(User user)
            {
                users.Add(user);
            }

            // Function to retrieve the user based on user id
            public User GetUser(string userid)
            {
                return users.Single(u => u.UserId == userid);
            }
        }


        class Program
        {
            // Dummy repository class for DB operations
            static MockUserRepository userRepo = new MockUserRepository();

            // Let us use the Password manager class to generate the password ans salt
            static PasswordManager pwdManager = new PasswordManager();

            static void Main(string[] args)
            {
                // Let us first test the password hash creation i.e. User creation
                string salt = SimulateUserCreation();

                // Now let is simualte the password comparison
                SimulateLogin(salt);

                Console.ReadLine();
            }

            public static string SimulateUserCreation()
            {
                Console.WriteLine("Let us first test the password hash creation i.e. User creation");
                Console.WriteLine("Please enter user id");
                string userid = Console.ReadLine() ?? string.Empty; // Handle null case for userid

                Console.WriteLine("Please enter password");
                string password = Console.ReadLine() ?? string.Empty; // Handle null case for password

                string salt = null ?? string.Empty;

                string passwordHash = pwdManager.GeneratePasswordHash(password, out salt);

                // Let us save the values in the database
                User user = new User
                {
                    UserId = userid,
                    PasswordHash = passwordHash,
                    Salt = salt
                };

                // Lets Add the User to the database
                userRepo.AddUser(user);

                return salt;
            }

            public static void SimulateLogin(string salt)
            {
                Console.WriteLine("Now let us simulate the password comparison");

                Console.WriteLine("Please enter user id");
                string userid = Console.ReadLine() ?? string.Empty; // Handle null case for userid

                Console.WriteLine("Please enter password");
                string password = Console.ReadLine() ?? string.Empty; // Handle null case for password

                // Let us retrieve the values from the database
                User user2 = userRepo.GetUser(userid);

                bool result = pwdManager.IsPasswordMatch(password, user2.Salt, user2.PasswordHash);

                if (result)
                {
                    Console.WriteLine("Password Matched");
                }
                else
                {
                    Console.WriteLine("Password not Matched");
                }
            }

        }

    }
}
