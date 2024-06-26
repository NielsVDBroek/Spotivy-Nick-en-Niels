using System;
using Spotivy_Nick_en_Niels;
using static Spotivy_Nick_en_Niels.Login;

internal class Program
{

    static async Task Main(string[] args)
    {
        AskUserLogin();

        Data.AddStandardData();

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
            var playTask = song.PlaySong();

            while (!playTask.IsCompleted)
            {
                if (Console.KeyAvailable)
                {
                    var userInput = Console.ReadKey(intercept: true);
                    if (userInput.Key == ConsoleKey.Spacebar)
                    {
                        song.PauseSong();
                        userInput = Console.ReadKey(intercept: true);
                        if (userInput.Key == ConsoleKey.Spacebar)
                        {
                            song.ResumeSong();
                        }
                    }
                }
                await Task.Delay(100);
            }

            await playTask;
            await Task.Delay(3000);
        }
    }
}
