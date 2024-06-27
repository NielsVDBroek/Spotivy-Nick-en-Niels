using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotivy_Nick_en_Niels
{
    internal class Playlist : List
    {
        public Playlist(string name, User owner) : base(name, owner) 
        {
            owner.Playlists.Add(this);
        }

        public void AddSong(Song songToAdd) 
        {
            if (this.Songs.Contains(songToAdd))
            {
                Console.WriteLine($"{songToAdd} is already in this list.");
            }
            else
            {
                this.Songs.Add(songToAdd);
            }
        }

        public void RemoveSong(Song songToRemove) 
        {
            if (this.Songs.Contains(songToRemove))
            {
                this.Songs.Remove(songToRemove);
            }
            else
            {
                Console.WriteLine($"{songToRemove} is not in this list.");
            }
            
        }

        public void RenameList() 
        {
            String newListName = Console.ReadLine();
            this.Name = newListName;
        }
    }
}
