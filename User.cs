using System;
using System.Collections.Generic;

namespace Spotivy_Nick_en_Niels
{
    internal class User : Person
    {
        public string PasswordHash { get; private set; }
        public string Salt { get; private set; }
        public List<Playlist> Playlists { get; } = new List<Playlist>();
        public List<User> Friends { get; } = new List<User>();
        public List<User> PendingFriendRequests { get; } = new List<User>();
        public List<User> SentFriendRequests { get; } = new List<User>();

        public User(string name, string password) : base(name, password)
        {
            string salt;
            this.PasswordHash = new Login.PasswordManager().GeneratePasswordHash(password, out salt);
            this.Salt = salt;
            Data.GetUsers().Add(this);
        }

        public void CreatePlaylist(string playlistName) 
        {
            new Playlist(playlistName, this);
        }
        public void RemovePlaylist() { }

        public List<Playlist> ShowPlaylists()
        {
            return Playlists;
        }
        public void ShowSongs() 
        {
            foreach (Song song in Data.GetSongs())
            {
                Console.WriteLine(song);
            }
        }
        public void ShowAlbums() { }
        public void ShowArtists() { }
        public void SendFriendRequest()
        {
            Console.WriteLine("Enter the name of the friend you want to add:");
            string friendName = Console.ReadLine();
            User friend = Data.GetUsers().Find(user => user.Name == friendName);
            if (friend != null)
            {
                if (!friend.PendingFriendRequests.Contains(this))
                {
                    SentFriendRequests.Add(friend);
                    friend.PendingFriendRequests.Add(this);
                    Console.WriteLine($"Friend request sent {friend.Name}.");
                }
                else
                {
                    Console.WriteLine($"Friend request to {friend.Name} already sent.");
                }
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
        public void AcceptFriendRequest()
        {
            if (PendingFriendRequests.Count == 0)
            {
                Console.WriteLine("You have no pending friend requests.");
                return;
            }

            Console.WriteLine("Pending friend requests:");
            for (int i = 0; i < PendingFriendRequests.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {PendingFriendRequests[i].Name}");
            }

            Console.WriteLine("Enter the number of the friend request you want to accept:");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= PendingFriendRequests.Count)
            {
                User friend = PendingFriendRequests[choice - 1];
                Friends.Add(friend);
                friend.Friends.Add(this);
                PendingFriendRequests.Remove(friend);
                Console.WriteLine($"You are now friends with {friend.Name}.");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
        public void ShowPendingFriendRequests()
        {
            if (PendingFriendRequests.Count == 0)
            {
                Console.WriteLine("You have no pending friend requests.");
                return;
            }

            Console.WriteLine("Pending friend requests:");
            foreach (var request in PendingFriendRequests)
            {
                Console.WriteLine(request.Name);
            }
        }
        public void DeleteFriendRequest(User friend)
        {
            if (SentFriendRequests.Contains(friend))
            {
                SentFriendRequests.Remove(friend);
                friend.PendingFriendRequests.Remove(this);
                Console.WriteLine($"Friend request to {friend.Name} deleted.");
            }
            else
            {
                Console.WriteLine($"No friend request to {friend.Name} found.");
            }
        }

        public void DenyFriendRequest(User friend)
        {
            if (PendingFriendRequests.Contains(friend))
            {
                PendingFriendRequests.Remove(friend);
                friend.SentFriendRequests.Remove(this);
                Console.WriteLine($"Friend request from {friend.Name} denied.");
            }
            else
            {
                Console.WriteLine($"No pending friend request from {friend.Name} found.");
            }
        }

        public void AddFriend()
        {
            while (true)
            {
                Console.WriteLine("Enter the name of the friend you want to add:");
                string friendName = Console.ReadLine();
                User friend = Data.GetUsers().Find(user => user.Name == friendName);

                if (friend != null)
                {
                    if (this.Friends.Contains(friend))
                    {
                        Console.WriteLine("You are already friends with " + friend.Name);
                    }
                    else
                    {
                        this.Friends.Add(friend);
                        Console.WriteLine("Friend added.");
                        Console.WriteLine("You are now friends with " + friend.Name);
                        Console.WriteLine($"You have {this.Friends.Count} friends.");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("User not found. Please enter a valid user name.");
                }
            }
        }
        public void RemoveFriend()
        {
            while (true)
            {
                Console.WriteLine("Enter the name of the friend you want to remove:");
                string friendName = Console.ReadLine();
                User friend = this.Friends.Find(user => user.Name == friendName);

                if (friend != null)
                {
                    this.Friends.Remove(friend);
                    Console.WriteLine("Friend removed.");
                    Console.WriteLine("You are no longer friends with " + friend.Name);
                    Console.WriteLine("You have " + this.Friends.Count + " friends.");
                    break;
                }
                else
                {
                    Console.WriteLine("User not found in your friends list. Please enter a valid friend name.");
                }
            }
        }

        public void ShowFriendsList()
        {
            if (this.Friends.Count == 0)
            {
                Console.WriteLine("You have no friends in your friends list.");
            }
            else
            {
                Console.WriteLine("Your friends list:");
                foreach (User friend in this.Friends)
                {
                    Console.WriteLine(friend.Name);
                }
            }
        }
        public void CopyPlaylist( Playlist playlistToCopy) 
        {
            Playlists.Add(playlistToCopy);
        }

    }
}
