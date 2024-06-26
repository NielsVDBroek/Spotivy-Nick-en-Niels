using System;
using Spotivy_Nick_en_Niels;
using static Spotivy_Nick_en_Niels.Login;

internal class Program
{

    static async Task Main(string[] args)
    {
        Data.AddStandardData();
        AskUserLogin();

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
        while (true)
        {
            Console.WriteLine("Enter your command (e.g., 'Play houdini'):");
            string input = Console.ReadLine();

            if (!string.IsNullOrEmpty(input))
            {
                await Client.ParseAndExecuteCommand(input);
            }
            else
            {
                Console.WriteLine("No command entered.");
            }
        }

    }
}
