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
            if (input.Equals("logout"))
            {
                Login.UserLogout();
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
                        case "add":
                            switch (parameter)
                            {
                                case "playlist":
                                    Console.WriteLine("Enter name for your new playlist:");
                                    string playlistName = Console.ReadLine();
                                    Login.GetCurrentUser().CreatePlaylist(playlistName);
                                    break;
                                case "friend":
                                    Login.GetCurrentUser().SendFriendRequest();
                                    break;
                                default:
                                    Console.WriteLine($"Unknown command: {parameter}");
                                    break;

                            }
                            break;
                        case "remove":
                            switch (parameter)
                            {
                                case "playlist":
                                    //Remove playlist
                                    break;
                                case "friend":
                                    Login.GetCurrentUser().RemoveFriend();
                                    break;
                                case "request":
                                    User loggedInUser = Login.GetCurrentUser();
                                    if (loggedInUser != null)
                                    {
                                        Console.WriteLine("Enter the name of the friend request you want to delete:");
                                        string friendName = Console.ReadLine();
                                        User friend = Data.GetUsers().Find(u => u.Name == friendName);
                                        if (friend != null)
                                        {
                                            loggedInUser.DeleteFriendRequest(friend);
                                        }
                                        else
                                        {
                                            Console.WriteLine("User not found.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No user is currently logged in.");
                                    }
                                    break;
                                default:
                                    Console.WriteLine($"Unknown command: {parameter}");
                                    break;

                            }
                            break;
                        case "accept":
                            if (parameter.Equals("request"))
                            {
                                Login.GetCurrentUser().AcceptFriendRequest();
                            }
                            break;
                        case "deny":
                            if (parameter.Equals("request"))
                            {
                                User loggedInUser = Login.GetCurrentUser();
                                if (loggedInUser != null)
                                {
                                    Console.WriteLine("Enter the name of the friend request you want to deny:");
                                    string friendName = Console.ReadLine();
                                    User friend = Data.GetUsers().Find(u => u.Name == friendName);
                                    if (friend != null)
                                    {
                                        loggedInUser.DenyFriendRequest(friend);
                                    }
                                    else
                                    {
                                        Console.WriteLine("User not found.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No user is currently logged in.");
                                }
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

        public static void ShowHelp()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("Show + Help - Show help menu");
            Console.WriteLine("Spacebar - Pause/Resume the song(if a song is playing)");
            Console.WriteLine("Add + Friend - Add a friend");
            Console.WriteLine("Show + Friends - Show friends list");
            Console.WriteLine("Logout - Log out");
            Console.WriteLine("Remove + Friend - Remove a friend");
            Console.WriteLine("Show + User - Show current logged-in user");
            Console.WriteLine("Accept + Request - Accept friend request");
            Console.WriteLine("Show + Requests - Show pending friend requests");
            Console.WriteLine("Remove + Request - Delete/cancel friend request");
            Console.WriteLine("Deny + Request - Deny incoming friend request");
            Console.WriteLine("Play + (song to play) - Play a song");
            Console.WriteLine("info + (song to get info from) - Play a song");
            Console.WriteLine("Add + Playlist - create a playlist");

            //alll new options
        }

        public static string AskShowOption()
        {
            Console.WriteLine("What do you want to show?");
            Console.WriteLine("Help, Songs, Users, Playlists, Artists, Friends, Requests, User?");
            string showInput = Console.ReadLine().ToLower();
            return showInput;
        }

        public static void HandleShowOption()
        {
            switch (AskShowOption())
            {
                case "help":
                    ShowHelp();
                    break;
                case "friends":
                        Login.GetCurrentUser().ShowFriendsList();
                    break;
                case "user":
                        Console.WriteLine("Current logged-in user: " + Login.GetCurrentUser().Name);
                    break;
                case "requests":
                        Login.GetCurrentUser().ShowPendingFriendRequests();
                    break;
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
    }
}
