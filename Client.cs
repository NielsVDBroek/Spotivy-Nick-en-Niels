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
        private static User currentUser => Login.GetCurrentUser();
        public static async Task ParseAndExecuteCommand(string input)
        {
            if (input.Equals("show"))
            {
                HandleShowOption();
            }
            else if (input.Equals("logout"))
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
                                    currentUser.CreatePlaylist(playlistName);
                                    break;
                                case "friend":
                                    currentUser.SendFriendRequest();
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
                                    currentUser.RemoveFriend();
                                    break;
                                case "request":
                                    if (currentUser != null)
                                    {
                                        Console.WriteLine("Enter the name of the friend request you want to delete:");
                                        string friendName = Console.ReadLine();
                                        User friend = Data.GetUsers().Find(u => u.Name == friendName);
                                        if (friend != null)
                                        {
                                            currentUser.DeleteFriendRequest(friend);
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
                                currentUser.AcceptFriendRequest();
                            }
                            break;
                        case "deny":
                            if (parameter.Equals("request"))
                            {
                                if (currentUser != null)
                                {
                                    Console.WriteLine("Enter the name of the friend request you want to deny:");
                                    string friendName = Console.ReadLine();
                                    User friend = Data.GetUsers().Find(u => u.Name == friendName);
                                    if (friend != null)
                                    {
                                        currentUser.DenyFriendRequest(friend);
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
                        case "playlist":
                            var selectedPlaylist = currentUser.Playlists.Find(playlist => playlist.Name.Equals(parameter, StringComparison.OrdinalIgnoreCase));
                            if (selectedPlaylist != null)
                            {
                                Console.WriteLine($"What do you want to do with {selectedPlaylist}?");
                                Console.WriteLine("Songs, Play, Add, Remove");
                                string playlistInput = Console.ReadLine().ToLower();
                                switch (playlistInput)
                                {
                                    case "songs":
                                        if (selectedPlaylist.Songs.Count() == 0)
                                        {
                                            Console.WriteLine("No songs in this list!");
                                        }
                                        else
                                        {
                                            foreach (Song song in selectedPlaylist.Songs)
                                            {
                                                Console.WriteLine(song);
                                            }
                                        }
                                        break;
                                    case "play":
                                        selectedPlaylist.PlayList();
                                        break;
                                    case "add":
                                        Console.WriteLine("Which song do you want to add to this playlist?");
                                        string playlistSongAddInput = Console.ReadLine().ToLower();
                                        var songToAddToList = Data.GetSongs().Find(song => song.Name.Equals(playlistSongAddInput, StringComparison.OrdinalIgnoreCase));
                                        selectedPlaylist.AddSong(songToAddToList);
                                        break;
                                    case "remove":
                                        Console.WriteLine("Which song do you want to add to this playlist?");
                                        string playlistSongRemoveInput = Console.ReadLine().ToLower();
                                        var songToRemoveFromList = Data.GetSongs().Find(song => song.Name.Equals(playlistSongRemoveInput, StringComparison.OrdinalIgnoreCase));
                                        selectedPlaylist.RemoveSong(songToRemoveFromList);
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"{selectedPlaylist} not found.");
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
            Console.WriteLine("Logout - Log out");
            Console.WriteLine("Show + Help - Show help menu");
            Console.WriteLine("Show + Friends - Show friends list");
            Console.WriteLine("Show + User - Show current logged-in user");
            Console.WriteLine("Show + Requests - Show pending friend requests");
            Console.WriteLine("While song is playing -> Spacebar - Pause/Resume the song");
            Console.WriteLine("Add + Friend - Add a friend");
            Console.WriteLine("Add + Playlist - create a playlist");
            Console.WriteLine("Remove + Friend - Remove a friend");
            Console.WriteLine("Remove + Request - Delete/cancel friend request");
            Console.WriteLine("Accept + Request - Accept friend request");
            Console.WriteLine("Deny + Request - Deny incoming friend request");
            Console.WriteLine("Play + (song to play) - Play a song");
            Console.WriteLine("info + (song to get info from) - Play a song");
            Console.WriteLine("playlist + (playlistname) - select a playlist");
            Console.WriteLine("within playlist -> Songs - shows the songs within the playlist");
            Console.WriteLine("within playlist -> Play - plays all the songs in the playlist");
            Console.WriteLine("within playlist -> Add - allows the user to add a song to the playlist");
            Console.WriteLine("within playlist -> Remove - allows the user to remove a song from the playlist");
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
                    currentUser.ShowFriendsList();
                    break;
                case "user":
                        Console.WriteLine("Current logged-in user: " + currentUser.Name);
                    break;
                case "requests":
                    currentUser.ShowPendingFriendRequests();
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
                    currentUser.ShowPlaylists();
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
