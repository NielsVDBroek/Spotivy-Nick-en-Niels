using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotivy_Nick_en_Niels
{
    internal class User : Person
    {
        public List<Playlist> Playlists { get; }
        public List<User> Friends { get; }
        public User(string name, string password, Users UsersLibrary) : base(name, password) 
        {
            UsersLibrary.AddUser(this);
        }

        public void PlaySong() {}
        public void PauseSong() {}
        public void SkipSong() {}
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
