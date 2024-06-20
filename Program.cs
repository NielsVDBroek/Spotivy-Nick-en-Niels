using Spotivy_Nick_en_Niels;
using static Spotivy_Nick_en_Niels.Login;

internal class Program
{
    static Login.PasswordManager pwdManager = new Login.PasswordManager();
    static User currentUser = null;

    static async Task Main(string[] args)
    {
        bool loggedIn = false;
        while (!loggedIn)
        {
            Console.WriteLine("Do you want to login?(L), sign up?(S), or logout?(O)");
            string userChoice = Console.ReadLine() ?? string.Empty;

            if (userChoice.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                SimulateUserCreation();
            }
            else if (userChoice.Equals("L", StringComparison.OrdinalIgnoreCase))
            {
                SimulateLogin();
                loggedIn = true;
            }
            else if (userChoice.Equals("O", StringComparison.OrdinalIgnoreCase))
            {
                SimulateLogout();
            }
            else
            {
                Console.WriteLine("Invalid choice. Exiting.");
            }

            if (currentUser != null)
            {
                Data.AddStandardData();

                var cts = new CancellationTokenSource();

                Console.WriteLine("Data added!");
                Console.WriteLine(DateTime.Now);

                Console.WriteLine();
                Console.WriteLine("All artists:");
                foreach (Artist artist in Data.GetArtists())
                {
                    Console.WriteLine(artist);
                }
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("All songs:");
                foreach (Song song in Data.GetSongs())
                {
                    Console.WriteLine(song);
                }
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("All users:");
                foreach (User user in Data.GetUsers())
                {
                    Console.WriteLine(user);
                }
                Console.WriteLine();

                foreach (Song song in Data.GetSongs())
                {
                    var playTask = song.PlaySong(cts.Token);

                    await Task.Delay(5000);
                    song.PauseSong();
                    await Task.Delay(5000);
                    song.ResumeSong();
                    await playTask;
                }
            }
        }
    }

    public static void SimulateUserCreation()
    {
        Console.WriteLine("Please enter username");
        string username = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Please enter password");
        string password = Console.ReadLine() ?? string.Empty;

        // Create User object with username and password
        User user = new User(username, password);

        // Print the salt and hash
        Console.WriteLine("User created successfully!");
        Console.WriteLine($"Salt: {user.Salt}");
        Console.WriteLine($"Password Hash: {user.PasswordHash}");
        Console.WriteLine();
    }

    public static void SimulateLogin()
    {
        Console.WriteLine("Now let us simulate the password comparison");

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
                continue;
            }

            // Print the entered password and the stored password hash for comparison
            Console.WriteLine($"Entered Password: {password}");
            Console.WriteLine($"Stored Password Hash: {user.PasswordHash}");
            Console.WriteLine($"Stored Salt: {user.Salt}");

            bool result = pwdManager.IsPasswordMatch(password, user.Salt, user.PasswordHash);
            Console.WriteLine($"Password Match: {result}");

            if (result)
            {
                Console.WriteLine("Password Matched");
                currentUser = user;
                isAuthenticated = true;
            }
            else
            {
                Console.WriteLine("Password not Matched. Please try again.");
            }
        }
    }

    public static void SimulateLogout()
    {
        if (currentUser == null)
        {
            Console.WriteLine("No user is currently logged in.");
        }
        else
        {
            currentUser = null;
            Console.WriteLine("User logged out successfully.");
        }
    }
}
