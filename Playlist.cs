using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotivy_Nick_en_Niels
{
    internal class Playlist : List
    {
        public Playlist(string name, User owner) : base(name, owner) { }

        public void AddSong() { }
        public void RemoveSong() { }
        public void RenameList() { }
        public void RemoveList() { }
    }
}
