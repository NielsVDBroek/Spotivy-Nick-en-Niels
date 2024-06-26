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

        public User(string name, string password) : base(name, password)
        {
            string salt;
            this.PasswordHash = new Login.PasswordManager().GeneratePasswordHash(password, out salt);
            this.Salt = salt;
            Data.GetUsers().Add(this);
        }

        public void PlaySong(Song song) 
        {
            song.PlaySong();
        }
        public void PauseSong(Song song) 
        {
            song.PauseSong();
        }
        public void ResumeSong(Song song) 
        {
            song.ResumeSong();
        }
        public void SkipSong()
        {
            
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
        public void ShowFriendslist() { }
        public static void AddFriend() 
        {
            Console.WriteLine("Enter the name of the friend you want to add:");
            string friendName = Console.ReadLine();
            User friend = Data.GetUsers().Find(user => user.Name == friendName);
            if (friend != null)
            {
                Data.GetUsers().Add(friend);
                Console.WriteLine("Friend added.");
                Console.WriteLine("You are now friends with " + friend.Name);
                Console.WriteLine("You have " + Data.GetUsers().Count + " friends.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
        public static void RemoveFriend() 
        { 
            Console.WriteLine("Enter the name of the friend you want to remove:");
            string friendName = Console.ReadLine();
            User friend = Data.GetUsers().Find(user => user.Name == friendName);
            if (friend != null)
            {
                Data.GetUsers().Remove(friend);
                Console.WriteLine("Friend removed.");
                Console.WriteLine("You are no longer friends with " + friend.Name);
                Console.WriteLine("You have " + Data.GetUsers().Count + " friends.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
        public void ShowFriend() { }
        public void CopyPlaylist( Playlist playlistToCopy) 
        {
            Playlists.Add(playlistToCopy);
        }
    }
}
