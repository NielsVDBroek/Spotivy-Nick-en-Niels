using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Spotivy_Nick_en_Niels
{
    public static class Client
    {
        public static async Task ParseAndExecuteCommand(string input)
        {
            if (input.Equals("show"))
            {
                HandleShowOption();
            }
            else
            {
                string[] parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    string command = parts[0].ToLower();
                    string parameter = parts[1];

                    switch (command)
                    {
                        case "play":
                            await PlaySongBasedOnName(parameter);
                            break;
                        case "info":
                            GetSongInfo(parameter);
                            break;
                        case "create":
                            if (parameter.Equals("playlist"))
                            {
                                Console.WriteLine("Enter name for your new playlist:");
                                string playlistName = Console.ReadLine().ToLower();
                                Login.GetCurrentUser().CreatePlaylist(playlistName);

                            }
                            break;
                        default:
                            Console.WriteLine($"Unknown command: {command}");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid command format.");
                }
            }
        }

        public static string AskShowOption()
        {
            Console.WriteLine("What do you want to show?");
            Console.WriteLine("Songs, Users, Playlists, Artists, Albums?");
            string showInput = Console.ReadLine().ToLower();
            return showInput;
        }

        public static void HandleShowOption()
        {
            switch (AskShowOption())
            {
                case "songs":
                    if (Data.GetSongs().Count > 0)
                    {
                        Console.WriteLine("All songs:");
                        foreach (Song song in Data.GetSongs())
                        {
                            Console.WriteLine(song);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No songs available.");
                    }
                    break;
                case "users":
                    if (Data.GetSongs().Count > 0)
                    {
                        Console.WriteLine("All users:");
                        foreach (User user in Data.GetUsers())
                        {
                            Console.WriteLine(user);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No users available.");
                    }
                    break;
                case "artists":
                    if (Data.GetSongs().Count > 0)
                    {
                        Console.WriteLine("All artists:");
                        foreach (Artist artist in Data.GetArtists())
                        {
                            Console.WriteLine(artist);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No artists available.");
                    }
                    break;
                case "playlists":
                    Login.GetCurrentUser().ShowPlaylists();
                    break;
                case "albums":
                    Console.WriteLine("All albums:");
                    // all albums
                    break;
            }
        }
        public static async Task PlaySongBasedOnName(string input)
        {
            var songToPlay = Data.GetSongs().Find(song => song.Name.Equals(input, StringComparison.OrdinalIgnoreCase));

            if (songToPlay != null)
            {
                var playTask = songToPlay.PlaySong();

                while (!playTask.IsCompleted)
                {
                    if (Console.KeyAvailable)
                    {
                        var userInput = Console.ReadKey(intercept: true);
                        if (userInput.Key == ConsoleKey.Spacebar)
                        {
                            songToPlay.PauseSong();
                            userInput = Console.ReadKey(intercept: true);
                            if (userInput.Key == ConsoleKey.Spacebar)
                            {
                                songToPlay.ResumeSong();
                            }
                        }
                        if (userInput.Key == ConsoleKey.Q)
                        {
                            songToPlay.EndSong();
                        }
                    }
                    await Task.Delay(100);
                }

                await playTask;
                await Task.Delay(1500);
            }
            else
            {
                Console.WriteLine($"{songToPlay} not found.");
            }
        }

        public static async Task GetSongInfo(string input)
        {
            Song songToGetInfo = Data.GetSongs().Find(song => song.Name.Equals(input, StringComparison.OrdinalIgnoreCase));

            if (songToGetInfo != null)
            {
                songToGetInfo.ShowInfo();
            }
            else
            {
                Console.WriteLine($"{songToGetInfo} not found.");
            }
        }

        public static void ShowHelp()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("H - Show help");
            Console.WriteLine("Spacebar - Pause/Resume the song(if a song is playing)");
            Console.WriteLine("A - Add a friend");
            Console.WriteLine("F - Show friends list");
            Console.WriteLine("O - Logout");
            Console.WriteLine("R - Remove a friend");
            Console.WriteLine("U - Show current logged-in user");
            Console.WriteLine("V - Accept friend request");
            Console.WriteLine("P - Show pending friend requests");
            Console.WriteLine("D - Delete/cancel friend request");
            Console.WriteLine("N - Deny incoming friend request");
        }

    }
}
