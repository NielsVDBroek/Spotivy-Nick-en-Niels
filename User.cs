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
            Salt = Login.SaltGenerator.GetSaltString();
            PasswordHash = new Login.PasswordManager().GeneratePasswordHash(password, out string generatedSalt);

            // Print the salt and hash
            Console.WriteLine($"Generated Salt for {name}: {Salt}");
            Console.WriteLine($"Generated Hash for {name}: {PasswordHash}");
        }

        public void PlaySong() { }
        public void PauseSong() { }
        public void SkipSong() { }
        public void CreatePlaylist() { }
        public void RemovePlaylist() { }
        public void ShowPlaylists() { }
        public void ShowSongs() { }
        public void ShowAlbums() { }
        public void ShowArtists() { }
        public void ShowFriendslist() { }
        public void AddFriend() { }
        public void RemoveFriend() { }
        public void ShowFriend() { }
        public void CopyPlaylist() { }
    }
}
