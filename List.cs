using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotivy_Nick_en_Niels
{
    internal class List
    {
        public string Name { get; set; }
        private User Owner { get; set; }
        public List<Song> Songs { get; }

        public List(string name, User owner) 
        {
            Name = name;
            Owner = owner;
        }

        public void PlayList() { }
        public void SkipSong() { }
        public void AddToList() { }

    }
}
