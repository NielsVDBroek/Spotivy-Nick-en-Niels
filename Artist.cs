using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotivy_Nick_en_Niels
{
    internal class Artist : Person
    {
        public List<Song> Songs { get; }
        public List<Album> Albums { get; }
        public Artist(string name, string password) : base(name, password) 
        {
            
        }

        public void CreateSong() { }
        public void RemoveSong() { }
        public void CreateAlbum() { }
        public void RemoveAlbum() { }

    }
}
