using Spotivy_Nick_en_Niels;

internal class Program
{
    static Login.MockUserRepository userRepo = new Login.MockUserRepository();
    static Login.PasswordManager pwdManager = new Login.PasswordManager();

    static async Task Main(string[] args)
    {
        Console.WriteLine("Do you want to login?(L) or sign up?(S)");
        string userChoice = Console.ReadLine() ?? string.Empty;

        if (userChoice.Equals("S", StringComparison.OrdinalIgnoreCase))
        {
            SimulateUserCreation();
        }
        else if (userChoice.Equals("L", StringComparison.OrdinalIgnoreCase))
        {
            SimulateLogin();
        }
        else
        {
            Console.WriteLine("Invalid choice. Exiting.");
            return;
        }
      
        Data.AddStandardData();

        var cts = new CancellationTokenSource();

        Console.WriteLine("Data toegevoegd!");
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

    public static void SimulateUserCreation()
    {
        Console.WriteLine("Please enter username");
        string username = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Please enter password");
        string password = Console.ReadLine() ?? string.Empty;

        // Create User object with username and password
        User user = new User(username, password);

        // Add the user to the repository
        userRepo.AddUser(user);

        // Print the salt and hash
        Console.WriteLine("User created successfully!");
        Console.WriteLine($"Salt for {username}: {user.Salt}");
        Console.WriteLine($"Password Hash for {username}: {user.PasswordHash}");
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

            User user = userRepo.GetUser(username);

            if (user == null)
            {
                Console.WriteLine("User not found. Please try again.");
                continue;
            }

            bool result = pwdManager.IsPasswordMatch(password, user.Salt, user.PasswordHash);

            if (result)
            {
                Console.WriteLine("Password Matched");
                isAuthenticated = true;
            }
            else
            {
                Console.WriteLine("Password not Matched. Please try again.");
            }
        }
    }
}
